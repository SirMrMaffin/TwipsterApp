using System;
using System.Linq;
using System.Windows;
using TwipsterApp.Data;
using TwipsterApp.Models;
using TwipsterApp.Services;

namespace TwipsterApp
{
    /// <summary>
    /// Interaction logic for UserEditingWindow.xaml
    /// </summary>
    public partial class UserEditingWindow : Window
    {
        private readonly TwipsterMainWindow mainWindow;
        private UserModelService userModelServices = new UserModelService();
        public UserEditingWindow()
        {
            InitializeComponent();
        }

        public UserEditingWindow(TwipsterMainWindow mainWindow) : this()
        {
            this.mainWindow = mainWindow;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoginTexBox.Text = CurrentUserModel.CurrentUser.Login;
            NameTexBox.Text = CurrentUserModel.CurrentUser.Name;
            SurnameTexBox.Text = CurrentUserModel.CurrentUser.Surname;
            DateOfBirthPicker.SelectedDate = CurrentUserModel.CurrentUser.BirthDate;
        }

        private void OnSaveChangesButtonClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                using var context = new TwipsterDbContext();
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
                new ExceptionHandlerService().Explain(x);
            }
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PutCurrentUserInformationToTextBoxt(User currentUserEntity)
        {
            CurrentUserModel.CurrentUser = currentUserEntity;
            mainWindow.CurrentUserTextBlock.Text = userModelServices.CurrentUserToString();
        }

        private void OnChangePasswordButtonClicked(object sender, RoutedEventArgs e)
        {
            new PasswordChangeWindow().Show();
        }
    }
}
