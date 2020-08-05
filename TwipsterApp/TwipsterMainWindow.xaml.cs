using System.Linq;
using System.Windows;
using TwipsterApp.Data;
using TwipsterApp.Models;
using TwipsterApp.ViewModels;

namespace TwipsterApp
{
    /// <summary>
    /// Interaction logic for TwipsterMainWindow.xaml
    /// </summary>
    public partial class TwipsterMainWindow : Window
    {
        public TwipsterMainWindow()
        {
            InitializeComponent();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            var currentUser = CurrentUserModel.currentUser;

            using var context = new TwipsterDbContext();
            
            //Deleting current user and other users passwords and logins from array
            var usersCensored = context.Users.OrderBy(x => x.Name)
                                .Where(x => x.Login != currentUser.Login)
                                .Select(x => new SelectedUserModel 
                                {
                                    Login = x.Login,
                                    Name = x.Name,
                                    Surname = x.Surname,
                                    BirthDate = x.BirthDate
                                })
                                .ToList();

            PutCurrentUserInformationToTextBoxt(currentUser);
            UsersGrid.ItemsSource = usersCensored;
            PostGridRefresh(context);
        }

        private void OnCreatePostButtonClicked(object sender, RoutedEventArgs e)
        {
            new PostCreationWindow(() => 
            {
                OnShowAllPostsButtonClicked(null, null);
            }).Show();
        }

        private void OnLogOutButtonClicked(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            Close();
        }

        private void OnShowAllPostsButtonClicked(object sender, RoutedEventArgs e)
        {
            using var context = new TwipsterDbContext();
            PostGridRefresh(context);
        }

        private void OnShowSelectedUserPostsClicked(object sender, RoutedEventArgs e)
        {
            using var context = new TwipsterDbContext();
            var selectedUser = context.Users.Single(x => x.Login == (UsersGrid.SelectedItem as SelectedUserModel).Login);

            var userPostsList = context.Posts.Where(x => x.UserId == selectedUser.Id)
                                        .Select(x => new PostViewModel 
                                        {
                                            Name = x.User.Name,
                                            Surname = x.User.Surname,
                                            Content = x.Content,
                                            PostTime = x.PostTime
                                        })
                                        .ToList();
            PostsGrid.ItemsSource = userPostsList;
        }

        private void PutCurrentUserInformationToTextBoxt(User currentUser)
        {
            CurrentUserTextBlock.Text = $"{currentUser.Name} {currentUser.Surname} \nDate of birth: {currentUser.BirthDate.Date} \nLogin: {currentUser.Login}";
        }

        public void PostGridRefresh(TwipsterDbContext context)
        {
            var postsList = context.Posts.OrderBy(x => x.PostTime).ToList();
            PostsGrid.ItemsSource = postsList;
        }

    }
}
