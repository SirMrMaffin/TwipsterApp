using System;

namespace TwipsterApp.Models
{
    public class PasswordValidator : IValidator
    {
        public void Validate(User user, string lineToValidateTo)
        {
            if (user.Password != lineToValidateTo)
            {
                throw new Exception("Invalid password.");
            }
        }
    }
}
