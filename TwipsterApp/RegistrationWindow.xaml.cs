using System;
using System.Windows;
using TwipsterApp.Data;
using TwipsterApp.Models;
using TwipsterApp.Services;

namespace TwipsterApp
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void OnRegisterButtonClicked(object sender, RoutedEventArgs e)
        {
            using (var context = new TwipsterDbContext())
            {
                var user = new User
                {
                    Login = LoginTexBox.Text,
                    Name = NameTexBox.Text,
                    Surname = SurnameTexBox.Text,
                    BirthDate = DateOfBirthPicker.SelectedDate.Value,
                    Password = PasswordPasswordBox.Password
                };

                try {
                    context.Add(user);
                    context.SaveChanges();

                    Close();
                } catch (Exception x) 
                {
                    new ExceptionHandlerService().Explain(x);
                }
            }
        }
    }
}
