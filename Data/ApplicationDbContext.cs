using Labb_3___API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Labb_3___API.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeopleWithInterest>().HasKey(pi => new { pi.InterestId, pi.PeopleId });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<People> People { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<PeopleWithInterest> PeopleWithInterests { get; set; }
    }
}
