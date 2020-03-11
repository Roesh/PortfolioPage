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
using Microsoft.EntityFrameworkCore;

namespace PortfolioPage.Pages.ProjectTracker.Update
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
        public projectUpdateViewModel projectUpdateVM {get; set;}         
        public projectUpdate projectUpdate {get; set;}         

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            projectUpdate = await Context.projectUpdate.FirstOrDefaultAsync(m => m.ID == id);
            if (projectUpdate == null)
            {
                return NotFound();
            }
            if(projectUpdate.creatingUserID != getLoggedInUserId()){
                return Forbid();
            }

            projectUpdateVM = new projectUpdateViewModel();
            {
                projectUpdateVM.projectUpdateType = projectUpdate.projectUpdateType;
                projectUpdateVM.updateText = projectUpdate.updateText;
                projectUpdateVM.updateTitle= projectUpdate.updateTitle;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectUpdate = Context.projectUpdate.FirstOrDefault(m => m.ID == id);
            if (projectUpdate == null)
            {
                return NotFound();
            }
            // AUTHORIZATION
            if(projectUpdate.creatingUserID != getLoggedInUserId()){
                return Forbid();
            }            
            
            Context.projectUpdate.Remove(projectUpdate);
            await Context.SaveChangesAsync();
            
            return RedirectToPage("./Index");
        }
    }
}
