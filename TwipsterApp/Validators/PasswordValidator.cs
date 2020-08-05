using System;
using TwipsterApp.Interfaces;
using TwipsterApp.Models;

namespace TwipsterApp.Validators
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
