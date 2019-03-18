using Intranet.Users.Enums;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Intranet.Authentication.Tokens
{
    internal class TokenUser : ITokenUser
    {
        public string Id { get; }
        public string Email { get; }
        public string Role { get; }

        public TokenUser(IHttpContextAccessor context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                Id = context.HttpContext.User.Claims.First(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Value;
                Email = context.HttpContext.User.Claims.First(c => c.Type.Equals(ClaimTypes.Email)).Value;
                Role = context.HttpContext.User.Claims.First(c => c.Type.Equals(ClaimTypes.Role)).Value;
            }
            else
            {
                Id = null;
                Email = null;
                Role = Permission.User.ToString();
            }
        }
    }
}
