using System;
using System.Linq;
using System.Reflection;
using BaseRepository.Repositories;
using Intranet.Users.Contexts;
using Intranet.Users.Models;
using Intranet.Users.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
                var arguments = readRepository.BaseType.GetGenericArguments();
                var modelType = arguments[0];
                var interfaceType = typeof(IReadRepository<>).MakeGenericType(modelType);

                services.AddScoped(interfaceType, readRepository);
            }
            
            var writeRepositories = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && t.BaseType != null
                            && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(WriteRepositoryBase<,>))
                .Select(t => t)
                .ToArray();

            foreach (var writeRepository in writeRepositories)
            {
                var arguments = writeRepository.BaseType.GetGenericArguments();
                var modelType = arguments[0];
                var interfaceType = typeof(IWriteRepository<>).MakeGenericType(modelType);

                services.AddScoped(interfaceType, writeRepository);
            }
        }
    }
}
