using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using BaseRepository.Repositories;
using Cqrs.Queries;
using Cqrs.Validators;
using Intranet.Authentication.Tokens;
using Intranet.Users.Models;

namespace Intranet.Logic.QueryHandlers.Identity
{
    public class UserPropQuery : IQuery<UserPropDTO>
    {
    }

    internal class UserPropQueryHandler : AsyncQueryHandlerBase<UserPropQuery, UserPropDTO>
    {
        private readonly ITokenUser tokenUser;
        private readonly IReadRepository<User> readUserRepository;

        public UserPropQueryHandler(ITokenUser tokenUser, IReadRepository<User> readUserRepository)
        {
            this.tokenUser = tokenUser;
            this.readUserRepository = readUserRepository;
        }

        public override async Task ValidateAsync(UserPropQuery query, IValidationResult validationResult)
        {
            await Task.CompletedTask;
        }

        public override async Task<UserPropDTO> ExecuteAsync(UserPropQuery query)
        {
            return await readUserRepository.GetAsync(
                where: u => u.Id.Equals(tokenUser.Id),
                select: u => new UserPropDTO()
                {
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                });
        }
    }

    public class UserPropDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}