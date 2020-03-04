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

            if (Project == null)
            {
                return NotFound();
            }

            var isPublic = Project.isPublic;

            if (!isPublic)
            {
                return Forbid();
            }

            return Page();
        }
    }
}
