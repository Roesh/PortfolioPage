using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using PortfolioPage.Data;
using PortfolioPage.Models;
using PortfolioPage.Pages.ProjectTracker;

namespace PortfolioPage.Pages_ProjectTracker_Component
{
    public class IndexModel : DI_BasePageModel
    {

        public IndexModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {            
        }

        public IList<projectComponent> projectComponent { get;set; }

        public async Task OnGetAsync()
        {
            // TODO: Access limited to master user
            projectComponent = await Context.projectComponent.ToListAsync();
        }
    }
}
