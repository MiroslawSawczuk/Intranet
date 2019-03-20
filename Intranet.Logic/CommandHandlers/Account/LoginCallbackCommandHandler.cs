using Cqrs.Commands;
using Intranet.Authentication.Tokens;
using Intranet.Users.Enums;
using Intranet.Users.Models;
using Intranet.Users.Repositories;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Intranet.Logic.CommandHandlers.Account
{
    public class LoginCallbackCommand : ICommand
    {
    }

    internal class LoginCallbackCommandHandler : AsyncCommandHandlerBase<LoginCallbackCommand>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ITokenBuilder tokenBuilder;
        private readonly IWriteUserRepository writeUserRepository;
        private readonly IReadUserRepository readUserRepository;

        public LoginCallbackCommandHandler(IHttpContextAccessor httpContextAccessor, ITokenBuilder tokenBuilder,
            IWriteUserRepository writeUserRepository, IReadUserRepository readUserRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.tokenBuilder = tokenBuilder;
            this.writeUserRepository = writeUserRepository;
            this.readUserRepository = readUserRepository;
        }

        public override async Task ExecuteAsync(LoginCallbackCommand command)
        {
            var userEmail = httpContextAccessor.HttpContext.User.Claims.First(c => c.Type.Equals(ClaimTypes.Email)).Value;
            User user = await readUserRepository.Get(userEmail);

            if (user.Id == null)
            {
                user.Id = await writeUserRepository.CreateAsync(
                    create: u =>
                    {
                        u.Email = userEmail;
                        u.Permission = Permission.User;
                    },
                    keySelect: u => u.Id);

                Body = tokenBuilder.BuildToken(user.Id, userEmail, Permission.User, string.Empty);
            }
            else
            {
                Body = tokenBuilder.BuildToken(user.Id, user.Email, user.Permission, user.TenantId ?? string.Empty);
            }

        }
    }
}
