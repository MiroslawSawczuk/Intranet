using System;
using System.Collections.Generic;
using System.Text;
using BaseRepository.Repositories;
using Intranet.Users.Contexts;
using Intranet.Users.Models;

namespace Intranet.Users.Repositories
{
    public class WriteTenantRepository : WriteRepositoryBase<Tenant, UsersContext>, IWriteTenantRepository
    {
        private readonly UsersContext userContext;

        public WriteTenantRepository(UsersContext userContext) : base(userContext)
        {
            this.userContext = userContext;
        }
    }
}
