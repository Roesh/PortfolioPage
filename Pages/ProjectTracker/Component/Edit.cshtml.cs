using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using PortfolioPage.Data;
using PortfolioPage.Pages.ProjectTracker;
using PortfolioPage.Models;

namespace PortfolioPage.Pages_ProjectTracker_Component
{
    public class EditModel : DI_BasePageModel
    {

        public EditModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public projectComponentViewModel projectComponentVM { get; set; }        

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var projectComponent = await Context.projectComponent.FirstOrDefaultAsync(m => m.ID == id);

            if (projectComponent == null)
            {
                return NotFound();
            }
            
            if(projectComponent.creatingUserID != getLoggedInUserId()){
                return Forbid();
            }
            
            projectComponentVM = populateVitrualModelFromProjectComponent(projectComponent);
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {

            var projectComponent = await Context.projectComponent.FirstOrDefaultAsync(m => m.ID == id);

            if (projectComponent == null)
            {
                return NotFound();
            }
            // AUTHORIZATION
            if(projectComponent.creatingUserID != getLoggedInUserId()){
                    return Forbid();
            }            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var projectComponentEntry = Context.Attach(projectComponent);
            projectComponentEntry.CurrentValues.SetValues(projectComponentVM);

            ModelState.Clear();
            if (!TryValidateModel(projectComponentEntry.Entity))
            {
                return Page();
            }
            await Context.SaveChangesAsync();

            return RedirectToPage("/ProjectTracker/MyProjects");
        }

        private projectComponentViewModel populateVitrualModelFromProjectComponent(projectComponent projectComponent)
        {        
            var projectComponentVM = new projectComponentViewModel();
            projectComponentVM.completionDate = projectComponent.completionDate;
            projectComponentVM.completionDeadline = projectComponent.completionDeadline;
            projectComponentVM.componentDescription = projectComponent.componentDescription;
            projectComponentVM.componentStatus = projectComponent.componentStatus;
            projectComponentVM.componentType = projectComponent.componentType;
            projectComponentVM.projectComponentID = projectComponent.projectComponentID;
            projectComponentVM.startDate = projectComponent.startDate;
            projectComponentVM.title = projectComponent.title;         
            return projectComponentVM;
        }
    }
}
