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
    public class DeleteModel : DI_BasePageModel
    {
        private readonly PortfolioPage.Data.ApplicationDbContext _context;

        public DeleteModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
            _context = context;
        }

        [BindProperty]
        public project project { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            project = await _context.project.FirstOrDefaultAsync(m => m.ID == id);

            if (project == null)
            {
                return NotFound();
            }
            // TODO: Move this to authorization folder
            if(project.creatingUserID != getLoggedInUserId()){
                return Forbid();
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
            
            if (project == null){
                return NotFound();
            }
            // TODO: Move this to authorization folder
            if(project.creatingUserID != getLoggedInUserId()){
                return Forbid();
            }
        
            _context.project.Remove(project);
            await _context.SaveChangesAsync();    

            return RedirectToPage("/ProjectTracker/MyProjects");
        }
    }
}
