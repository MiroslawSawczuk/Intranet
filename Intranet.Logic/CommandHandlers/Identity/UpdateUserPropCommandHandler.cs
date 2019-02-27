using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BaseRepository.Repositories;
using Cqrs.Commands;
using Cqrs.Validators;
using Intranet.Authentication.Tokens;
using Intranet.Users.Models;
using Microsoft.AspNetCore.Http;

namespace Intranet.Logic.CommandHandlers.Account
{
    public class UpdateUserPropCommand : ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    internal class UpdateUserPropCommandHandler : AsyncCommandHandlerBase<UpdateUserPropCommand>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ITokenBuilder tokenBuilder;
        private readonly IWriteRepository<User> writeUserRepository;
        private readonly IReadRepository<User> readUserRepository;

        public UpdateUserPropCommandHandler(IHttpContextAccessor httpContextAccessor, ITokenBuilder tokenBuilder,
            IWriteRepository<User> writeUserRepository, IReadRepository<User> readUserRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.tokenBuilder = tokenBuilder;
            this.writeUserRepository = writeUserRepository;
            this.readUserRepository = readUserRepository;
        }

        public override async Task ValidateAsync(UpdateUserPropCommand command, IValidationResult validationResult)
        {
            //zrobiæ walidacjê czy propertisy z commanda String.IsNullOrWhitespace()
            await Task.CompletedTask;
        }

        public override async Task ExecuteAsync(UpdateUserPropCommand command)
        {
            var userEmail = httpContextAccessor.HttpContext.User.Claims.First(c => c.Type.Equals(ClaimTypes.Email)).Value;

            var userId = await readUserRepository.GetAsync(
                where: u => u.Email.Equals(userEmail),
                select: u => u.Id);

            string id = null;
            if (userId != null)
            {
                id = await writeUserRepository.UpdateAsync(
                    where: u => u.Id == userId,
                    update: u =>
                    {
                        u.FirstName = command.FirstName;
                        u.LastName = command.LastName;
                    },
                    keySelect: u => u.Id);
            }

            Body = tokenBuilder.BuildToken(id ?? userId, userEmail);
        }
    }
}