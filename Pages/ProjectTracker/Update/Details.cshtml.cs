using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PortfolioPage.Data;
using PortfolioPage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace PortfolioPage.Pages.ProjectTracker.Update
{
    public class DetailsModel : DI_BasePageModel
    {

        public DetailsModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public projectUpdateViewModel projectUpdateViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? ProjectID, int? ComponentID)
        {
            
            {
                bool projectIDProvided = (ProjectID != null);
                bool componentIDProvided = (ComponentID != null);

                if(!projectIDProvided && !componentIDProvided){
                    return NotFound(new String("ProjectID or ComponentID parameters required to view project updates"));
                }

                var updateListQuery = (from pu in Context.projectUpdate                                 
                                        select pu);

                if(componentIDProvided){
                    currentComponent = await Context.projectComponent.FindAsync(ComponentID);
                    if(currentComponent == null){
                        return NotFound(new String("Parent component ID argument not found"));
                    }
                    // AUTHORIZATION
                    if(currentComponent.creatingUserID != getLoggedInUserId()){
                        return Forbid();                        
                    }                    
                    currentProject = await Context.project.FindAsync(currentComponent.projectID);
                    
                    updateList = updateListQuery.Where(pu => pu.projectComponentID == currentComponent.ID)
                                    .ToList();;
                                    
                }
                
                else if(projectIDProvided){
                    currentProject = await Context.project.FindAsync(ProjectID);
                    if(currentProject == null){
                        return NotFound(new String("Parent project ID argument not found"));
                    }
                    // AUTHORIZATION
                    if(currentProject.creatingUserID != getLoggedInUserId()){
                        return Forbid();
                    }

                    updateList = updateListQuery.Where(pu => pu.projectID == currentProject.ID)
                                    .ToList();
                }
            }            
            viewingUserCanDelete = viewingUserCanEdit = true;
            return Page();
        }
    }
}
