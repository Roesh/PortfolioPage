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

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var projectUpdate = await Context.projectUpdate.FirstOrDefaultAsync(m => m.ID == id);

            if (projectUpdate == null)
            {
                return NotFound();
            }
            if(projectUpdate.creatingUserID != getLoggedInUserId()){
                return Forbid();
            }
            {
                projectUpdateViewModel.projectUpdateType = projectUpdate.projectUpdateType;
                projectUpdateViewModel.updateText = projectUpdate.updateText ;
                projectUpdateViewModel.updateTitle= projectUpdate.updateTitle;
            }
            return Page();
        }
    }
}
