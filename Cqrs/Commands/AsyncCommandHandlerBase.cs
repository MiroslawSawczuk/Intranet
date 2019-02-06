using System.Threading.Tasks;
using Cqrs.Validators;

namespace Cqrs.Commands
{
    public abstract class AsyncCommandHandlerBase<TCommand> : IAsyncCommandHandler<TCommand> where TCommand : ICommand
    {
        public int? Status { get; protected set; }
        public object Body { get; protected set; }

        public abstract Task ValidateAsync(TCommand command, IValidationResult validationResult);
        public abstract Task ExecuteAsync(TCommand command);
    }
}
