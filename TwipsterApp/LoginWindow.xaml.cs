using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            new RegistrationWindow().Show();
        }

        private void OnLoginButtonClicked(object sender, RoutedEventArgs e)
        {
            var passwordValidator = new PasswordValidator();

            using (var context = new TwipsterDbContext())
            try {
                var currentUser = new CurrentUserModel(context.Users.Single(x => x.Login == LoginTexBox.Text));
                passwordValidator.Validate(CurrentUserModel.currentUser, PasswordTexBox.Text);
            } catch (Exception x) {
                MessageBox.Show(x.Message);
            }

            var twipsterMainWindow = new TwipsterMainWindow();
            twipsterMainWindow.Show();
            Close();
        }
    }
}
