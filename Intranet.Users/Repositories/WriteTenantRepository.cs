using BaseRepository.Repositories;
using Intranet.Users.Contexts;
using Intranet.Users.Models;

namespace Intranet.Users.Repositories
{
    public class WriteTenantRepository : WriteRepositoryBase<Tenant, UsersContext>, IWriteTenantRepository
    {
        public WriteTenantRepository(UsersContext userContext) : base(userContext)
        {
        }
    }
}
