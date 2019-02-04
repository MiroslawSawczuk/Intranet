using Cqrs.Validators;

namespace Cqrs.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        int? Status { get; }
        object Body { get; }
        void Validate(TCommand command, IValidationResult validationResult);
        void Execute(TCommand command);
    }
}
