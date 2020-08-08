using TwipsterApp.Models;

namespace TwipsterApp.Services
{
    public class UserVievModelServices
    {
        public User GetCurrentUser()
        {
            return CurrentUserModel.CurrentUser;
        }

        public string CurrentUserToString()
        {
            return $"{CurrentUserModel.CurrentUser.Name} {CurrentUserModel.CurrentUser.Surname} \nDate of birth: {CurrentUserModel.CurrentUser.BirthDate.Date} \nLogin: {CurrentUserModel.CurrentUser.Login}";
        }
    }
}
