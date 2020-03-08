using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PortfolioPage.Data;
using PortfolioPage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using PortfolioPage.Pages.ProjectTracker;

namespace PortfolioPage.Pages.ProjectTracker.Component
{
    [AllowAnonymous]
    public class DetailsModel : DI_BasePageModel
    {

        public DetailsModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {           
        }

        public projectComponent projectComponent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            projectComponent = await Context.projectComponent.FirstOrDefaultAsync(m => m.ID == id);         
            
            if (projectComponent == null)
            {
                return NotFound();
            }
            parentComponent = projectComponent;
            componentList = (from pc in Context.projectComponent
                            where pc.projectComponentID == projectComponent.ID
                            select pc).Include(pc => pc.childComponents).ToList();
            //AUTHORIZATION
            bool loginUserIsComponentOwner = (projectComponent.creatingUserID == getLoggedInUserId());

            if (loginUserIsComponentOwner && 
                (await Context.project.FirstOrDefaultAsync
                (m => m.ID == projectComponent.projectID))
                .isPublic == false)
            {
                return Forbid();
            }
            if(loginUserIsComponentOwner){
                viewingUserCanDelete = true;
                viewingUserCanEdit = true;
            }
            
            return Page();
        }
    }
}
