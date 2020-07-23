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
            new RegistrationWindow().Show();
        }

        private void OnLoginButtonClicked(object sender, RoutedEventArgs e)
        {
            using (var context = new TwipsterDbContext())
            try
            {
                CurrentUserModel.currentUser = (context.Users.Single(x => x.Login == LoginTexBox.Text));
                var passwordValidator = new PasswordValidator();
                passwordValidator.Validate(CurrentUserModel.currentUser, PasswordTexBox.Text);
            } catch (Exception x) 
            {
                MessageBox.Show(x.Message);
            }

            new TwipsterMainWindow().Show();
            Close();
        }
    }
}
