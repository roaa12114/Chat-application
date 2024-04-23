using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace AllChatServer
{
    public partial class MainWindow : Window
    {
        struct ClientInfo
        {
            public Socket clientSocket;   //Socket of the client
            public string clientUsername;  //Name by which the user logged into the chat room
        }
        ArrayList clientList;
        Socket serverSocket;
        byte[] byteData = new byte[1024];

        public MainWindow()
        {
            InitializeComponent();
            Window1 window1 = new Window1();
            window1.Show();
            this.Close();
        }

        private void startServerButtonClicked(object sender, RoutedEventArgs e)
        {
            clientList = new ArrayList();

            try
            {
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 1000);
                serverSocket.Bind(ipEndPoint);
                serverSocket.Listen(4);
                serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);
                serverTextBox.Text = "Server is started!";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+"On server start");
            }


        }

        private void OnAccept(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = serverSocket.EndAccept(ar);
                serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);
                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, 
                                            new AsyncCallback(OnReceive), clientSocket);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "On Accept");
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
        
            try
            {
                Socket clientSocket = (Socket)ar.AsyncState;
                clientSocket.EndReceive(ar);

                Data msgReceived = new Data();
                msgReceived.byteToData(byteData);

                Data msgToSend = new Data();
                byte[] byteMessage;

                msgToSend.command = msgReceived.command;
                msgToSend.senderUsername = msgReceived.senderUsername;
                msgToSend.receiverUsername = msgReceived.receiverUsername;

                switch (msgReceived.command)
                {
                    case Command.LOGIN:

                        msgToSend.command = Command.ACCEPT;

                        ClientInfo clientInfo = new ClientInfo();
                        clientInfo.clientSocket = clientSocket;
                        clientInfo.clientUsername = msgReceived.senderUsername;
                        clientList.Add(clientInfo);

                        msgToSend.receiverUsername = null;
                        msgToSend.stringMessage = "<<<" + msgReceived.senderUsername + " has joined the room>>>\n";

                        byteMessage = msgToSend.dataToByte();
                        foreach (ClientInfo client in clientList)
                        {
                            if (client.clientSocket != clientSocket)
                            {
                                client.clientSocket.Send(byteMessage, 0, byteMessage.Length, SocketFlags.None);
                            }
                        }
                        break;

                    case Command.LOGOUT:

                        int i = 0;
                        foreach (ClientInfo client in clientList)
                        {
                            if (client.clientSocket == clientSocket)
                            {
                                clientList.RemoveAt(i);
                                break;
                            }
                            i++;
                        }
                        clientSocket.Close();
                        msgToSend.stringMessage = "<<<" + msgReceived.senderUsername + " has left the room>>>\n";

                        byteMessage = msgToSend.dataToByte();
                        foreach (ClientInfo client in clientList)
                        {
                            if (client.clientSocket != clientSocket)
                            {
                                client.clientSocket.Send(byteMessage, 0, byteMessage.Length, SocketFlags.None);
                            }
                        }
                        break;

                    case Command.PUBLIC_MESSAGE:
                        msgToSend.stringMessage = msgReceived.senderUsername + ": " + msgReceived.stringMessage;
                        byteMessage = msgToSend.dataToByte();

                        foreach (ClientInfo client in clientList)
                        {
                            if (client.clientSocket != clientSocket)
                            {
                                client.clientSocket.Send(byteMessage, 0, byteMessage.Length, SocketFlags.None);
                            }
                        }

                        break;

                    case Command.PRIVATE_MESSAGE:
                        msgToSend.stringMessage = "(Private Message): " + msgReceived.senderUsername +
                                                    ": " + msgReceived.stringMessage;
                        byteMessage = msgToSend.dataToByte();

                        foreach (ClientInfo client in clientList)
                        {
                            if (client.clientSocket != clientSocket && client.clientUsername.Equals(msgReceived.receiverUsername))
                            {
                                client.clientSocket.Send(byteMessage, 0, byteMessage.Length, SocketFlags.None);
                            }
                        }
                        break;

                    case Command.LIST:
                        msgToSend.command = Command.LIST;
                        msgToSend.senderUsername = null;
                        msgToSend.receiverUsername = null;
                        msgToSend.stringMessage = null;
                        foreach (ClientInfo client in clientList)
                        {
                            msgToSend.stringMessage += client.clientUsername + "*";
                        }
                        byteMessage = msgToSend.dataToByte();
                        clientSocket.BeginSend(byteMessage, 0, byteMessage.Length, SocketFlags.None,
                                new AsyncCallback(OnSend), clientSocket);
                        break;
                }

                if (msgReceived.command != Command.LOGOUT)
                {
                    clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None,
                        new AsyncCallback(OnReceive), clientSocket);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "On Receive");
            }
        }

        private void OnSend(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndSend(ar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "On Send");
            }
        }

        private void stopButtonClicked(object sender, RoutedEventArgs e)
        {
            serverSocket.Close();
            serverTextBox.Text = "Server is stopped!";
        }
    }
}
