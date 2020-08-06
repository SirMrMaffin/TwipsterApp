using System;
using TwipsterApp.Interfaces;

namespace TwipsterApp.Validators
{
    public class PasswordValidator : IValidator
    {
        public void Validate(string userPassword, string lineToValidateTo)
        {
            if (userPassword != lineToValidateTo)
            {
                throw new Exception("Invalid password.");
            }
        }
    }
}
