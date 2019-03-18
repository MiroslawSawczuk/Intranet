using BaseRepository.Repositories;
using Intranet.Users.Contexts;
using Intranet.Users.Models;

namespace Intranet.Users.Repositories
{
    public class WriteUserRepository : WriteRepositoryBase<User, UsersContext> , IWriteUserRepository
    {
        public WriteUserRepository(UsersContext context) : base(context)
        {
        }
    }
}
