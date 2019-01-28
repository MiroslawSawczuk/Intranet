using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BaseRepository.Repositories
{
    public abstract class ReadRepositoryBase<TEntity, TContext> : IReadRepository<TEntity> where TEntity : class where TContext : DbContext
    {
        private readonly TContext context;

        protected ReadRepositoryBase(TContext context)
        {
            this.context = context;
        }

        public async Task<TResult> GetAsync<TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TResult>> @select)
        {
            return await context.Set<TEntity>().AsNoTracking()
                .Where(where)
                .Select(select)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TResult>> GetManyAsync<TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TResult>> select)
        {
            return await context.Set<TEntity>().AsNoTracking()
                .Where(where)
                .Select(select)
                .ToListAsync();
        }
    }
}
