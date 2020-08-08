using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TwipsterApp.Data;
using TwipsterApp.Interfaces;
using TwipsterApp.Services;
using TwipsterApp.Validators;

namespace TwipsterApp
{
    /// <summary>
    /// Interaction logic for PasswordChangeWindow.xaml
    /// </summary>
    public partial class PasswordChangeWindow : Window
    {
        private UserVievModelServices userModelServices = new UserVievModelServices();
        public PasswordChangeWindow()
        {
            InitializeComponent();
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var validatorsList = new List<IValidator> 
            {
                new PasswordValidator(userModelServices.GetCurrentUser().Password, OldPasswordPasswordBox.Password),
                new ChangedPasswordValidator(NewPasswordPasswordBox.Password, RepeatNewPasswordPasswordBox.Password),
            };

            using var context = new TwipsterDbContext();
            try
            {
                foreach(var validator in validatorsList)
                {
                    validator.Validate();
                };

                var currentUserEntity = context.Users.Single(x => x.Login == userModelServices.GetCurrentUser().Login);
                currentUserEntity.Password = NewPasswordPasswordBox.Password;
                context.SaveChanges();

                Close();
            } catch (Exception x)
            {
                new ExceptionHandlerService().Explain(x);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
