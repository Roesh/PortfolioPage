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
using Microsoft.EntityFrameworkCore;
using PortfolioPage.Pages.ProjectTracker.Component;

namespace PortfolioPage.Pages.ProjectTracker.Update
{
    public class EditModel : DI_BasePageModel
    {
        public EditModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public int? ProjectID;
        public int? ComponentID;

        [BindProperty]
        public projectUpdateViewModel projectUpdateVM { get; set; }

        public async Task<IActionResult> OnGetAsync(int projectUpdateID)
        {       
            var projectUpdate = await Context.projectUpdate.FirstOrDefaultAsync(m => m.ID == projectUpdateID);
            if(projectUpdate == null){
                return NotFound();
            }
            // AUTHORIZATION
            if(projectUpdate.creatingUserID != getLoggedInUserId()){
                return Forbid();
            }            
            {
                projectUpdateVM.projectUpdateType = projectUpdate.projectUpdateType;
                projectUpdateVM.updateText = projectUpdate.updateText;
                projectUpdateVM.updateTitle = projectUpdate.updateTitle;
            }
            
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? projectUpdateID)
        {
            if(projectUpdateID == null){
                return NotFound();
            }
            var projectUpdate = await Context.projectUpdate.FirstOrDefaultAsync(m => m.ID == projectUpdateID);

            if(projectUpdate == null){
                return NotFound();
            }
            // AUTHORIZATION
            if(projectUpdate.creatingUserID != getLoggedInUserId()){
                return Forbid();
            }   

            if(!ModelState.IsValid){
                return Page();
            }
           
            var projectUpdateEntry = Context.Add(new projectUpdate());
            projectUpdateEntry.CurrentValues.SetValues(projectUpdateVM);            
            
            ModelState.Clear();
            
            if (!TryValidateModel(projectUpdateEntry.Entity))
            {
                return Page();
            }

            await Context.SaveChangesAsync();

            return RedirectToPage("/ProjectTracker/MyProjects");
        }
    }
}
