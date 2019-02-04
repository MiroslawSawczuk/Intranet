using Cqrs.Validators;
using System.Threading.Tasks;

namespace Cqrs.Queries
{
    public interface IAsyncQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task ValidateAsync(TQuery query, IValidationResult validationResult);
        Task<TResult> ExecuteAsync(TQuery query);
    }
}
