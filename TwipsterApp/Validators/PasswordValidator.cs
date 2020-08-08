using System;
using TwipsterApp.Interfaces;

namespace TwipsterApp.Validators
{
    public class PasswordValidator : IValidator
    {
        private readonly string userPassword;
        private readonly string lineToValidateTo;

        public PasswordValidator(string userPassword, string lineToValidateTo)
        {
            this.userPassword = userPassword;
            this.lineToValidateTo = lineToValidateTo;
        }

        public void Validate()
        {
            if (userPassword != lineToValidateTo)
            {
                throw new Exception("Invalid password.");
            }
        }
    }
}
