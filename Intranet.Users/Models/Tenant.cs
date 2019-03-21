using System.Collections.Generic;

namespace Intranet.Users.Models
{
    public class Tenant
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ParentTenantId { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
