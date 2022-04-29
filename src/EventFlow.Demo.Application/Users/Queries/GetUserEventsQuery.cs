using EventFlow.Demo.Core.Abstractions.ReadModels;
using EventFlow.Demo.Core.Abstractions.Repositories;
using EventFlow.Demo.Core.Users.Entities;
using EventFlow.Queries;

namespace EventFlow.Demo.Application.Users.Queries
{
    public record GetUserEventsQuery(Guid Id) : IQuery<List<CommittedDomainEvent>>;

    public class GetUserEventsQueryHandler : IQueryHandler<GetUserEventsQuery, List<CommittedDomainEvent>>
    {
        private IEventsRepository _repository;

        public GetUserEventsQueryHandler(IEventsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CommittedDomainEvent>> ExecuteQueryAsync(GetUserEventsQuery query, CancellationToken cancellationToken)
        {
            return await _repository.GetAsync<User>(query.Id);
        }
    }
}
