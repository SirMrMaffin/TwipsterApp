using System;
using System.Linq;
using System.Windows;
using TwipsterApp.Data;
using TwipsterApp.Models;
using TwipsterApp.Services;
using TwipsterApp.Validators;

namespace TwipsterApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserVievModelServices userModelServices = new UserVievModelServices(); 
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
            using var context = new TwipsterDbContext();
            try
            {
                CurrentUserModel.CurrentUser = context.Users.Single(x => x.Login == LoginTexBox.Text);
                new PasswordValidator(userModelServices.GetCurrentUser().Password, PasswordPasswordBox.Password).Validate();
                new TwipsterMainWindow().Show();
                Close();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
    }
}
