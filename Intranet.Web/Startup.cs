using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Intranet.Authentication.DependencyInjection;
using Intranet.Users.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VueCliMiddleware;

namespace Intranet.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IContainer ApplicationContainer { get; private set; }
        public IConfigurationRoot Configuration { get; private set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var builder = new ContainerBuilder();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSqlServerUsers(configuration =>
            {
                configuration.ConnectionString = Configuration["ConnectionStrings:Users"];
            });

            services.AddMicrosoftAuthentication(configuration =>
            {
                configuration.ApplicationId = Configuration["Authentication:Microsoft:ApplicationId"];
                configuration.Password = Configuration["Authentication:Microsoft:Password"];
            }, jwtConfiguration =>
            {
                jwtConfiguration.Audience = Configuration["Jwt:Audience"];
                jwtConfiguration.Issuer = Configuration["Jwt:Issuer"];
                jwtConfiguration.Key = Configuration["Jwt:Key"];

                if (int.TryParse(Configuration["Jwt:ExpireTime:Minutes"], out var minutes)
                    && int.TryParse(Configuration["Jwt:ExpireTime:Hours"], out var hours))
                {
                    jwtConfiguration.Expiration = new TimeSpan(hours, minutes, 0);
                }
            });

            builder.Populate(services);
            this.ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "serve", port: 8080);
                }
            });
        }
    }
}
