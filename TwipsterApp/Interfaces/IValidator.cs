using TwipsterApp.ViewModels;

namespace TwipsterApp.Interfaces
{
    public interface IValidator
    {
        public void Validate(User user, string lineToValidateTo);
    }
}
