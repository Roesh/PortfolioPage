using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PortfolioPage.Data;
using PortfolioPage.Models;
using PortfolioPage.Pages.ProjectTracker;

namespace PortfolioPage.Pages.ProjectTracker.Component
{
    public class DeleteModel : DI_BasePageModel
    {

        public DeleteModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
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
            // AUTHORIZATION
            if(projectComponent.creatingUserID != getLoggedInUserId()){
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

            projectComponent = await Context.projectComponent.FindAsync(id);

            if (projectComponent == null)
            {
                return NotFound();
            }
            // AUTHORIZATION
            if(projectComponent.creatingUserID != getLoggedInUserId()){
                return Forbid();
            }

            Context.projectComponent.Remove(projectComponent);
            //Handle other actions needed. Child project components will be orphaned, so we will 
            deleteChildComponents(projectComponent, this.Context);
            
            await Context.SaveChangesAsync();
            
            return RedirectToPage("./Index");
        }
        public void deleteChildComponents(projectComponent projectComponent, ApplicationDbContext Context){
            var children= getChildren(projectComponent);
            if(children.Any()){
                foreach(var pc in children){
                    Context.projectComponent.Remove(pc);
                    deleteChildComponents(pc,Context);
                }
            }
        }
        private IEnumerable<projectComponent> getChildren(projectComponent projectComponent){
            return (from pc in Context.projectComponent 
                    where pc.projectComponentID == projectComponent.ID
                    select pc).ToList();
        }
    }

}

    
