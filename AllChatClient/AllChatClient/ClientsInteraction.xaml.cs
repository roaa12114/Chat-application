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
using SimpleTCP;
using System.IO;
using Microsoft.Win32;

namespace AllChatClient
{
   
    public partial class ClientsInteraction : Page
    {
        private string username;
        Boolean ifFileSelected = false;
        FileInfo fileInfo;
        SimpleTcpClient client;
        OpenFileDialog openFileDialog;


        public ClientsInteraction(string clientName,SimpleTcpClient client, string ipAddress)
        {

            InitializeComponent();

            this.username = clientName;
            this.client = client;
            this.client.DataReceived += ClientDataReceived;

            string currentPath = "C:\\Pranay\\Projects\\C#Projects\\AllChat\\AllChatFiles";
            if (!Directory.Exists(currentPath + "/" + username))
            {
                Directory.CreateDirectory(currentPath + "/" + username);
            }

            usernameTextBlock.Text += username;
        }


        private void ClientDataReceived(object sender, SimpleTCP.Message e)
        {
            if (e.MessageString.Length > 4 && e.MessageString.Contains("list"))
            {
                String msg = e.MessageString.Substring(4, e.MessageString.Length - 4);
                string[] listClients = new string[msg.Split(':').Length - 2];
                int count = 0;
                for (int i = 0; i < msg.Split(':').Length; i++)
                {
                    if (msg.Split(':')[i] != "" && msg.Split(':')[i] != this.username)
                    {
                        listClients[count] = msg.Split(':')[i];
                        count++;
                    }
                }
                onlineUsersListBox.Items.Dispatcher.Invoke((Action)delegate ()
                {
                    onlineUsersListBox.ItemsSource = listClients;
                });
            }
            else
            {
                messageTextBox.Dispatcher.Invoke((Action)delegate ()
                {
                    if (e.MessageString.Contains("just joined") && !e.MessageString.Contains(username))
                    {
                        messageTextBox.Text += e.MessageString;
                    }
                    else if (e.MessageString.Contains("just joined") && e.MessageString.Contains(username))
                    {
                        messageTextBox.Text += "";
                    }
                    else
                    {
                        messageTextBox.Text += e.MessageString;
                    }
                });

            }

            if (e.MessageString.Equals("All"))
            {
                messageTextBox.Dispatcher.Invoke((Action)delegate ()
                {
                    messageTextBox.Text += "";
                });

            }
        }

        private void sendButtonClicked(object sender, RoutedEventArgs e)
        {
            if (onlineUsersListBox.SelectedIndex != -1)
            {
                if (writeMessageTextBox.Text.Length > 0 && !ifFileSelected && !writeMessageTextBox.Text.Equals(" ") && !writeMessageTextBox.Text.Equals("\n"))
                {
                    client.WriteLine(onlineUsersListBox.SelectedItem.ToString() + "#" + username + ": " + writeMessageTextBox.Text);
                }
                else if (ifFileSelected)
                {
                    byte[] b = File.ReadAllBytes(openFileDialog.FileName);
                    client.Write(onlineUsersListBox.SelectedItem.ToString() + "#File#" + fileInfo.Name + "#");
                    client.Write(b);
                    ifFileSelected = false;
                }
                else
                {
                    System.Windows.MessageBox.Show("Please type something or choose a file to send!");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please select someone to send the message to!");
            }

            writeMessageTextBox.Text = "";
        }

        private void addFileButtonClicked(object sender, RoutedEventArgs e)
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
                fileInfo = new FileInfo(openFileDialog.FileName);
                ifFileSelected = true;
                writeMessageTextBox.Text = "Sending " + fileInfo.Name;
        }

        private void logoutButtonClicked(object sender, RoutedEventArgs e)
        {
            client.Write(username + ":closed");
            client.TcpClient.Close();
            Login login = new Login();
            Application.Current.MainWindow.Content = login;
        }

        public void writeMessageClicked(object sender, MouseButtonEventArgs e)
        {
            writeMessageTextBox.Text = "";
            client.WriteLine("list");
        }
    }
}
