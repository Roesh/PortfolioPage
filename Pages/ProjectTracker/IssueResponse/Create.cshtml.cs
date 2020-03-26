using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortfolioPage.Data;
using PortfolioPage.Models;
using PortfolioPage.Pages.ProjectTracker;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using PortfolioPage.Pages.ProjectTracker.Component;

namespace PortfolioPage.Pages.ProjectTracker.IssueResponse
{
    public class CreateModel : DI_BasePageModel
    {
        public CreateModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }
        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        
        public async Task<IActionResult> OnPostAsync(int IssueID, IFormCollection formFields)
        {
            string responseText = formFields["currentIssueResponseVM.responseText"];
            var issue = await Context.issue.FirstOrDefaultAsync(m => m.ID == IssueID);
            
            // AUTHORIZATION
            if(!(issue.assignedUserID == getLoggedInUserId() || issue.creatingUserID == getLoggedInUserId())){
                return Forbid();
            }

            var issueResponseEntry = Context.Add(new issueResponse());
            
            // Items to be set automatically
            {
                issueResponseEntry.Entity.responseText = responseText;
                issueResponseEntry.Entity.creationDate = DateTime.Now;
                issueResponseEntry.Entity.creatingUserID = getLoggedInUserId();
                issueResponseEntry.Entity.issueID = IssueID;
            }

            ModelState.Clear();
            
            if (!TryValidateModel(issueResponseEntry.Entity))
            {
                return Page();
            }

            await Context.SaveChangesAsync();

            return RedirectToPage("/ProjectTracker/MyProjects");
        }
    }
}
