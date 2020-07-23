using System;
using System.Windows;
using TwipsterApp.Models;

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
                    Password = PasswordTexBox.Text
                };

                try {
                    context.Add(user);
                    context.SaveChanges();

                    Close();
                } catch (Exception x) {
                    MessageBox.Show(x.Message + "\n Check provided information.");
                }
            }
        }
    }
}
