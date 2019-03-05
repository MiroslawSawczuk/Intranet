using System.Threading.Tasks;
using Cqrs.Executors;
using Intranet.Logic.CommandHandlers.Identity;
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
        [Route("user-props")]
        public async Task<IActionResult> UserProp(UserPropsQuery query) => await executor.QueryAsync<UserPropsQuery, UserPropsDto>(query);

        [Authorize]
        [HttpPatch("update-user-props")]
        public async Task<IActionResult> UpdateUserProp([FromBody]UpdateUserPropsCommand command) => await executor.HandleAsync(command);
    }
}
