using EventFlow.Core;

namespace EventFlow.Demo.Core
{
    public class Id : Identity<Id>
    {
        public Id(string value) : base(value)
        {
        }

        public static implicit operator Guid(Id identity) => new Guid(identity.Value);
        public static implicit operator Id(Guid id) => new Id(id.ToString());
    }
}
