using System;
using System.Linq;
using System.Windows;
using TwipsterApp.Data;
using TwipsterApp.Models;

namespace TwipsterApp
{
    /// <summary>
    /// Interaction logic for UserEditingWindow.xaml
    /// </summary>
    public partial class UserEditingWindow : Window
    {
        public UserEditingWindow()
        {
            InitializeComponent();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoginTexBox.Text = CurrentUserModel.CurrentUser.Login;
            NameTexBox.Text = CurrentUserModel.CurrentUser.Name;
            SurnameTexBox.Text = CurrentUserModel.CurrentUser.Surname;
            DateOfBirthPicker.DisplayDate = CurrentUserModel.CurrentUser.BirthDate;
        }


        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            using var context = new TwipsterDbContext();
            try
            {
                var currentUserEntity = context.Users.Single(x => x.Login == CurrentUserModel.CurrentUser.Login);

                currentUserEntity.Login = LoginTexBox.Text;
                currentUserEntity.Name = NameTexBox.Text;
                currentUserEntity.Surname = SurnameTexBox.Text;
                currentUserEntity.BirthDate = DateOfBirthPicker.SelectedDate.Value;

                context.SaveChanges();
                PutCurrentUserInformationToTextBoxt(currentUserEntity);

                Close();
            } catch (Exception x)
            {
                MessageBox.Show(x.Message + "\n Check provided information.");
            }
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PutCurrentUserInformationToTextBoxt(User currentUserEntity)
        {
            var twipsterMainWindowOpened = Application.Current.Windows
                .Cast<Window>()
                .Single(window => window is TwipsterMainWindow) as TwipsterMainWindow;
            var userTextBlock = twipsterMainWindowOpened.CurrentUserTextBlock;

            CurrentUserModel.CurrentUser = currentUserEntity;
            userTextBlock.Text = $"{CurrentUserModel.CurrentUser.Name} {CurrentUserModel.CurrentUser.Surname} \nDate of birth: {CurrentUserModel.CurrentUser.BirthDate.Date} \nLogin: {CurrentUserModel.CurrentUser.Login}";
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            new PasswordChangeWindow().Show();
        }
    }
}
