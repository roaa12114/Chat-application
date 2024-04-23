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

namespace AllChatClient
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
            MessageTextBox.Visibility = Visibility.Hidden;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void loginButtonClicked(object sender, RoutedEventArgs e)
        {
            MessageTextBox.Visibility = Visibility.Visible;
            MessageTextBox.Foreground = Brushes.Red;

            if (UsernameTextBox.Text.Equals(""))
            {
                MessageTextBox.Text = "UserName Can't be empty! Please try again!";
                return;
            }

            if (PasswordTextBox.Password.Equals(""))
            {
                MessageTextBox.Text = "Password Can't be empty! Please try again!";
                return;
            }

            UserAuthentication userAuthentication = new UserAuthentication();
            userAuthentication.username = UsernameTextBox.Text;
            userAuthentication.password = PasswordTextBox.Password;

            if (UserAuthenticationManager.loginUserExists(userAuthentication))
            {
                MessageTextBox.Foreground = Brushes.Green;
                MessageTextBox.Text = "You've logged in successfully!";

                //Server server = new Server();
                //server.Show();

                Client client = new Client(UsernameTextBox.Text);
                Application.Current.MainWindow.Content = client;
            }
            else
            {
                MessageTextBox.Text = "Incorrect Username or Password!";
            }
        }

        private void registerButtonClicked(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            Application.Current.MainWindow.Content = register;
        }

        private void passwordTextBoxClicked(object sender, MouseButtonEventArgs e)
        {
            PasswordTextBox.Password = "";
        }

        private void usernameTextBoxClicked(object sender, MouseButtonEventArgs e)
        {
            UsernameTextBox.Text = "";
        }

        

       
    }
}
