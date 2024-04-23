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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
            MessageTextBox.Visibility = Visibility.Hidden;
        }

        private void registerButtonClicked(object sender, RoutedEventArgs e)
        {
            MessageTextBox.Visibility = Visibility.Visible;
            MessageTextBox.Foreground = Brushes.Red;
            if (UsernameTextBox.Text.Equals(""))
            {
                MessageTextBox.Text = "UserName Can't be empty! Please try again!";
                return;
            }

            if (UsernameTextBox.Text.Contains(' '))
            {
                MessageTextBox.Text = "UserName should have no spaces! Please try again!";
                return;
            }

            if (PasswordTextBox.Password.Equals(""))
            {
                MessageTextBox.Text = "Password Can't be empty! Please try again!";
                return;
            }

            if (PasswordTextBox.Password.Contains(' '))
            {
                MessageTextBox.Text = "Password should have no spaces! Please try again!";
                return;
            }

            if (ReTypePasswordTextBox.Password.Equals(""))
            {
                MessageTextBox.Text = "Re-Typed Password Can't be empty! Please try again!";
                return;
            }

            if (PasswordTextBox.Password.Length < 6)
            {
                MessageTextBox.Text = "Password should be of min 6 characters!";
                return;
            }

            if (!PasswordTextBox.Password.Equals(ReTypePasswordTextBox.Password))
            {
                MessageTextBox.Text = "Passwords do not match! Please try again!";
                return;
            }

            UserAuthentication userAuthentication = new UserAuthentication();
            userAuthentication.username = UsernameTextBox.Text;
            userAuthentication.password = PasswordTextBox.Password;

            if (UserAuthenticationManager.registerUserExists(userAuthentication))
            {
                MessageTextBox.Text = "This Username already exists!";
                return;
            }

            UserAuthenticationManager.persist(userAuthentication);

            MessageTextBox.Foreground = Brushes.Green;
            MessageTextBox.Text = "User Registered Successfully!";
        }

        private void resetButtonClicked(object sender, RoutedEventArgs e)
        {
            UsernameTextBox.Text = "";
            PasswordTextBox.Password = "";
            ReTypePasswordTextBox.Password = "";
            MessageTextBox.Visibility = Visibility.Hidden;
        }

        private void backButtonClicked(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            Application.Current.MainWindow.Content = login;
        }

        private void usernameTextBoxClicked(object sender, MouseButtonEventArgs e)
        {
            UsernameTextBox.Text = "";
        }

        private void passwordTextBoxClicked(object sender, MouseButtonEventArgs e)
        {
            PasswordTextBox.Password = "";
        }

        private void reTypePasswordTextBoxClicked(object sender, MouseButtonEventArgs e)
        {
            ReTypePasswordTextBox.Password = "";
        }
    }
}
