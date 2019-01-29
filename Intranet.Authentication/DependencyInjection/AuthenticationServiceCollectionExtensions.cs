using System;
using Intranet.Authentication.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Intranet.Authentication.DependencyInjection
{
    public static class AuthenticationServiceCollectionExtensions
    {
        public static void AddMicrosoftAuthentication(this IServiceCollection services, Action<MicrosoftAuthenticationOptions> configuration,
            Action<JwtTokenOptions> jwtConfiguration)
        {
            var settings = new MicrosoftAuthenticationOptions();
            configuration.Invoke(settings);

            var jwtSettings = new JwtTokenOptions();
            jwtConfiguration.Invoke(jwtSettings);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ITokenBuilder>(t => new TokenBuilder(jwtSettings));
            services.AddScoped<ITokenUser, TokenUser>();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddJwtBearer(options =>
                {
                    options.Audience = jwtSettings.Audience;
                    options.Authority = jwtSettings.Issuer;
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
