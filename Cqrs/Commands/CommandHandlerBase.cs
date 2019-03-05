using Cqrs.Validators;

namespace Cqrs.Commands
{
    public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public int? Status { get; protected set; }
        public object Body { get; protected set; }
        
        public virtual void Validate(TCommand command, IValidationResult validationResult) { }
        public abstract void Execute(TCommand command);
    }
}
