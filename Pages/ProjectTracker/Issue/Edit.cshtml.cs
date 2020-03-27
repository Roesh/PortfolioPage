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
using Microsoft.EntityFrameworkCore;
using PortfolioPage.Pages.ProjectTracker.Component;

namespace PortfolioPage.Pages.ProjectTracker.Issue
{
    public class EditModel : DI_BasePageModel
    {
        public EditModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public projectIssueViewModel IssueVM { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {       
            var issue = await Context.issue.FirstOrDefaultAsync(m => m.ID == id);
            if(issue == null){
                return NotFound();
            }
            
            if(issue.projectComponentID != null){
                currentComponent = await GetComponentAsync((int)issue.projectComponentID, true, true, true);
            }            
            currentProject = await GetProjectAsync((int)issue.projectID, true, true, true);

            // AUTHORIZATION
            if(issue.creatingUserID != getLoggedInUserId()){
                return Forbid();
            }            
            
            IssueVM = new projectIssueViewModel();
            {
                IssueVM.issueTitle = issue.issueTitle;
                IssueVM.issueText = issue.issueText;
                IssueVM.projectComponentID = issue.projectComponentID;
                IssueVM.projectID = issue.projectID;
                IssueVM.projectIssuePriority = issue.projectIssuePriority;
                IssueVM.projectIssueType = issue.projectIssueType;
            }
            
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if(id == null){
                return NotFound();
            }
            var issue = await Context.issue.FirstOrDefaultAsync(m => m.ID == id);

            if(issue == null){
                return NotFound();
            }
            // AUTHORIZATION
            if(issue.creatingUserID != getLoggedInUserId()){
                return Forbid();
            }   

            if(!ModelState.IsValid){
                return Page();
            }
           
            var issueEntry = Context.Attach(issue);
            issueEntry.CurrentValues.SetValues(IssueVM);

            ModelState.Clear();
                        
            if (!TryValidateModel(issueEntry.Entity))
            {
                return Page();
            }

            await Context.SaveChangesAsync();

            return RedirectToPage("/ProjectTracker/MyProjects");
        }
    }
}
