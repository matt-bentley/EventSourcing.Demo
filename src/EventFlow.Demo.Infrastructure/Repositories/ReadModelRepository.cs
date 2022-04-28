using EventFlow.Demo.Core.Abstractions.ReadModels;
using EventFlow.Demo.Core.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventFlow.Demo.Infrastructure.Repositories
{
    public class ReadModelRepository<T> : IReadModelRepository<T> where T : ReadModel
    {
        private DemoContext _context;
        private DbSet<T> _entitySet;
        
        public ReadModelRepository(DemoContext context)
        {
            _context = context;
            _entitySet = _context.Set<T>();
        }

        public IQueryable<T> Get()
        {
            return _entitySet.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await Get()
                .FirstOrDefaultAsync(predicate);
        }
    }
}
