using System.Threading.Tasks;
using Cqrs.Commands;
using Cqrs.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Cqrs.Executors
{
    public interface IExecutor
    {
        IActionResult Handle<TCommand>(TCommand command) where TCommand : ICommand;
        Task<IActionResult> HandleAsync<TCommand>(TCommand command) where TCommand : ICommand;
        IActionResult Query<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
        Task<IActionResult> QueryAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}
