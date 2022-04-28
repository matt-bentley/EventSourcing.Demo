using EventFlow.Demo.Core.Abstractions.ReadModels;

namespace EventFlow.Demo.Core.Applications.ReadModels
{
    public class ComponentReadModel : ReadModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string ApplicationId { get; set; }
    }
}
