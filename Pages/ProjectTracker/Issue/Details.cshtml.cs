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
    public class DetailsModel : PageModel
    {
        private readonly PortfolioPage.Data.ApplicationDbContext _context;

        public DetailsModel(PortfolioPage.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public issue issue { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            issue = await _context.issue.FirstOrDefaultAsync(m => m.ID == id);

            if (issue == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
