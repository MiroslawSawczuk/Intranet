using BaseRepository.Repositories;
using Intranet.Users.Contexts;
using Intranet.Users.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Intranet.Users.Repositories
{
    public class ReadUserRepository : ReadRepositoryBase<User, UsersContext>, IReadUserRepository
    {
        private readonly UsersContext userContext;

        public ReadUserRepository(UsersContext userContext) : base(userContext)
        {
            this.userContext = userContext;
        }

        public async Task<User> Get(string email)
        {
            return await userContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email.Equals(email));
        }
    }
}
