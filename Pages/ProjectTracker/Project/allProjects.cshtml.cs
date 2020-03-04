using PortfolioPage.Authorization;
using PortfolioPage.Data;
using PortfolioPage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioPage.Pages.ProjectTracker.Project
{
    public class allProjectsModel : DI_BasePageModel
    {
        public allProjectsModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        private const string masterUserName = "me@roshan.page";
        
        public async Task<IActionResult> OnGetAsync(){
            
            if(getLoggedInUserName() == masterUserName){
                var projects = from c in Context.project select c;
                projectList = await projects.ToListAsync();                            
                return Page();
            }
            return Forbid();
        }
    }
}