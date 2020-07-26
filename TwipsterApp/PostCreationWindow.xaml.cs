using System;
using System.Windows;
using TwipsterApp.Models;

namespace TwipsterApp
{
    /// <summary>
    /// Interaction logic for PostCreationWindow.xaml
    /// </summary>
    public partial class PostCreationWindow : Window
    {
        public PostCreationWindow()
        {
            InitializeComponent();
        }

        private void OnCreatePostButtonClicked(object sender, RoutedEventArgs e)
        {
            using (var context = new TwipsterDbContext())
            {
                var post = new Post
                {
                    UserId = CurrentUserModel.currentUser.Id,
                    PostTime = DateTime.Now,
                    Content = PostContentTextBox.Text
                };

                try
                {
                    context.Add(post);
                    context.SaveChanges();
                    
                    Close();
                } catch (Exception x)
                {
                    MessageBox.Show(x.Message + "\n Check provided information.");
                }
            }
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
