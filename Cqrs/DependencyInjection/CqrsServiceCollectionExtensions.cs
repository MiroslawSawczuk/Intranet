using System;
using System.Linq;
using System.Reflection;
using Cqrs.Commands;
using Cqrs.Executors;
using Cqrs.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Cqrs.DependencyInjection
{
    public static class CqrsServiceCollectionExtensions
    {
        public static void AddCqrs(this IServiceCollection services, Action<CqrsOptions> configuration)
        {
            var settings = new CqrsOptions();
            configuration.Invoke(settings);
            
            services.AddSingleton<IExecutor, Executor>();

            if (settings.AutoRegister)
            {
                var assembly = AppDomain.CurrentDomain.GetAssemblies().First(x => x.FullName.Contains(settings.HandlersProjectName));
                services.RegisterCommandHandlers(assembly);
                services.RegisterQueryHandlers(assembly);
            }
        }

        private static void RegisterCommandHandlers(this IServiceCollection services, Assembly assembly)
        {
            var asyncCommandHandlers = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && t.BaseType != null
                            && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(AsyncCommandHandlerBase<>))
                .Select(t => t)
                .ToArray();

            foreach (var asyncCommandHandler in asyncCommandHandlers)
            {
                var arguments = asyncCommandHandler.BaseType.GetGenericArguments();
                var commandType = arguments[0];
                var interfaceType = typeof(IAsyncCommandHandler<>).MakeGenericType(commandType);

                services.AddScoped(interfaceType, asyncCommandHandler);
            }
            
            var commandHandlers = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && t.BaseType != null
                            && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(CommandHandlerBase<>))
                .Select(t => t)
                .ToArray();

            foreach (var commandHandler in commandHandlers)
            {
                var arguments = commandHandler.BaseType.GetGenericArguments();
                var commandType = arguments[0];
                var interfaceType = typeof(ICommandHandler<>).MakeGenericType(commandType);

                services.AddScoped(interfaceType, commandHandler);
            }
        }

        private static void RegisterQueryHandlers(this IServiceCollection services, Assembly assembly)
        {
            var asyncQueryHandlers = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && t.BaseType != null
                            && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(AsyncQueryHandlerBase<,>))
                .Select(t => t)
                .ToList();

            foreach (var asyncQueryHandler in asyncQueryHandlers)
            {
                var arguments = asyncQueryHandler.BaseType.GetGenericArguments();
                var queryType = arguments[0];
                var queryResultType = arguments[1];
                var interfaceType = typeof(IAsyncQueryHandler<,>).MakeGenericType(queryType, queryResultType);

                services.AddScoped(interfaceType, asyncQueryHandler);
            }
            
            var queryHandlers = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && t.BaseType != null
                            && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(QueryHandlerBase<,>))
                .Select(t => t)
                .ToList();

            foreach (var queryHandler in queryHandlers)
            {
                var arguments = queryHandler.BaseType.GetGenericArguments();
                var queryType = arguments[0];
                var queryResultType = arguments[1];
                var interfaceType = typeof(IQueryHandler<,>).MakeGenericType(queryType, queryResultType);

                services.AddScoped(interfaceType, queryHandler);
            }
        }
    }
}
