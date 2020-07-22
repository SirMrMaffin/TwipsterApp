using System;
using System.Collections.Generic;
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
        public static User currentUser;
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
            //var user = new User();
            var validator = new PasswordValidator();
            var twipsterMainWindow = new TwipsterMainWindow();

            var validators = new List<IValidator>
            {
                new PasswordValidator()
            };

            try {
                using (var context = new TwipsterDbContext())
                {
                    currentUser = context.Users.Single(x => x.Login == LoginTexBox.Text);
                }
                validators[0].Validate(currentUser, PasswordTexBox.Text);
                twipsterMainWindow.Show();
                Close();
            } catch (Exception x) {
                MessageBox.Show(x.Message);
            }
        }
    }
}
