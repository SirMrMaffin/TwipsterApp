using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TwipsterApp.Data;
using TwipsterApp.Interfaces;
using TwipsterApp.Models;
using TwipsterApp.Services;
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

        private void OnChangePasswordButtonClicked(object sender, RoutedEventArgs e)
        {
            var validatorsList = new List<IValidator> 
            {
                new TwoLinesValidator(CurrentUserModel.CurrentUser.Password, OldPasswordPasswordBox.Password, "Invalid password."),
                new TwoLinesValidator(NewPasswordPasswordBox.Password, RepeatNewPasswordPasswordBox.Password, "New password repeat is incorrect."),
            };

            using var context = new TwipsterDbContext();
            try
            {
                foreach(var validator in validatorsList)
                {
                    validator.Validate();
                };

                var currentUserEntity = context.Users.Single(x => x.Login == CurrentUserModel.CurrentUser.Login);
                currentUserEntity.Password = NewPasswordPasswordBox.Password;
                context.SaveChanges();

                Close();
            } catch (Exception x)
            {
                ExceptionHandlerService.Explain(x);
            }
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
