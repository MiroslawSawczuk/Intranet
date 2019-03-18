using Cqrs.Commands;
using Intranet.Authentication.Tokens;
using Intranet.Users.Repositories;
using System.Threading.Tasks;

namespace Intranet.Logic.CommandHandlers.Identity
{
    public class UpdateUserPropsCommand : ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    internal class UpdateUserPropsCommandHandler : AsyncCommandHandlerBase<UpdateUserPropsCommand>
    {
        private readonly ITokenUser tokenUser;
        private readonly IWriteUserRepository writeUserRepository;

        public UpdateUserPropsCommandHandler(ITokenUser tokenUser, IWriteUserRepository writeUserRepository)
        {
            this.tokenUser = tokenUser;
            this.writeUserRepository = writeUserRepository;
        }
        
        public override async Task ExecuteAsync(UpdateUserPropsCommand command)
        {
            await writeUserRepository.UpdateAsync(
                where: u => u.Id == tokenUser.Id,
                update: u =>
                {
                    u.FirstName = command.FirstName;
                    u.LastName = command.LastName;
                },
                keySelect: u => u.Id);
        }
    }
}
