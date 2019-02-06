using Cqrs.Validators;

namespace Cqrs.Queries
{
    public abstract class QueryHandlerBase<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        public abstract void Validate(TQuery query, IValidationResult validationResult);
        public abstract TResult Execute(TQuery query);
    }
}
