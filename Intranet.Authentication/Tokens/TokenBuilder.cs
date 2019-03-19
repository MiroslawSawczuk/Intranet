using Intranet.Authentication.DependencyInjection;
using Intranet.Users.Enums;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Intranet.Authentication.Tokens
{
    internal class TokenBuilder : ITokenBuilder
    {
        private readonly JwtTokenOptions jwtTokenOptions;

        public TokenBuilder(JwtTokenOptions jwtTokenOptions)
        {
            this.jwtTokenOptions = jwtTokenOptions;
        }

        public string BuildToken(string id, string email)
        {
            return BuildToken(id, email);
        }

        public string BuildToken(string id, string email, Permission role, string tenantId)
        {
            return BuildToken(id, email, role, tenantId, out var expiration);
        }

        public string BuildToken(string id, string email, Permission role, string tenantId, out DateTime expiration)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role.ToString()),
                new Claim("TenantId", tenantId)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenOptions.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            expiration = DateTime.UtcNow.Add(jwtTokenOptions.Expiration);

            var token = new JwtSecurityToken(
                issuer: jwtTokenOptions.Issuer,
                audience: jwtTokenOptions.Audience,
                expires: expiration,
                claims: claims,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
