using System.Threading.Tasks;
using Cqrs.Executors;
using Intranet.Authentication.Tokens;
using Intranet.Logic.CommandHandlers.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Intranet.Web.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IExecutor executor;
        private readonly ITokenUser tokenUser;

        public AccountController(IExecutor executor, ITokenUser tokenUser)
        {
            this.executor = executor;
            this.tokenUser = tokenUser;
        }

        [HttpGet("sign-in")]
        public IActionResult SignIn(SignInCommand command) => executor.Handle(command);

        [HttpGet("external-login-callback")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> LoginCallback(LoginCallbackCommand command) => await executor.HandleAsync(command);

        [HttpGet("test")]
        [Authorize]
        public IActionResult Test()
        {
            return Ok(tokenUser.Email);
        }
    }
}
