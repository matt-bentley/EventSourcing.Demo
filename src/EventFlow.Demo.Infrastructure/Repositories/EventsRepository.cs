using EventFlow.Aggregates;
using EventFlow.Demo.Core;
using EventFlow.Demo.Core.Abstractions.ReadModels;
using EventFlow.Demo.Core.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlow.Demo.Infrastructure.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private DemoContext _context;

        public EventsRepository(DemoContext context)
        {
            _context = context;
        }

        public async Task<List<CommittedDomainEvent>> GetAsync<T>(Id id) where T : IAggregateRoot
        {
            var events = await _context.EventEntity
                                       .Where(e => e.AggregateName == typeof(T).Name && e.AggregateId == id.Value)
                                       .Select(e => new CommittedDomainEvent(e.AggregateId, e.Data, e.Metadata, e.AggregateSequenceNumber))           
                                       .ToListAsync();
            return events;
        }
    }
}
