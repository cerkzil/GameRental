using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GR.Domains;

namespace GR.EF
{
    public class Context : IdentityDbContext<AppUser>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=test8DB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
