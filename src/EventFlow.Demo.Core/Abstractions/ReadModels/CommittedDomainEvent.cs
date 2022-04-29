using EventFlow.EventStores;

namespace EventFlow.Demo.Core.Abstractions.ReadModels
{
    public record CommittedDomainEvent(string AggregateId, string Data, string Metadata, int AggregateSequenceNumber) : ICommittedDomainEvent;
}
