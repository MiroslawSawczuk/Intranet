using BaseRepository.Repositories;
using Intranet.Users.Contexts;
using Intranet.Users.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Intranet.Users.Repositories
{
    public class ReadTenantRepository : ReadRepositoryBase<Tenant, UsersContext>, IReadTenantRepository
    {
        private readonly UsersContext userContext;

        public ReadTenantRepository(UsersContext userContext) : base(userContext)
        {
            this.userContext = userContext;
        }

        public async Task<Tenant> GetAsync(string id)
        {
            return await userContext.Tenants.AsNoTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));
        }
    }
}
