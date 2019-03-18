using Cqrs.Queries;
using Intranet.Authentication.Tokens;
using Intranet.Users.Repositories;
using System.Threading.Tasks;

namespace Intranet.Logic.QueryHandlers.Identity
{
    public class NameQuery : IQuery<string>
    {
    }

    internal class NameQueryHandler : AsyncQueryHandlerBase<NameQuery, string>
    {
        private readonly ITokenUser tokenUser;
        private readonly IReadUserRepository readUserRepository;

        public NameQueryHandler(ITokenUser tokenUser, IReadUserRepository readUserRepository)
        {
            this.tokenUser = tokenUser;
            this.readUserRepository = readUserRepository;
        }

        public override async Task<string> ExecuteAsync(NameQuery query)
        {
            return await readUserRepository.GetAsync(
                where: u => u.Id.Equals(tokenUser.Id),
                select: u => u.FirstName ?? u.Email);
        }
    }
}