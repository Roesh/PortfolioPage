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
using PortfolioPage.Pages.ProjectTracker.Component;

namespace PortfolioPage.Pages.ProjectTracker.Project
{
    public class DeleteModel : DI_BasePageModel
    {

        public DeleteModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public project project { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            project = await Context.project.FirstOrDefaultAsync(m => m.ID == id);

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

            project = await Context.project.FindAsync(id);
            
            if (project == null){
                return NotFound();
            }
            // TODO: Move this to authorization folder
            if(project.creatingUserID != getLoggedInUserId()){
                return Forbid();
            }

            Context.project.Remove(project);
            var firstLevelComponents = (from pc in Context.projectComponent 
                                        where pc.projectID == project.ID && pc.nodeDepth == 0
                                        select pc).ToList();

            foreach(var component in firstLevelComponents){
                var dm = new Component.DeleteModel(Context,AuthorizationService,UserManager);
                Context.Remove(component);
                dm.deleteChildComponents(component,Context);
            }
            await Context.SaveChangesAsync();

            return RedirectToPage("/ProjectTracker/MyProjects");
        }
    }
}
