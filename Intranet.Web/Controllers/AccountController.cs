using System.Linq;
using System.Security.Claims;
using Intranet.Authentication.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Intranet.Web.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly ITokenBuilder tokenBuilder;
        private readonly ITokenUser tokenUser;

        public AccountController(ITokenBuilder tokenBuilder, ITokenUser tokenUser)
        {
            this.tokenBuilder = tokenBuilder;
            this.tokenUser = tokenUser;
        }
        
        [HttpGet("sign-in")]
        public IActionResult SignIn() // TODO: implement CQRS command
        {
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = "/account/external-login-callback"
            };
            
            return Challenge(authenticationProperties, "Microsoft");
        }
        
        [HttpGet("external-login-callback")] // TODO: implement CQRS command
        public IActionResult PostSignIn()
        {
            var user = this.HttpContext.User;
            
            return Ok(tokenBuilder.BuildToken("losowe-id", user.Claims.First(c => c.Type.Equals(ClaimTypes.Email)).Value));
        }

        [Authorize]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(tokenUser.Email);
        }
    }
}
