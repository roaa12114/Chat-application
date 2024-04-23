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
using System.Windows.Shapes;
using SimpleTCP;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace AllChatServer
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string currentPath = "F:\\semester 4\\telecommunication\\AllChat-main\\AllChatFiles";
        SimpleTcpServer server;
        List<ClientDetails> listOfClients = new List<ClientDetails>();

        public Window1()
        {
            InitializeComponent();
        }
        private void ServerLoaded(object sender, RoutedEventArgs e)
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0x13; //enter key
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += ServerDataReceived;
        }
        private void ServerDataReceived(object sender, SimpleTCP.Message e)
        {
            string listToBeSent = "list:All:";

            if (e.MessageString.Contains("just joined"))
            {
                String user = e.MessageString.Substring(4, e.MessageString.Length - 4 - 18);
                Socket socket = e.TcpClient.Client;
                listOfClients.Add(new ClientDetails(user, socket));

                server.Broadcast(e.MessageString);
                serverTextBox.Dispatcher.Invoke((Action)delegate ()
                {
                    serverTextBox.Text += e.MessageString;
                });
            }

            if (e.MessageString.Contains("list"))
            {
                foreach (ClientDetails user in listOfClients)
                {
                    listToBeSent += user.username + ":";
                }
                List<byte> vs = new List<byte>();
                vs.AddRange(Encoding.UTF8.GetBytes(listToBeSent));
                e.TcpClient.Client.Send(vs.ToArray());
            }

            if (e.MessageString.Contains("#"))
            {
                if (e.MessageString.Substring(0, 3).Equals("All"))
                {
                    if (!e.MessageString.Substring(4, 4).Equals("File"))
                    {
                        string toAll = "[All] " + e.MessageString.Substring(4, e.MessageString.Length - 4) + "\n";
                        server.Broadcast(toAll);
                        serverTextBox.Dispatcher.Invoke((Action)delegate ()
                        {
                            serverTextBox.Text += toAll;
                        });
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Cannot send a file to everyone!");
                    }
                }
                else
                {
                    int posOfDelimiter = e.MessageString.IndexOf('#');
                    string user = e.MessageString.Substring(0, posOfDelimiter);
                    //if its not a file
                    if (!e.MessageString.Substring(user.Length + 1, 4).Equals("File"))
                    {
                        string msg = "[Private] " + e.MessageString.Substring(posOfDelimiter + 1, e.MessageString.Length - posOfDelimiter - 1) + "\n";
                        Socket replySocket = findSocket(user, listOfClients);
                        if (replySocket != null)
                        {
                            List<byte> vs = new List<byte>();
                            vs.AddRange(Encoding.UTF8.GetBytes(msg));
                            replySocket.Send(vs.ToArray());
                            e.TcpClient.Client.Send(vs.ToArray());
                        }
                        else
                        {
                            System.Windows.MessageBox.Show(user + " has logged out!");
                        }
                    }
                    //if its a file
                    else
                    {
                        string bigName = e.MessageString.Substring(user.Length + 6, e.MessageString.Length - user.Length - 6);
                        int posOfStar = bigName.IndexOf('#');
                        string fileName = bigName.Substring(0, posOfStar);

                        byte[] array = new byte[(e.MessageString.Length - user.Length - fileName.Length - 7)];
                        Buffer.BlockCopy(e.Data, user.Length + fileName.Length + 7, array, 0, array.Length);
                        File.WriteAllBytes(currentPath + "\\" + user + "\\" + fileName, array);
                        String srcUsername = findUsername(e.TcpClient.Client, listOfClients);
                        Socket fromSock = findSocket(user, listOfClients);
                        if (fromSock != null)
                        {
                            List<byte> vs = new List<byte>();
                            vs.AddRange(Encoding.UTF8.GetBytes("[Private] " + srcUsername + ": Sent file " + fileName + "\n"));
                            List<byte> vs2 = new List<byte>();
                            vs2.AddRange(Encoding.UTF8.GetBytes("[Private] " + srcUsername + ": Sent file " + fileName + " | Location: " + currentPath + "\\" + user + "\\" + fileName + "\n"));
                            fromSock.Send(vs2.ToArray());
                            e.TcpClient.Client.Send(vs.ToArray());
                        }
                    }
                }
            }
            if (e.MessageString.Contains("closed"))
            {
                string username = e.MessageString.ToString().Split(':')[0];

                for (int i = 0; i < listOfClients.Count(); i++)
                {
                    if (listOfClients[i].username == username)
                        listOfClients.RemoveAt(i);
                }

                server.Broadcast("<<< " + username + " just left! >>>\n");//Chat log

                serverTextBox.Dispatcher.Invoke((Action)delegate ()
                {
                    serverTextBox.Text += "<<< " + username + " just left! >>>\n";//Server log
                });

            }
        }

        private Socket findSocket(string user, List<ClientDetails> list)
        {
            Socket userSocket;
            int counter = 0;

            foreach (ClientDetails details in list)
            {
                if (details.username.Equals(user)) break;
                counter++;
            }

            ClientDetails resSock = list.ElementAt(counter);
            userSocket = resSock.userSocket;

            return userSocket;
        }

        private string findUsername(Socket socket, List<ClientDetails> list)
        {
            string user;
            int counter = 0;

            foreach (ClientDetails details in list)
            {
                if (details.userSocket.Equals(socket)) break;
                counter++;
            }

            ClientDetails resUser = list.ElementAt(counter);
            user = resUser.username;

            return user;
        }

        private void startServerButtonClicked(object sender, RoutedEventArgs e)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            server.Start(ip, 1111);
            startServerButton.IsEnabled = false;
            serverTextBox.Text = "Server is running!\n";
        }

        private void stopServerButtonClicked(object sender, RoutedEventArgs e)
        {
            if (server.IsStarted)
            {
                server.Broadcast("<<< The server has crashed! Please close this window and try again later! >>>");
                server.Stop();
                startServerButton.IsEnabled = true;
                serverTextBox.Text = "Server stopped.\n";
            }
        }
    }
}
