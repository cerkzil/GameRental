using Microsoft.EntityFrameworkCore;
using GR.Domains;

namespace Skaitykla.EF
{
    public class BookContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; Database=coreDB; Trusted_Connection=True;");
        }

    }
}
