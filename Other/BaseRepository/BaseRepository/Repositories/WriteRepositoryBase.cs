using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BaseRepository.Repositories
{
    public abstract class WriteRepositoryBase<TEntity, TContext> : IWriteRepository<TEntity> where TEntity : class, new() where TContext : DbContext
    {
        private readonly TContext context;

        protected WriteRepositoryBase(TContext context)
        {
            this.context = context;
        }

        public async Task<TKey> CreateAsync<TKey>(Action<TEntity> create, Func<TEntity, TKey> keySelect)
        {
            var entity = new TEntity();
            create.Invoke(entity);

            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();

            return keySelect.Invoke(entity);
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            context.Set<TEntity>().Remove(await context.Set<TEntity>().FirstOrDefaultAsync(where));
            await context.SaveChangesAsync();
        }

        public async Task<TKey> UpdateAsync<TKey>(Expression<Func<TEntity, bool>> where, Action<TEntity> update, Func<TEntity, TKey> keySelect)
        {
            var entity = await context.Set<TEntity>().FirstOrDefaultAsync(where);
            update.Invoke(entity);

            await context.SaveChangesAsync();

            return keySelect.Invoke(entity);
        }
    }
}
