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
using PortfolioPage.Pages.ProjectTracker.Component;
using Microsoft.EntityFrameworkCore;

namespace PortfolioPage.Pages.ProjectTracker.Issue
{
    public class DeleteModel : DI_BasePageModel
    {
        public DeleteModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public projectIssueViewModel issueVM {get; set;}         
        public issue issue {get; set;}

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            issue = await Context.issue.FirstOrDefaultAsync(m => m.ID == id);
            if (issue == null)
            {
                return NotFound();
            }
            if(issue.creatingUserID != getLoggedInUserId()){
                return Forbid();
            }

            issueVM = new projectIssueViewModel();
            {
                issueVM.issueText = issue.issueText;
                issueVM.issueTitle = issue.issueTitle;
                issueVM.projectIssueType = issue.projectIssueType;
                issueVM.projectComponentID= issue.projectComponentID;
                issueVM.projectID = issue.projectID;
                issueVM.projectIssuePriority = issue.projectIssuePriority;
                issueVM.projectIssueType = issue.projectIssueType;                
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = Context.issue.FirstOrDefault(m => m.ID == id);
            if (issue == null)
            {
                return NotFound();
            }
            
            // AUTHORIZATION
            if(issue.creatingUserID != getLoggedInUserId()){
                return Forbid();
            }            
            
            Context.issue.Remove(issue);
            await Context.SaveChangesAsync();
            
            return RedirectToPage("/ProjectTracker/MyProjects");
        }
    }
}