using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortfolioPage.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PortfolioPage.Models;

namespace PortfolioPage.Pages.ProjectTracker.Project
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly PortfolioPage.Data.ApplicationDbContext _context;

        public EditModel(PortfolioPage.Data.ApplicationDbContext context)
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!projectExists(project.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool projectExists(int id)
        {
            return _context.project.Any(e => e.ID == id);
        }
    }
}
