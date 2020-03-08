using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortfolioPage.Data;
using PortfolioPage.Models;
using PortfolioPage.Pages.ProjectTracker;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using PortfolioPage.Pages.ProjectTracker.Component;

namespace PortfolioPage.Pages.ProjectTracker.Component
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

        public int parentProjectID;
        public int? parentComponentID;
        public async Task<IActionResult> OnGetAsync(int parentProject, int? parentComponent)
        {
            parentProjectID = parentProject;
            this.parentComponentID = parentComponent;

            currentProject = await Context.project.FindAsync(parentProject);

            if(parentComponent != null){
                currentComponent = await Context.projectComponent.FindAsync(parentComponent);                
                if(currentComponent == null){
                    return NotFound(new ArgumentException("Parent component ID argument not found"));
                }
            }

            if(currentProject == null){
                return NotFound(new ArgumentException("Parent project ID argument not found"));
            }
            
            return Page();
        }

        [BindProperty]
        public projectComponentViewModel projectComponentVM { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int parentProject, int? parentComponent)
        {
            var project = await Context.project.FindAsync(parentProject);
                        
            if(project == null){
                return NotFound();
            }
            if(project.creatingUserID != getLoggedInUserId()){
                return Forbid();
            }            
            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            // TODO: potentially refactor the creation code across entities (project, project component, project update)            
            var projectComponentEntry = Context.Add(new projectComponent());
            projectComponentEntry.CurrentValues.SetValues(projectComponentVM);
            
            // Items to be set automatically: Creation user, creation date, parent project Id, based on the url passed in.
            {
                projectComponentEntry.Entity.projectID = parentProject;
                projectComponentEntry.Entity.creatingUserID = getLoggedInUserId();
                projectComponentEntry.Entity.creationDate = DateTime.Now;
                if(projectComponentVM.parentComponentID == null){
                    projectComponentEntry.Entity.nodeDepth = 0;
                }else{
                    projectComponentEntry.Entity.nodeDepth = (int)projectComponentVM.parentComponentID + 1;
                }                
            }
            ModelState.Clear();
            
            if (!TryValidateModel(projectComponentEntry.Entity,nameof(projectComponentEntry.Entity)))
            {
                return Page();
            }

            await Context.SaveChangesAsync();

            return RedirectToPage("/ProjectTracker/MyProjects");
        }
        
    }
}
