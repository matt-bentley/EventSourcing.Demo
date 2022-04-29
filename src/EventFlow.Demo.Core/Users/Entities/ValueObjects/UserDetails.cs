using EventFlow.ValueObjects;

namespace EventFlow.Demo.Core.Users.Entities.ValueObjects
{
    public class UserDetails : ValueObject
    {
        public UserDetails(string firstName, string lastName)
        {
            firstName = firstName.Trim();
            lastName = lastName.Trim();

            if (string.IsNullOrEmpty(firstName))
            {
                throw new InvalidOperationException($"First Name is required");
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new InvalidOperationException($"Last Name is required");
            }

            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}
