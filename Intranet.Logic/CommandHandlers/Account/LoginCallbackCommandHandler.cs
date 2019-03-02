using BaseRepository.Repositories;
using Cqrs.Commands;
using Cqrs.Validators;
using Intranet.Authentication.Tokens;
using Intranet.Users.Models;
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
        private readonly IWriteRepository<User> writeUserRepository;
        private readonly IReadRepository<User> readUserRepository;

        public LoginCallbackCommandHandler(IHttpContextAccessor httpContextAccessor, ITokenBuilder tokenBuilder,
            IWriteRepository<User> writeUserRepository, IReadRepository<User> readUserRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.tokenBuilder = tokenBuilder;
            this.writeUserRepository = writeUserRepository;
            this.readUserRepository = readUserRepository;
        }

        public override async Task ValidateAsync(LoginCallbackCommand command, IValidationResult validationResult)
        {
            await Task.CompletedTask;
        }

        public override async Task ExecuteAsync(LoginCallbackCommand command)
        {
            var userEmail = httpContextAccessor.HttpContext.User.Claims.First(c => c.Type.Equals(ClaimTypes.Email)).Value;

            var userId = await readUserRepository.GetAsync(
                where: u => u.Email.Equals(userEmail),
                select: u => u.Id);

            string id = null;
            if (userId == null)
            {
                id = await writeUserRepository.CreateAsync(
                    create: u => u.Email = userEmail,
                    keySelect: u => u.Id);
            }

            Body = tokenBuilder.BuildToken(id ?? userId, userEmail);
        }
    }
}
