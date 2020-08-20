using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TwipsterApp.Data;
using TwipsterApp.Models;
using TwipsterApp.Services;
using TwipsterApp.ViewModels;

namespace TwipsterApp
{
    /// <summary>
    /// Interaction logic for CreateNewDialogueWindow.xaml
    /// </summary>
    public partial class CreateNewDialogueWindow : Window
    {
        private readonly MessengerMainWindow mainWindow;

        public CreateNewDialogueWindow()
        {
            InitializeComponent();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            RefreshUsersListDataGrid();
        }

        public CreateNewDialogueWindow(MessengerMainWindow mainWindow) : this()
        {
            this.mainWindow = mainWindow;
        }

        private void OnSearchButtonClicked(object sender, RoutedEventArgs e)
        {
            var searchedUsers = new List<UserViewModel>();

            foreach (var item in UsersListDataGrid.ItemsSource)
            {
                if ((item as UserViewModel).Name  == SearchTextBox.Text || (item as UserViewModel).Surname == SearchTextBox.Text)
                {
                    searchedUsers.Add(item as UserViewModel);
                }
            }
            UsersListDataGrid.ItemsSource = searchedUsers.OrderBy(x => x.Name);
        }

        private void OnRefreshUsersListButtonClicked(object sender, RoutedEventArgs e)
        {
            RefreshUsersListDataGrid();
        }

        private void RefreshUsersListDataGrid()
        {
            var context = new TwipsterDbContext();

            var usersCensored = context.Users.OrderBy(x => x.Name)
                                .Where(x => x.Login != CurrentUserModel.CurrentUser.Login)
                                .Select(x => new UserViewModel
                                {
                                    Id = x.Id,
                                    Login = x.Login,
                                    Name = x.Name,
                                    Surname = x.Surname,
                                    BirthDate = x.BirthDate
                                })
                                .ToList();

            UsersListDataGrid.ItemsSource = usersCensored;
        }

        private void OnSendMessageButtonClicked(object sender, RoutedEventArgs e)
        {
            var context = new TwipsterDbContext();

            var newMessage = new Message
            {
                FromUser = context.Users.Single(x => x.Id == CurrentUserModel.CurrentUser.Id),
                ToUser = context.Users.Single(x => x.Id == (UsersListDataGrid.SelectedItem as UserViewModel).Id),
                Content = MessageBodyTextBox.Text,
                DateTimeSend = DateTime.Now
            };

            try
            {
                context.Add(newMessage);
                context.SaveChanges();
                mainWindow.AllUserDialoguesRefresh();
                Close();
            }
            catch (Exception x)
            {
                ExceptionHandlerService.Explain(x);
            }
        }
    }
}
