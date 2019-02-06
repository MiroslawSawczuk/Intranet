using System;
using System.Text;
using Intranet.Authentication.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                        RequireSignedTokens = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer
                    };

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
