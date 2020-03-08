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
using PortfolioPage.Pages.ProjectTracker;
using PortfolioPage.Authorization;

namespace PortfolioPage.Pages_ProjectTracker_Project
{
    public class CreateModel : DI_BasePageModel
    {
        public CreateModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public IActionResult OnGet()
        {     
            return Page();
        }

        [BindProperty]
        public projectViewModel projectVM { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        
        public async Task<IActionResult> OnPostAsync()
        {
            // Make sure view model is valid first
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var projectEntry = Context.Add(new project());
            projectEntry.CurrentValues.SetValues(projectVM);

            { // Automatic behind the scenes population
                projectEntry.Entity.creatingUserID = getLoggedInUserId();
                projectEntry.Entity.creationDate = DateTime.Now;
            }

            ModelState.Clear();
            
            if (!TryValidateModel(projectEntry.Entity,nameof(projectEntry)))
            {
                return Page();
            }
            
            await Context.SaveChangesAsync();

            return RedirectToPage("/ProjectTracker/MyProjects");
        }
    }
}
