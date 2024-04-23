using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllChatClient
{
    public enum Command
    {
        LOGIN,
        LOGOUT,
        PUBLIC_MESSAGE,
        PRIVATE_MESSAGE,
        LIST,
        ACCEPT,
        DECLINE,
        NULL
    }
    public class Data
    {
        public Command command;
        public string senderUsername;
        public string receiverUsername;
        public string stringMessage;

        public Data()
        {
            this.command = Command.NULL;
            this.senderUsername = null;
            this.receiverUsername = null;
            this.stringMessage = null;
        }

        public void byteToData(byte[] data)
        {
            this.command = (Command)BitConverter.ToInt32(data, 0);
            int senderUsernameLength = BitConverter.ToInt32(data, 4);
            int receiverUsernameLength = BitConverter.ToInt32(data, 8);
            int stringMessageLength = BitConverter.ToInt32(data, 12);

            if (senderUsernameLength > 0)
                this.senderUsername = Encoding.UTF8.GetString(data, 16, senderUsernameLength);
            else
                this.senderUsername = null;
            if (receiverUsernameLength > 0)
                this.receiverUsername = Encoding.UTF8.GetString(data, 16 + senderUsernameLength, receiverUsernameLength);
            else
                this.receiverUsername = null;
            if (stringMessageLength > 0)
                this.stringMessage = Encoding.UTF8.GetString(data, 16 + receiverUsernameLength + receiverUsernameLength, stringMessageLength);
            else
                this.stringMessage = null;
        }

        public byte[] dataToByte()
        {
            List<byte> result = new List<byte>();
            result.AddRange(BitConverter.GetBytes((int)command));
            if (senderUsername != null)
                result.AddRange(BitConverter.GetBytes(senderUsername.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));
            if (receiverUsername != null)
                result.AddRange(BitConverter.GetBytes(receiverUsername.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));
            if (stringMessage != null)
                result.AddRange(BitConverter.GetBytes(stringMessage.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));

            if (senderUsername != null)
                result.AddRange(Encoding.UTF8.GetBytes(senderUsername));
            if (receiverUsername != null)
                result.AddRange(Encoding.UTF8.GetBytes(receiverUsername));
            if (stringMessage != null)
                result.AddRange(Encoding.UTF8.GetBytes(stringMessage));

            return result.ToArray();
        }
    }
}
