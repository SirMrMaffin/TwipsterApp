using System;
using System.Windows;
using TwipsterApp.Data;
using TwipsterApp.Models;
using TwipsterApp.Services;

namespace TwipsterApp
{
    /// <summary>
    /// Interaction logic for PostCreationWindow.xaml
    /// </summary>
    public partial class PostCreationWindow : Window
    {
        private readonly Action callback;

        private PostCreationWindow()
        {
            InitializeComponent();
        }

        public PostCreationWindow(Action callback) : this()
        {
            this.callback = callback;
        }

        private void OnCreatePostButtonClicked(object sender, RoutedEventArgs e)
        {
            using var context = new TwipsterDbContext();
            var post = new Post
            {
                UserId = CurrentUserModel.CurrentUser.Id,
                PostTime = DateTime.Now,
                Content = PostContentTextBox.Text
            };

            try
            {
                context.Add(post);
                context.SaveChanges();

                callback?.Invoke();

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
    }
}
