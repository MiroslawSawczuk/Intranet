using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Intranet.Authentication.DependencyInjection
{
    public static class AuthenticationServiceCollectionExtensions
    {
        public static void AddMicrosoftAuthentication(this IServiceCollection services, Action<MicrosoftAuthenticationOptions> configuration)
        {
            var settings = new MicrosoftAuthenticationOptions();
            configuration.Invoke(settings);

            services.AddAuthentication(options => 
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddJwtBearer(options =>
                {
                    options.Audience = "http://localhost:58414/";
                    options.Authority = "http://localhost:58414/";
                    options.RequireHttpsMetadata = false;
                })
                .AddMicrosoftAccount(microsoftOptions =>
                {
                    microsoftOptions.ClientId = settings.ApplicationId;
                    microsoftOptions.ClientSecret = settings.Password;
                    microsoftOptions.CallbackPath = new PathString("/signin-microsoft");
                });
        }
    }
}
