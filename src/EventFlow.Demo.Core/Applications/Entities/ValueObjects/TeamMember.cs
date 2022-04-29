using EventFlow.Demo.Core.Exceptions;
using EventFlow.ValueObjects;

namespace EventFlow.Demo.Core.Applications.Entities.ValueObjects
{
    public class TeamMember : ValueObject
    {
        public Guid UserId { get; private set; }
        public string Email { get; private set; }

        public TeamMember(Guid userId, string email)
        {
            email = email.Trim();
            if (string.IsNullOrEmpty(email))
            {
                throw new DomainException("Email is required");
            }

            UserId = userId;
            Email = email.ToLower();
        }

        public TeamMember UpdateEmail(string email)
        {
            return new TeamMember(UserId, email);
        }
    }
}
