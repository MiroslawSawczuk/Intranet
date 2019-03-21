using BaseRepository.Repositories;
using Intranet.Users.Contexts;
using Intranet.Users.Models;

namespace Intranet.Users.Repositories
{
    public class ReadTenantRepository : ReadRepositoryBase<Tenant, UsersContext>, IReadTenantRepository
    {
        public ReadTenantRepository(UsersContext userContext) : base(userContext)
        {
        }
    }
}
