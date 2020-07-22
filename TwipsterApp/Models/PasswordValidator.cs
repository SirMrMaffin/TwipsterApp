namespace TwipsterApp.Models
{
    class PasswordValidator : IValidate
    {
        public bool Validate(User user, string lineToValidate)
        {
            if (user.Password.Equals(lineToValidate))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
