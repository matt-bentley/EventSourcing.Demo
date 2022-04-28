using EventFlow.Demo.Core.Abstractions.Repositories;
using EventFlow.Demo.Core.Users.ReadModels;
using EventFlow.Queries;

namespace EventFlow.Demo.Application.Users.Queries
{
    public record GetByEmailQuery(string Email) : IQuery<UserReadModel>;

    public class GetByEmailQueryHandler : IQueryHandler<GetByEmailQuery, UserReadModel>
    {
        private IReadModelRepository<UserReadModel> _repository;

        public GetByEmailQueryHandler(IReadModelRepository<UserReadModel> repository)
        {
            _repository = repository;
        }

        public async Task<UserReadModel> ExecuteQueryAsync(GetByEmailQuery query, CancellationToken cancellationToken)
        {
            return await _repository.FirstOrDefaultAsync(e => e.Email == query.Email);
        }
    }
}
