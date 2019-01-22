using Intranet.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Users.Contexts
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; internal set; }

        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
        }
    }
}
