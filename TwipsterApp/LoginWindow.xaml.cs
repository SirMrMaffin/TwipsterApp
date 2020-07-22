using System;
using System.Linq;
using System.Windows;
using TwipsterApp.Models;

namespace TwipsterApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
        }

        private void OnLoginButtonClicked(object sender, RoutedEventArgs e)
        {
            var user = new User();
            var validator = new PasswordValidator();

            try {
                using (var context = new TwipsterDbContext())
                {
                    user = context.Users.Single(x => x.Login == LoginTexBox.Text);
                }
                if (validator.Validate(user, PasswordTexBox.Text) == true)
                {
                    MessageBox.Show("kek");
                } else {
                    throw new Exception("Invalid password");
                }
            } catch (Exception x) {
                MessageBox.Show(x.Message);
            }
        }
    }
}
