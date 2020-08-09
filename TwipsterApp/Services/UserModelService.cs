using TwipsterApp.Models;

namespace TwipsterApp.Services
{
    public class UserModelService
    {
        public string CurrentUserToString()
        {
            return $"{CurrentUserModel.CurrentUser.Name} {CurrentUserModel.CurrentUser.Surname} \nDate of birth: {CurrentUserModel.CurrentUser.BirthDate.Date} \nLogin: {CurrentUserModel.CurrentUser.Login}";
        }
    }
}
