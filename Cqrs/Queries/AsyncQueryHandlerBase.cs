using System.Threading.Tasks;
using Cqrs.Validators;

namespace Cqrs.Queries
{
    public abstract class AsyncQueryHandlerBase<TQuery, TResult> : IAsyncQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        public abstract Task ValidateAsync(TQuery query, IValidationResult validationResult);
        public abstract Task<TResult> ExecuteAsync(TQuery query);
    }
}
