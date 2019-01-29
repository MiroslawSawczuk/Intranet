using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Intranet.Authentication.Tokens
{
    internal class TokenUser : ITokenUser
    {
        public string Id { get; }
        public string Email { get; }

        public TokenUser(IHttpContextAccessor context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                Id = context.HttpContext.User.Claims.First(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Value;
                Email = context.HttpContext.User.Claims.First(c => c.Type.Equals(ClaimTypes.Email)).Value;
            }
            else
            {
                Id = null;
                Email = null;
            }
        }
    }
}
