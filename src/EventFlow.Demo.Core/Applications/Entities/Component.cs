using EventFlow.Entities;

namespace EventFlow.Demo.Core.Applications.Entities
{
    public class Component : Entity<Id>
    {
        public Component(Id id, string name, string url) : base(id) 
        { 
            Name = name;
            Url = url;
        }

        public string Name { get; private set; }
        public string Url { get; private set; }
    }
}
