using Microsoft.EntityFrameworkCore;
using PortfolioPage.Models;
using System.Collections;

namespace PortfolioPage.Data
{
    public class PortfolioPageContext : DbContext
    {
        public PortfolioPageContext (
            DbContextOptions<PortfolioPageContext> options)
            : base(options)
        {
        }

        public DbSet<project> project { get; set; }
        public DbSet<projectCompletionDeadlineHistory> projectCompletionDeadlineHx { get; set; }
        public DbSet<projectPhaseHistory> projectPhaseHistory { get; set; }

        public DbSet<projectComponent> projectComponent { get; set; }
        public DbSet<projectComponentCompletionDeadlineHistory> projectComponentCompletionDeadlineHx { get; set; }
        public DbSet<projectUpdate> projectUpdate { get; set; }
        public DbSet<projectUpdateEditHistory> projectUpdateEditHistory { get; set; }
        
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<project>().ToTable("Project");
            modelBuilder.Entity<projectPhaseHistory>().ToTable("Project Phase");
            modelBuilder.Entity<projectCompletionDeadlineHistory>().ToTable("Project Completion Deadline history");

            modelBuilder.Entity<projectComponent>().ToTable("Project Components");
            modelBuilder.Entity<projectComponentCompletionDeadlineHistory>().ToTable("Project Component Deadline History");

            modelBuilder.Entity<projectUpdate>().ToTable("Project Update");
            modelBuilder.Entity<projectUpdateEditHistory>().ToTable("Project Update History");
        }
    }
}