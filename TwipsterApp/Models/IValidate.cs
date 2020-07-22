using TwipsterApp.Models;

namespace TwipsterApp
{
    public interface IValidate
    {
        public bool Validate(User user, string lineToValidate);
    }
}
