using Cqrs.Commands;
using Intranet.Authentication.Tokens;
using Intranet.Users.Enums;
using Intranet.Users.Repositories;
using System.Threading.Tasks;
using Cqrs.Validators;
using Remotion.Linq.Clauses;

namespace Intranet.Logic.CommandHandlers.Account
{
    public class SaveUserCommand : ICommand
    {
        public string Email { get; set; }
        public string TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    internal class SaveUserCommandHandler : AsyncCommandHandlerBase<SaveUserCommand>
    {
        private readonly ITokenBuilder tokenBuilder;
        private readonly IWriteUserRepository writeUserRepository;
        private readonly IReadUserRepository readUserRepository;
        private readonly IReadTenantRepository readTenantRepository;

        public SaveUserCommandHandler(ITokenBuilder tokenBuilder,
            IWriteUserRepository writeUserRepository, IReadUserRepository readUserRepository, IReadTenantRepository readTenantRepository)
        {
            this.tokenBuilder = tokenBuilder;
            this.writeUserRepository = writeUserRepository;
            this.readUserRepository = readUserRepository;
            this.readTenantRepository = readTenantRepository;
        }

        public override async Task ValidateAsync(SaveUserCommand command, IValidationResult validationResult)
        {
            var tenantId = await readTenantRepository.GetAsync(
                where: t => t.Id.Equals(command.TenantId),
                select: t => t.Id);

            if (tenantId == null)
            {
                validationResult.AddError($"A company with number: {command.TenantId} does not exist.");
            }

            var userId = await readUserRepository.GetAsync(
                where: u => u.Email.Equals(command.Email)
                            && u.TenantId.Equals(command.TenantId),
                select: u => u.Id);

            if (userId != null)
            {
                validationResult.AddError($"User with email: {command.Email} in company with number: {command.TenantId} is already exist.");
            }
        }

        public override async Task ExecuteAsync(SaveUserCommand command)
        {
            var id = await writeUserRepository.CreateAsync(
                create: u =>
                {
                    u.Email = command.Email;
                    u.FirstName = command.FirstName;
                    u.LastName = command.LastName;
                    u.TenantId = command.TenantId;
                    u.Permission = Permission.User;
                },
                keySelect: u => u.Id);

            Body = tokenBuilder.BuildToken(id, command.Email, Permission.User, command.TenantId);
        }
    }
}
