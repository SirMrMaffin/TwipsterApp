using System;
using TwipsterApp.Interfaces;

namespace TwipsterApp.Validators
{
    public class ChangedPasswordValidator : IValidator
    {
        private readonly string firstLine;
        private readonly string secondLine;

        public ChangedPasswordValidator(string firstLine, string secondLine)
        {
            this.firstLine = firstLine;
            this.secondLine = secondLine;
        }
        public void Validate()
        {
            if (firstLine != secondLine)
            {
                throw new Exception("New password repeat is incorrect.");
            }
        }
    }
}
