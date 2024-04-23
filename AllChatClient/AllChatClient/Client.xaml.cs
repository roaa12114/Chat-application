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
   
    public partial class Client : Page
    {
        private string username;
        SimpleTcpClient client;
        public Client(string username)
        {
            InitializeComponent();
            MessageTextBox.Visibility = Visibility.Hidden;
            this.username = username;
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void connectButtonClicked(object sender, RoutedEventArgs e)
        {
            MessageTextBox.Visibility = Visibility.Visible;
            MessageTextBox.Foreground = Brushes.Red;

            if (IPAddressTextBox.Text.Equals(""))
            {
                MessageTextBox.Text = "IP address cannot be empty!";
                return;
            }

            if (IPAddressTextBox.Text.Length > 15)
            {
                MessageTextBox.Text = "You have entered incorrect IP address, Please try again!";
                return;
            }

            string[] ipParts = IPAddressTextBox.Text.Split(".");
            if (ipParts.Length != 4)
            {
                MessageTextBox.Text = "You have entered incorrect IP address, Please try again!";
                return;
            }

            MessageTextBox.Foreground = Brushes.Green;
            MessageTextBox.Text = "Logging In!";

            try
            {
                client.Connect(IPAddressTextBox.Text, 1111);
                client.Write("<<< " + username + " just joined! >>>\n");

                ClientsInteraction clientsInteraction = new ClientsInteraction(username, client, IPAddressTextBox.Text);
                Application.Current.MainWindow.Content = clientsInteraction;
            }
            catch (Exception ex) 
            {
                System.Windows.MessageBox.Show("Server is not active");
                
            }
            
            
        }

        private void logoutButtonClicked(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            Application.Current.MainWindow.Content = login;
        }

        private void IPAddressTextBoxClicked(object sender, MouseButtonEventArgs e)
        {
            IPAddressTextBox.Text = "";
        }

    }
}
