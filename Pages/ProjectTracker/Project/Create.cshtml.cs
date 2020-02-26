using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortfolioPage.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PortfolioPage.Models;

namespace PortfolioPage.Pages.ProjectTracker.Project
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly PortfolioPage.Data.PortfolioPageContext _context;

        public CreateModel(PortfolioPage.Data.PortfolioPageContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public project project { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.project.Add(project);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
