using Cqrs.Validators;
using System.Threading.Tasks;

namespace Cqrs.Commands
{
    public interface IAsyncCommandHandler<TCommand> where TCommand : ICommand
    {
        int? Status { get; }
        object Body { get; }
        Task ValidateAsync(TCommand command, IValidationResult validationResult);
        Task ExecuteAsync(TCommand command);
    }
}
