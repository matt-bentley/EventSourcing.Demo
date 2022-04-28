
namespace EventFlow.Demo.Core.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(params string[] validationErrors) : base("Domain validation error")
        {
            ValidationErrors = validationErrors;
        }

        public readonly string[] ValidationErrors;
    }
}
