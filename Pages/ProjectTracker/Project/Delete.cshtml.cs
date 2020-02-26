using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PortfolioPage.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PortfolioPage.Models;

namespace PortfolioPage.Pages.ProjectTracker.Project
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly PortfolioPage.Data.PortfolioPageContext _context;

        public DeleteModel(PortfolioPage.Data.PortfolioPageContext context)
        {
            _context = context;
        }

        [BindProperty]
        public project project { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            project = await _context.project.FirstOrDefaultAsync(m => m.ID == id);

            if (project == null)
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

            project = await _context.project.FindAsync(id);

            if (project != null)
            {
                _context.project.Remove(project);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
