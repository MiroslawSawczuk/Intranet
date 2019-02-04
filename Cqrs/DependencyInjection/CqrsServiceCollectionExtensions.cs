using Cqrs.Executors;
using Microsoft.Extensions.DependencyInjection;

namespace Cqrs.DependencyInjection
{
    public static class CqrsServiceCollectionExtensions
    {
        public static void AddCqrs(this IServiceCollection services)
        {
            services.AddSingleton<IExecutor, Executor>();
        }
    }
}
