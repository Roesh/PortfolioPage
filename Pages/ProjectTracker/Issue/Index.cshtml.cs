using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PortfolioPage.Data;
using PortfolioPage.Models;

namespace PortfolioPage.Pages_ProjectTracker_Issue
{
    public class IndexModel : PageModel
    {
        private readonly PortfolioPage.Data.ApplicationDbContext _context;

        public IndexModel(PortfolioPage.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<issue> issue { get;set; }

        public async Task OnGetAsync()
        {
            issue = await _context.issue.ToListAsync();
        }
    }
}
