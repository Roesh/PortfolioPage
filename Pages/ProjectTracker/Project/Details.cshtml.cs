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
            Project = await Context.project.FirstOrDefaultAsync(m => m.ID == id);
            projectList = new List<project>();
            projectList.Add(Project);

            if (Project == null)
            {
                return NotFound();
            }

            var isPublic = Project.isPublic;
            if (!isPublic && Project.creatingUserID != getLoggedInUserId())
            {
                return Forbid();
            }
            
            componentList = (from pc in Context.projectComponent
                                where pc.projectID == id && pc.nodeDepth == 0
                                select pc)
                                .Include(pc => pc.childComponents)
                                .Include(pc => pc.projectUpdates)
                                .ToList();

            // AUTHORIZATION
            if(componentList.FirstOrDefault()?.creatingUserID == getLoggedInUserId()){
                viewingUserCanDelete = true;
                viewingUserCanEdit = true;
            }
            return Page();
        }
    }
}
