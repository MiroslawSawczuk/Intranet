using System.Threading.Tasks;
using BaseRepository.Repositories;
using Intranet.Users.Contexts;
using Intranet.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Users.Repositories
{
    public class ReadUserRepository : ReadRepositoryBase<User, UsersContext> , IReadUserRepository
    {
        private readonly UsersContext userContext;

        public ReadUserRepository(UsersContext userContext) : base(userContext)
        {
            this.userContext = userContext;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await userContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email.Equals(email));
        }
    }
}
