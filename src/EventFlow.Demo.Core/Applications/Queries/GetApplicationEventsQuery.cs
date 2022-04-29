using EventFlow.Demo.Core.Abstractions.ReadModels;
using EventFlow.Demo.Core.Abstractions.Repositories;
using EventFlow.Demo.Core.Applications.Entities;
using EventFlow.Queries;

namespace EventFlow.Demo.Core.Applications.Queries
{
    public record GetApplicationEventsQuery(Guid Id) : IQuery<List<CommittedDomainEvent>>;

    public class GetApplicationEventsQueryHandler : IQueryHandler<GetApplicationEventsQuery, List<CommittedDomainEvent>>
    {
        private IEventsRepository _repository;

        public GetApplicationEventsQueryHandler(IEventsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CommittedDomainEvent>> ExecuteQueryAsync(GetApplicationEventsQuery query, CancellationToken cancellationToken)
        {
            return await _repository.GetAsync<ApplicationEnvironment>(query.Id);
        }
    }
}
