using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortfolioPage.Data;
using PortfolioPage.Models;

namespace PortfolioPage.Pages_ProjectTracker_Issue
{
    public class EditModel : PageModel
    {
        private readonly PortfolioPage.Data.ApplicationDbContext _context;

        public EditModel(PortfolioPage.Data.ApplicationDbContext context)
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(issue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!issueExists(issue.ID))
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

        private bool issueExists(int id)
        {
            return _context.issue.Any(e => e.ID == id);
        }
    }
}
