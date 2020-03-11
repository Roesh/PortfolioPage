using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortfolioPage.Models;

namespace PortfolioPage.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
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
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<project>().ToTable("Project");
            modelBuilder.Entity<projectPhaseHistory>().ToTable("Project Phase Hx");
            modelBuilder.Entity<projectCompletionDeadlineHistory>().ToTable("Project Completion Deadline Hx");

            modelBuilder.Entity<projectComponent>().ToTable("Project Components");
            modelBuilder.Entity<projectComponentCompletionDeadlineHistory>().ToTable("Project Component Deadline Hx");

            modelBuilder.Entity<projectUpdate>().ToTable("Project Update");
            modelBuilder.Entity<projectUpdateEditHistory>().ToTable("Project Update Edit Hx");
        }
    }
    
}
