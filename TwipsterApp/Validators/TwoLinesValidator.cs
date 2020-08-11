using System;
using System.Windows.Input;
using TwipsterApp.Interfaces;

namespace TwipsterApp.Validators
{
    public class TwoLinesValidator : IValidator
    {
        private readonly string firstLine;
        private readonly string secondLine;
        private readonly Exception exception;

        public TwoLinesValidator(string firstLine, string secondLine, string exceptionMessage)
        {
            this.firstLine = firstLine;
            this.secondLine = secondLine;
            exception = new Exception(exceptionMessage);
        }

        public void Validate()
        {
            if (firstLine != secondLine)
            {
                throw exception;
            }
        }
    }
}
