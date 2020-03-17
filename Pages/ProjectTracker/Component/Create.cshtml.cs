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
        public async Task<IActionResult> OnGetAsync(int parentProjectID, int? parentComponentID)
        {
            this.parentProjectID = parentProjectID;
            this.parentComponentID = parentComponentID;

            currentProject = await Context.project.FindAsync(parentProjectID);
            
            // AUTHORIZATION
            if(currentProject.creatingUserID != getLoggedInUserId()){
                return Forbid();
            }

            if(parentComponentID != null){
                parentComponent = await Context.projectComponent.FindAsync(parentComponentID);                
                if(parentComponent == null){
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
        public async Task<IActionResult> OnPostAsync(int parentProjectID, int? parentComponentID)
        {            
            if(parentComponentID != null){
                parentComponent = await Context.projectComponent.FindAsync(parentComponentID);
                if(parentComponent == null){
                    return NotFound();
                }
            }

            currentProject = await Context.project.FindAsync(parentProjectID);     
            if(currentProject == null){
                return NotFound();
            }
            if(currentProject.creatingUserID != getLoggedInUserId()){
                return Forbid();
            }            
            
            projectComponentVM.projectComponentID = parentComponentID;

            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            // TODO: potentially refactor the creation code across entities (project, project component, project update)            
            var projectComponentEntry = Context.Add(new projectComponent());
            projectComponentEntry.CurrentValues.SetValues(projectComponentVM);
            
            // Items to be set automatically: Creation user, creation date, parent project Id, based on the url passed in.
            {
                projectComponentEntry.Entity.projectID = parentProjectID;
                projectComponentEntry.Entity.creatingUserID = getLoggedInUserId();
                projectComponentEntry.Entity.creationDate = DateTime.Now;
                if(parentComponentID == null){
                    projectComponentEntry.Entity.nodeDepth = 0;
                }else{
                    projectComponentEntry.Entity.nodeDepth = parentComponent.nodeDepth + 1;
                    projectComponentEntry.Entity.projectComponentID = parentComponentID;
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
