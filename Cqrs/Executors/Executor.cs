using Cqrs.Commands;
using Cqrs.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cqrs.Queries;
using Microsoft.AspNetCore.Authentication;

namespace Cqrs.Executors
{
    internal class Executor : IExecutor
    {
        private readonly IServiceProvider serviceProvider;

        public Executor(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IActionResult Handle<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandHandler = serviceProvider.GetService<ICommandHandler<TCommand>>();
            var validationResult = new ValidationResult();
            commandHandler.Validate(command, validationResult);
            if (validationResult.Errors.Count > 0)
            {
                return new BadRequestResult();
            }
            commandHandler.Execute(command);

            return commandHandler.Status.HasValue ? (IActionResult)new StatusCodeResult(commandHandler.Status.Value)
                : commandHandler.Body is IActionResult body ? body
                : commandHandler.Body != null ? (IActionResult)new OkObjectResult(commandHandler.Body)
                : new OkResult();
        }

        public async Task<IActionResult> HandleAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandHandler = serviceProvider.GetService<IAsyncCommandHandler<TCommand>>();
            var validationResult = new ValidationResult();
            await commandHandler.ValidateAsync(command, validationResult);
            if (validationResult.Errors.Count > 0)
            {
                return new BadRequestResult();
            }
            await commandHandler.ExecuteAsync(command);

            return commandHandler.Status.HasValue ? (IActionResult)new StatusCodeResult(commandHandler.Status.Value)
                : commandHandler.Body is IActionResult body ? body
                : commandHandler.Body != null ? (IActionResult)new OkObjectResult(commandHandler.Body)
                : new OkResult();
        }

        public IActionResult Query<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var queryHandler = serviceProvider.GetService<IQueryHandler<TQuery, TResult>>();
            var validationResult = new ValidationResult();
            queryHandler.Validate(query, validationResult);
            if (validationResult.Errors.Count > 0)
            {
                return new BadRequestResult();
            }

            return new OkObjectResult(queryHandler.Execute(query));
        }

        public async Task<IActionResult> QueryAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var queryHandler = serviceProvider.GetService<IAsyncQueryHandler<TQuery, TResult>>();
            var validationResult = new ValidationResult();
            await queryHandler.ValidateAsync(query, validationResult);
            if (validationResult.Errors.Count > 0)
            {
                return new BadRequestResult();
            }

            return new OkObjectResult(await queryHandler.ExecuteAsync(query));
        }
    }
}
