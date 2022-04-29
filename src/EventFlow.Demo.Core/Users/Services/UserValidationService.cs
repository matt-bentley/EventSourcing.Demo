using System.Net.Mail;

namespace EventFlow.Demo.Application.Users.Services
{
    public class UserValidationService : IUserValidationService
    {
        public bool IsValidEmail(string email)
        {
            bool valid = true;
            try
            {
                var mailUser = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }
            return valid;
        }
    }

    public interface IUserValidationService
    {
        bool IsValidEmail(string email);
    }
}
