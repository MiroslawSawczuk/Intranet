using System.Threading.Tasks;
using Cqrs.Validators;

namespace Cqrs.Commands
{
    public abstract class AsyncCommandHandlerBase<TCommand> : IAsyncCommandHandler<TCommand> where TCommand : ICommand
    {
        public int? Status { get; protected set; }
        public object Body { get; protected set; }

        public virtual async Task ValidateAsync(TCommand command, IValidationResult validationResult)
        {
            await Task.CompletedTask;
        }
        public abstract Task ExecuteAsync(TCommand command);
    }
}
