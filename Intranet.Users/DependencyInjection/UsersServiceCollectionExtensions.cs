using BaseRepository.Repositories;
using Intranet.Users.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Intranet.Users.DependencyInjection
{
    public static class UsersServiceCollectionExtensions
    {
        public static void AddSqlServerUsers(this IServiceCollection services, Action<SqlServerUsersOptions> configuration)
        {
            var settings = new SqlServerUsersOptions();
            configuration.Invoke(settings);

            services.AddDbContext<UsersContext>(options => options.UseSqlServer(settings.ConnectionString));

            if (settings.AutoRegister)
            {
                var assembly = AppDomain.CurrentDomain.GetAssemblies().First(x => x.FullName.Contains(settings.RepositoriesProjectName));
                services.RegisterRepositories(assembly);
            }
        }

        private static void RegisterRepositories(this IServiceCollection services, Assembly assembly)
        {
            var readRepositories = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && t.BaseType != null
                            && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(ReadRepositoryBase<,>))
                .Select(t => t)
                .ToArray();

            foreach (var readRepository in readRepositories)
            {
                var interfaceType = readRepository.GetInterfaces()
                    .FirstOrDefault(i => i.GetInterfaces().Any(ii => ii.GetGenericTypeDefinition() == typeof(IReadRepository<>)));

                services.AddScoped(interfaceType, readRepository);
            }

            var writeRepositories = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && t.BaseType != null
                            && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(WriteRepositoryBase<,>))
                .Select(t => t)
                .ToArray();

            foreach (var writeRepository in writeRepositories)
            {
                var interfaceType = writeRepository.GetInterfaces()
                    .FirstOrDefault(i => i.GetInterfaces().Any(ii => ii.GetGenericTypeDefinition() == typeof(IWriteRepository<>)));

                services.AddScoped(interfaceType, writeRepository);
            }
        }
    }
}
