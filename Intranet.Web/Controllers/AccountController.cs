using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Intranet.Web.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        [HttpGet("sign-in")]
        public IActionResult SignIn()
        {
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = "/account/external-login-callback"
            };
            
            return Challenge(authenticationProperties, "Microsoft");
        }
        
        [HttpGet("external-login-callback")]
        public IActionResult PostSignIn()
        {
            var user = this.HttpContext.User;
            
            return Ok(user.Claims.First(c => c.Type.Equals(ClaimTypes.Email)).Value);
        }
    }
}
