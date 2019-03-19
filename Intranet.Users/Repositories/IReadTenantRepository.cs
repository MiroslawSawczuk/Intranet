using BaseRepository.Repositories;
using Intranet.Users.Models;
using System.Threading.Tasks;

namespace Intranet.Users.Repositories
{
    public interface IReadTenantRepository : IReadRepository<Tenant>
    {
        Task<Tenant> GetAsync(string id);
    }
}
