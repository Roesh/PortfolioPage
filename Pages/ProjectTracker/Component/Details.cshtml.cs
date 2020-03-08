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

namespace PortfolioPage.Pages_ProjectTracker_Component
{
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
            //AUTHORIZATION
            if (projectComponent.creatingUserID != getLoggedInUserId())
            {
                return Forbid();
            }
            return Page();
        }
    }
}
