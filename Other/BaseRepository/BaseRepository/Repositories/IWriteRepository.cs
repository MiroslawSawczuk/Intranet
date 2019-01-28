using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BaseRepository.Repositories
{
    public interface IWriteRepository<TEntity> where TEntity : class
    {
        Task<TKey> CreateAsync<TKey>(Action<TEntity> create, Func<TEntity, TKey> keySelect);
        Task<TKey> UpdateAsync<TKey>(Expression<Func<TEntity, bool>> where, Action<TEntity> update, Func<TEntity, TKey> keySelect);
        Task DeleteAsync(Expression<Func<TEntity, bool>> where);
    }
}
