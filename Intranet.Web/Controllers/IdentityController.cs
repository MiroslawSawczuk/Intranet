using System.Threading.Tasks;
using Cqrs.Executors;
using Intranet.Logic.CommandHandlers.Account;
using Intranet.Logic.QueryHandlers.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Intranet.Web.Controllers
{
    [Route("api/identity")]
    public class IdentityController : Controller
    {
        private readonly IExecutor executor;

        public IdentityController(IExecutor executor)
        {
            this.executor = executor;
        }
        
        [Authorize]
        [Route("name")]
        public async Task<IActionResult> Name(NameQuery query) => await executor.QueryAsync<NameQuery, string>(query);

        [Authorize]
        [Route("userProp")]
        public async Task<IActionResult> UserProp(UserPropQuery query) => await executor.QueryAsync<UserPropQuery, UserPropDTO>(query);

        [Authorize]
        [HttpPost("updateUserProp")]
        public async Task<IActionResult> UpdateUserProp([FromBody]UpdateUserPropCommand command) => await executor.HandleAsync(command);
    }
}
