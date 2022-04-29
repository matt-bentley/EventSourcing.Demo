using EventFlow.Demo.Core.Abstractions.Repositories;
using EventFlow.Demo.Core.Applications.ReadModels;
using EventFlow.Queries;

namespace EventFlow.Demo.Core.Applications.Queries
{
    public record GetComponentQuery(Guid ApplicationId, Guid ComponentId) : IQuery<ComponentReadModel>;

    public class GetComponentQueryHandler : IQueryHandler<GetComponentQuery, ComponentReadModel>
    {
        private IReadModelRepository<ComponentReadModel> _repository;

        public GetComponentQueryHandler(IReadModelRepository<ComponentReadModel> repository)
        {
            _repository = repository;
        }

        public async Task<ComponentReadModel> ExecuteQueryAsync(GetComponentQuery query, CancellationToken cancellationToken)
        {
            return await _repository.FirstOrDefaultAsync(e => e.Id == query.ComponentId.ToString() && e.ApplicationId == query.ApplicationId.ToString());
        }
    }
}
