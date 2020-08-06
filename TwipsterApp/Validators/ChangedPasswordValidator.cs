using System;
using TwipsterApp.Interfaces;

namespace TwipsterApp.Validators
{
    public class ChangedPasswordValidator : IValidator
    {
        public void Validate(string firstLine, string secondLine)
        {
            if (firstLine != secondLine)
            {
                throw new Exception("New password repeat is incorrect.");
            }
        }
    }
}
