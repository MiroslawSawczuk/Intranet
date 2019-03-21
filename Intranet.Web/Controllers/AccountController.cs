using Cqrs.Executors;
using Intranet.Logic.CommandHandlers.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Intranet.Web.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IExecutor executor;

        public AccountController(IExecutor executor)
        {
            this.executor = executor;
        }

        [HttpGet("sign-in")]
        public IActionResult SignIn(SignInCommand command) => executor.Handle(command);

        [HttpGet("external-login-callback")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> LoginCallback(LoginCallbackCommand command) => await executor.HandleAsync(command);

        [HttpPost("save-user")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SaveUser([FromBody]SaveUserCommand command) => await executor.HandleAsync(command);
    }
}
