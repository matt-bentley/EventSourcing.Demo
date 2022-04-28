using EventFlow.Demo.Core.Abstractions.ReadModels;
using System.Linq.Expressions;

namespace EventFlow.Demo.Core.Abstractions.Repositories
{
    public interface IReadModelRepository<T> where T : ReadModel
    {
        IQueryable<T> Get();
        Task<T> GetByIdAsync(string id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    }
}
