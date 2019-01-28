using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BaseRepository.Repositories
{
    public interface IReadRepository<TEntity> where TEntity : class
    {
        Task<TResult> GetAsync<TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TResult>> @select);
        Task<IEnumerable<TResult>> GetManyAsync<TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TResult>> @select);
    }
}
