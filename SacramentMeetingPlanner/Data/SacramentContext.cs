using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SacramentMeetingPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace SacramentMeetingPlanner.Data
{
    public class SacramentContext : DbContext
    {
        public SacramentContext(DbContextOptions<SacramentContext> options) : base(options)
        {
        }

        public DbSet<Planner> Planners { get; set; } 
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Planner>().ToTable("Planner");
            modelBuilder.Entity<Member>().ToTable("Member");
        }
    }
}
