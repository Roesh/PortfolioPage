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
using System.Net.Http;
using System.Net;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using PortfolioPage.Pages.ProjectTracker.Component;

namespace PortfolioPage.Pages.ProjectTracker.Issue
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

        public int? ProjectID;
        public int? ComponentID;

        [BindProperty]
        public projectIssueViewModel IssueVM { get; set; }
        public async Task<IActionResult> OnGetAsync(int? ProjectID, int? ComponentID)
        {
            {
                bool projectIDProvided = (ProjectID != null);
                bool componentIDProvided = (ComponentID != null);

                if(!projectIDProvided && !componentIDProvided){
                    return NotFound(new String("ProjectID or ComponentID parameters required to create a project update"));
                }
                if(projectIDProvided){
                    currentProject = await GetProjectAsync((int)ProjectID, true, true, true);
                    if(currentProject == null){
                        return NotFound(new String("Parent component ID argument not found"));
                    }
                    // AUTHORIZATION
                    if(currentProject.creatingUserID != getLoggedInUserId()){
                        return Forbid();
                    }
                }
                if(componentIDProvided){
                    currentComponent = await GetComponentAsync((int)ComponentID,true,true,true);
                    if(currentComponent == null){
                        return NotFound(new String("Parent component ID argument not found"));
                    }
                    // AUTHORIZATION
                    if(currentComponent.creatingUserID != getLoggedInUserId()){
                        return Forbid();
                    }
                }
            }
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? ProjectID, int? ComponentID)
        {
            if(!ModelState.IsValid){
                return Page();
            }
            {
                bool projectIDProvided = (ProjectID != null);
                bool componentIDProvided = (ComponentID != null);
                if(!projectIDProvided && !componentIDProvided){
                    return NotFound(new String("ProjectID or ComponentID parameters required to open a new issue"));
                }
                if(componentIDProvided){
                    currentComponent = await GetComponentAsync((int)ComponentID, true, true, true);
                    if(currentComponent == null){
                        return NotFound(new String("Parent component ID argument not found"));
                    }
                    // AUTHORIZATION
                    if(currentComponent.creatingUserID != getLoggedInUserId()){
                        return Forbid();
                    }
                    ProjectID =  currentComponent.projectID;
                }
                else if(projectIDProvided){
                    currentProject = await Context.project.FindAsync(ProjectID);
                    if(currentProject == null){
                        return NotFound(new String("Parent component ID argument not found"));
                    }
                    // AUTHORIZATION
                    if(currentProject.creatingUserID != getLoggedInUserId()){
                        return Forbid();
                    }
                }
                
            }
 
            var issueEntry = Context.Add(new issue());
            issueEntry.CurrentValues.SetValues(IssueVM);
            
            // Items to be set automatically: Creation user, creation date, parent project Id, based on the url passed in.
            {
                issueEntry.Entity.issueCreationDate = DateTime.Now;
                issueEntry.Entity.projectID = ProjectID;
                issueEntry.Entity.projectComponentID = ComponentID;
                issueEntry.Entity.creatingUserID = getLoggedInUserId();
            }

            ModelState.Clear();
            
            if (!TryValidateModel(issueEntry.Entity))
            {
                return Page();
            }

            await Context.SaveChangesAsync();

            return RedirectToPage("/ProjectTracker/MyProjects");
        }
    }
}
