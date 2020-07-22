using System;

namespace TwipsterApp.Models
{
    class PasswordValidator : IValidator
    {
        public bool Validate(User user, string lineToValidate)
        {
            if (user.Password.Equals(lineToValidate))
            {
                return true;
            }
            else
            {
                throw new Exception("Invalid password");
            }
        }
    }
}
