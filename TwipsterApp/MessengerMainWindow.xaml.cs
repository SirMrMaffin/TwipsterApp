using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for MessengerMainWindow.xaml
    /// </summary>
    public partial class MessengerMainWindow : Window
    {
        public MessengerMainWindow()
        {
            InitializeComponent();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            AllUserDialoguesRefresh();
        }

        private void OnCloseMessengerButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnOpenSelectedDialogueButtonClicked(object sender, RoutedEventArgs e)  
        {
            UpdateSelectedDialogueMessages();
        }

        private void OnSendMessageButtonClicked(object sender, RoutedEventArgs e)
        {
            using var context = new TwipsterDbContext();
            
            var newMessage = new Message
            {
                FromUser = context.Users.Single(x => x.Id == CurrentUserModel.CurrentUser.Id),
                ToUser = context.Users.Single(x => x.Id == (AllDialoguesDatagrid.SelectedItem as UserViewModel).Id),
                Content = MessageBodyTextBox.Text,
                DateTimeSend = DateTime.Now
            };

            try
            {
                context.Add(newMessage);
                context.SaveChanges();
                UpdateSelectedDialogueMessages();
                AllUserDialoguesRefresh();
                MessageBodyTextBox.Text = "";
            } catch (Exception x)
            {
                ExceptionHandlerService.Explain(x);
            }
        }

        private void OnNewDialogueButtonClicked(object sender, RoutedEventArgs e)
        {
            new CreateNewDialogueWindow(this).Show();
        }

        private void UpdateSelectedDialogueMessages()
        {
            var context = new TwipsterDbContext();
            var dialogueMessages = context.Messages
                .Include(x => x.FromUser)
                .Include(x => x.ToUser)
                .Where(x => x.FromUser.Id == CurrentUserModel.CurrentUser.Id && x.ToUser.Id == (AllDialoguesDatagrid.SelectedItem as UserViewModel).Id || x.ToUser.Id == CurrentUserModel.CurrentUser.Id && x.FromUser.Id == (AllDialoguesDatagrid.SelectedItem as UserViewModel).Id)
                .Select(x => new MessageViewModel
                {
                    FromUserId = x.FromUser.Id,
                    ToUserId = x.ToUser.Id,
                    DateTimeSend = x.DateTimeSend,
                    FromUserFullName = x.FromUser.Name + " " + x.FromUser.Surname,
                    ToUserFullName = x.ToUser.Name + " " + x.ToUser.Surname,
                    Content = x.Content
                })
                .OrderBy(x => x.DateTimeSend)
                .ToList();

            DialogueMessagesDatagrid.ItemsSource = dialogueMessages;
        }

        public void AllUserDialoguesRefresh()
        {
            var context = new TwipsterDbContext();
            var allDialoguesUsersIds = context.Messages
                .Include(x => x.FromUser)
                .Where(x => x.FromUser.Id == CurrentUserModel.CurrentUser.Id)
                .Select(x => x.ToUser.Id)
                .Union(context
                    .Messages
                    .Include(x => x.ToUser)
                    .Where(x => x.ToUser.Id == CurrentUserModel.CurrentUser.Id)
                    .Select(x => x.FromUser.Id))
                .Distinct()
                .ToList();

            var dialogUsers = new List<User>();

            foreach (var userId in allDialoguesUsersIds)
            {
                dialogUsers.Add(context.Users.Single(x => x.Id == userId));
            }

            AllDialoguesDatagrid.ItemsSource = dialogUsers.Select(x => new UserViewModel 
                {
                    Id = x.Id,
                    Login = x.Login,
                    Name = x.Name,
                    Surname = x.Surname,
                    BirthDate = x.BirthDate
                });
        }
    }
}
