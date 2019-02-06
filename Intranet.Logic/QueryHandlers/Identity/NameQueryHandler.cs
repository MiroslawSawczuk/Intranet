using System.Threading.Tasks;
using BaseRepository.Repositories;
using Cqrs.Queries;
using Cqrs.Validators;
using Intranet.Authentication.Tokens;
using Intranet.Users.Models;

namespace Intranet.Logic.QueryHandlers.Identity
{
    public class NameQuery : IQuery<string>
    {
    }

    internal class NameQueryHandler : AsyncQueryHandlerBase<NameQuery, string>
    {
        private readonly ITokenUser tokenUser;
        private readonly IReadRepository<User> readUserRepository;

        public NameQueryHandler(ITokenUser tokenUser, IReadRepository<User> readUserRepository)
        {
            this.tokenUser = tokenUser;
            this.readUserRepository = readUserRepository;
        }

        public override async Task ValidateAsync(NameQuery query, IValidationResult validationResult)
        {
            await Task.CompletedTask;
        }

        public override async Task<string> ExecuteAsync(NameQuery query)
        {
            return await readUserRepository.GetAsync(
                where: u => u.Id.Equals(tokenUser.Id),
                select: u => u.FirstName);
        }
    }
}