using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PortfolioPage.Models;


namespace PortfolioPage.ProjectTracker.Pages
{
    public class summaryModel : PageModel
    {
        private readonly PortfolioPage.Data.PortfolioPageContext _context;

        public summaryModel(PortfolioPage.Data.PortfolioPageContext context)
        {
            _context = context;
        }

        public IList<project> project { get;set; }

        public async Task OnGetAsync()
        {
            project = await _context.project.ToListAsync();
        }
    }
}
