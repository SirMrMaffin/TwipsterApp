using System;

namespace TwipsterApp.Models
{
    public class CurrentUserModel
    {
        public static User currentUser;
        
        public CurrentUserModel(User user)
        {
            currentUser = user;
        }
    }
}
