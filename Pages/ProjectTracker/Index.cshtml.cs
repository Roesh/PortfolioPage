using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PortfolioPage.Data;
using PortfolioPage.Models;

namespace PortfolioPage.Pages_ProjectTracker
{
    public class IndexModel : PageModel
    {
        private readonly PortfolioPage.Data.PortfolioPageContext _context;

        public IndexModel(PortfolioPage.Data.PortfolioPageContext context)
        {
            _context = context;
        }

        public IList<Project> Project { get;set; }

        public async Task OnGetAsync()
        {
            Project = await _context.Project.ToListAsync();
        }
    }
}
