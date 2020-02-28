using PortfolioPage.Authorization;
using PortfolioPage.Data;
using PortfolioPage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioPage.Pages.ProjectTracker
{
    /* This page is meant exclusively as a dashboard for users to view their own projects
    You have to be authenticated to view this page. Accessing this page without being logged in
    will redirect users to the login page

    TODO: Right now, there isn't a great way to let users know why they were redirected to the login page
    Is there a way to pass parameters to the prompt and display the reason they need to login?
    
    TODO:Right now the corresponding razor page displays all projects with no sorting. We may want
    to implement some sort of default sorting and separation and let users */
    public class MyProjectsModel : DI_BasePageModel
    {
        public MyProjectsModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        
        public string userName;

        public async Task OnGetAsync()
        {
            string currentUserId = UserManager.GetUserId(User);
            userName = User.Identity.Name;
            
            var projects = from c in Context.project
                        where c.creatingUserID == currentUserId
                        select c;

            projectList = await projects.ToListAsync();
            
        }
    }
}