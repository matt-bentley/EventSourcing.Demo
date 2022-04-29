using EventFlow.Demo.Core.Abstractions.Repositories;
using EventFlow.Demo.Core.Applications.ReadModels;
using EventFlow.Queries;

namespace EventFlow.Demo.Application.Applications.Queries
{
    public record GetApplicationSummariesQuery() : IQuery<List<ApplicationSummaryReadModel>>;

    public class GetApplicationSummariesQueryHandler : IQueryHandler<GetApplicationSummariesQuery, List<ApplicationSummaryReadModel>>
    {
        private IReadModelRepository<ApplicationSummaryReadModel> _repository;

        public GetApplicationSummariesQueryHandler(IReadModelRepository<ApplicationSummaryReadModel> repository)
        {
            _repository = repository;
        }

        public Task<List<ApplicationSummaryReadModel>> ExecuteQueryAsync(GetApplicationSummariesQuery query, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.Get().ToList());
        }
    }
}
