using EventFlow.ReadStores;

namespace EventFlow.Demo.Core.Abstractions.ReadModels
{
    public abstract class ReadModel : IReadModel
    {
        public string Id { get; set; }
    }
}
