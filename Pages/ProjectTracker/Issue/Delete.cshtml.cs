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
    public class DeleteModel : PageModel
    {
        private readonly PortfolioPage.Data.ApplicationDbContext _context;

        public DeleteModel(PortfolioPage.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            issue = await _context.issue.FindAsync(id);

            if (issue != null)
            {
                _context.issue.Remove(issue);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
