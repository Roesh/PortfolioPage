using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PortfolioPage.Data;
using PortfolioPage.Models;
using PortfolioPage.Authorization;

namespace PortfolioPage.Pages.ProjectTracker.Project
{
    [AllowAnonymous]
    public class DetailsModel : DI_BasePageModel
    {
        public DetailsModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            currentProject = await GetProjectAsync(id,true, true, true);
            
            if (currentProject == null)
            {
                return NotFound();
            }

            var isPublic = currentProject.isPublic;
            if (!isPublic && Project.creatingUserID != getLoggedInUserId())
            {
                return Forbid();
            }
            
            componentList = await (from pc in Context.projectComponent
                                where pc.projectID == id && pc.nodeDepth == 0
                                select pc)
                                .Include(pc => pc.childComponents)
                                .Include(pc => pc.projectUpdates)
                                .ToListAsync();

            // AUTHORIZATION
            if(componentList.FirstOrDefault()?.creatingUserID == getLoggedInUserId()){
                viewingUserCanDelete = true;
                viewingUserCanEdit = true;
            }
            return Page();
        }
    }
}
