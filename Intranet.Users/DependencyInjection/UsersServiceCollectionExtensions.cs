using System;
using Intranet.Users.Contexts;
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
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
