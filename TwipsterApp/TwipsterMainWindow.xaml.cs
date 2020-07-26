using System.Linq;
using System.Windows;
using TwipsterApp.Models;

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

            using (var context = new TwipsterDbContext())
            {
                //Deleting current user and other users passwords and logins from array
                var usersCensored = context.Users.OrderBy(x => x.Name)
                                    .Where(x => x.Login != currentUser.Login)
                                    .Select(x => new { x.Name, x.Surname, x.BirthDate })
                                    .ToList();

                PutCurrentUserInformationToTextBoxt(currentUser);
                UsersGrid.ItemsSource = usersCensored;
                PostGridRefresh(context);
            }
        }

        private void OnCreatePostButtonClicked(object sender, RoutedEventArgs e)
        {
            new PostCreationWindow().Show();
        }

        private void OnLogOutButtonClicked(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            Close();
        }

        private void PutCurrentUserInformationToTextBoxt(User currentUser)
        {
            CurrentUserTextBlock.Text = $"{currentUser.Name} {currentUser.Surname} \nDate of birth: {currentUser.BirthDate.Date} \nLogin: {currentUser.Login}";
        }

        private void OnPostsRefreshButtonClicked(object sender, RoutedEventArgs e)
        {
            using (var context = new TwipsterDbContext())
            {
                PostGridRefresh(context);
            }
        }
        private void PostGridRefresh(TwipsterDbContext context)
        {
            //need to be parsed
            var postsList = context.Posts.OrderBy(x => x.PostTime)
                                .Select(x => new { x.User.Name, x.User.Surname, x.Content, x.PostTime })
                                .ToList();
            PostsGrid.ItemsSource = postsList;
        }
    }
}
