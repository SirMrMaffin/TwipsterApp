using System;
using System.Linq;
using System.Windows;
using TwipsterApp.Data;
using TwipsterApp.Models;
using TwipsterApp.Validators;

namespace TwipsterApp
{
    /// <summary>
    /// Interaction logic for PasswordChangeWindow.xaml
    /// </summary>
    public partial class PasswordChangeWindow : Window
    {
        public PasswordChangeWindow()
        {
            InitializeComponent();
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            using var context = new TwipsterDbContext();
            try
            {
                new PasswordValidator().Validate(CurrentUserModel.CurrentUser.Password, OldPasswordTextBox.Text);
                new ChangedPasswordValidator().Validate(NewPasswordFirstTextBox.Text, NewPasswordSecondTextBox.Text);

                var currentUserEntity = context.Users.Single(x => x.Login == CurrentUserModel.CurrentUser.Login);
                currentUserEntity.Password = NewPasswordFirstTextBox.Text;
                context.SaveChanges();

                Close();
            } catch (Exception x)
            {
                MessageBox.Show(x.Message + "\n Check provided information.");
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
