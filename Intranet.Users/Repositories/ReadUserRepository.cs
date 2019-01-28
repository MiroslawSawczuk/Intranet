using BaseRepository.Repositories;
using Intranet.Users.Contexts;
using Intranet.Users.Models;

namespace Intranet.Users.Repositories
{
    internal class ReadUserRepository : ReadRepositoryBase<User, UsersContext>
    {
        public ReadUserRepository(UsersContext context) : base(context)
        {
        }
    }
}
