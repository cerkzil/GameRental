using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GR.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace GR.EF
{
    public class Context : IdentityDbContext<AppUser>
    {
        //public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=testDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

    }
}
