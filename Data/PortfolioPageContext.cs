using Microsoft.EntityFrameworkCore;

namespace PortfolioPage.Data
{
    public class PortfolioPageContext : DbContext
    {
        public PortfolioPageContext (
            DbContextOptions<PortfolioPageContext> options)
            : base(options)
        {
        }

        public DbSet<PortfolioPage.Models.Project> Project { get; set; }
        //public DbSet<PortfolioPage.Models.ProjectStatusCat> ProjectStatusCat { get; set; }
    }
}