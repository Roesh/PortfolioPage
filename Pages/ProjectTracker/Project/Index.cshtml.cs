using PortfolioPage.Authorization;
using PortfolioPage.Data;
using PortfolioPage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioPage.Pages.ProjectTracker.Project
{
    [AllowAnonymous]
    public class IndexModel : DI_BasePageModel
    {
        public IndexModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public string currentUserId;     
        public bool RequestedUserFound = true;  
        public string requestedUserName = "";  

        public async Task OnGetAsync(string user)
        {
            this.requestedUserName = user;
            string requestedUserId = "";
            
            if(string.IsNullOrEmpty(requestedUserId 
                = getUserIdFromUserName(requestedUserName)))
            {
                RequestedUserFound = false;
            }

            else{                
                var projects = from c in Context.project
                            where c.creatingUserID == requestedUserId 
                            && c.isPublic
                            select c;
                
                projectList = await projects.ToListAsync();
            }
        }
    }
}