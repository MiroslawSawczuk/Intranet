using Intranet.Users.Enums;

namespace Intranet.Users.Models
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Permission Permission { get; set; }
        public string TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
