using Cqrs.Validators;

namespace Cqrs.Queries
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        void Validate(TQuery query, IValidationResult validationResult);
        TResult Execute(TQuery query);
    }
}
