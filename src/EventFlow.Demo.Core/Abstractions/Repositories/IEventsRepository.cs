using EventFlow.Aggregates;
using EventFlow.Demo.Core.Abstractions.ReadModels;

namespace EventFlow.Demo.Core.Abstractions.Repositories
{
    public interface IEventsRepository
    {
        Task<List<CommittedDomainEvent>> GetAsync<TAggregate>(Id id) where TAggregate : IAggregateRoot;
    }
}
