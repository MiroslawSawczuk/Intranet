using BaseRepository.Repositories;
using Intranet.Users.Models;

namespace Intranet.Users.Repositories
{
    public interface IWriteTenantRepository : IWriteRepository<Tenant>
    {
    }
}
