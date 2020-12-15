using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RegistruCentras.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistruDomains.Data
{
    public class Context : IdentityDbContext<AppUser>
    {
        public DbSet<Faq> Faqs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RegistruCentras;Trusted_Connection=True;MultipleActiveResultSets=True");
        }
    }
}
