using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace AllChatServer
{
    public class ClientDetails
    {
        public string username { get; set; }
        public Socket userSocket { get; set; }

        public ClientDetails(string username, Socket userSocket)
        {
            this.username = username;
            this.userSocket = userSocket;
        }
    }
}
