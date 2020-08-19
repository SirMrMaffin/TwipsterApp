using Microsoft.EntityFrameworkCore;
using System;
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
            SelectedDialogueModel.SelectedDialogue = (AllDialoguesDatagrid.SelectedItem as DialogueVievModel);
            using var context = new TwipsterDbContext();
            var dialogueMessages = context.Messeges
                .Include(x => x.FromUser)
                .Include(x => x.ToUser)
                .Where(x => x.FromUser.Id == CurrentUserModel.CurrentUser.Id || x.ToUser.Id == (AllDialoguesDatagrid.SelectedItem as DialogueVievModel).ToUser.Id)
                .Select(x => new MessegeVievModel
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

            DialogueMessegesDatagrid.ItemsSource = dialogueMessages;
        }

        private void OnSendMessegeButtonClicked(object sender, RoutedEventArgs e)
        {
            User ToUserChecked;
            using var context = new TwipsterDbContext();
            if (SelectedDialogueModel.SelectedDialogue.ToUser.Id == CurrentUserModel.CurrentUser.Id)
            {
                ToUserChecked = context.Users.Single(x => x.Id == SelectedDialogueModel.SelectedDialogue.FromUser.Id);
            }
            else
            {
                throw new Exception("Error");
            }

            var newMessege = new Messege
            {
                FromUser = context.Users.Single(x => x.Id == CurrentUserModel.CurrentUser.Id),
                ToUser = ToUserChecked,
                Content = MessegeBodyTextBox.Text,
                DateTimeSend = DateTime.Now
            };

            try
            {
                context.Add(newMessege);
                context.SaveChanges();
            } catch (Exception x)
            {
                ExceptionHandlerService.Explain(x);
            }
        }

        private void OnNewDialogueButtonClicked(object sender, RoutedEventArgs e)
        {
            new CreateNewDialogueWindow(this).Show();
        }

        public void AllUserDialoguesRefresh()
        {
            var context = new TwipsterDbContext();
            var allUserDialogues = context.Messeges
                .Include(x => x.FromUser)
                .Include(x => x.ToUser)
                .Where(x => x.FromUser.Id == CurrentUserModel.CurrentUser.Id || x.ToUser.Id == CurrentUserModel.CurrentUser.Id)
                .OrderBy(x => x.DateTimeSend)
                .Select(x => new DialogueVievModel
                {
                    FromUserFullName = x.FromUser.Name + " " + x.FromUser.Surname,
                    ToUserFullName = x.ToUser.Name + " " + x.ToUser.Surname,
                    DateTimeSend = x.DateTimeSend,
                    FromUser = x.FromUser,
                    ToUser = x.ToUser,
                })
                .ToList();

            AllDialoguesDatagrid.ItemsSource = allUserDialogues;
        }
    }
}
