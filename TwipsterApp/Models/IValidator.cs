using TwipsterApp.Models;

namespace TwipsterApp
{
    public interface IValidator
    {
        public bool Validate(User user, string lineToValidate);
    }
}
