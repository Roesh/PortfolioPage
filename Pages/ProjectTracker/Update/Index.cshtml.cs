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

namespace PortfolioPage.Pages.ProjectTracker.Update
{
    public class IndexModel : DI_BasePageModel
    {

        public IndexModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {           
        }

        public IList<projectUpdateViewModel> projectUpdateViewModel { get;set; }

        public async Task OnGetAsync()
        {   
            projectUpdateViewModel = new List<projectUpdateViewModel>();
            await foreach(var projectUpdate in Context.projectUpdate){              
                projectUpdateViewModel.Add(new Models.projectUpdateViewModel(){
                    updateText = projectUpdate.updateText,
                    updateTitle = projectUpdate.updateTitle,
                    projectUpdateType = projectUpdate.projectUpdateType
                });
            }
        }
    }
}
