using TwipsterApp.Models;

namespace TwipsterApp
{
    public interface IValidator
    {
        public void Validate(User user, string lineToValidateTo);
    }
}
