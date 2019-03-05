using System.Threading.Tasks;
using Cqrs.Validators;

namespace Cqrs.Queries
{
    public abstract class AsyncQueryHandlerBase<TQuery, TResult> : IAsyncQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        public virtual async Task ValidateAsync(TQuery query, IValidationResult validationResult)
        {
            await Task.CompletedTask;
        }
        public abstract Task<TResult> ExecuteAsync(TQuery query);
    }
}
