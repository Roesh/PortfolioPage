using PortfolioPage.Data;
using PortfolioPage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PortfolioPage.Pages.ProjectTracker
{
    /* Base class for ProjectTracker pages (project, project Component, project updates, project issues) */
    public class DI_BasePageModel : PageModel
    {
        protected ApplicationDbContext Context { get; }
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<IdentityUser> UserManager { get; }

        public string maxCardWidth { get; set; }
        public string minCardWidth { get; set; }
        public string row5_ComponentSummary {get; set; }
        public string row6_IssueAndUpdateSummary {get; set; }
        public string row7_DescriptionView {get; set; }

        public IList<string> addedHeader { get; set; }
        public IList<project> projectList { get; set; }
        // Used to temporarily store projects and be referenced by partial views
        public project currentProject {get; set; } 

        public bool parentComponentCardDisplayed = false;
        public bool displaySingleProject = false;
        public bool displaySingleComponent = false;
        public projectComponent parentComponent;
        public projectComponent currentComponent {get; set; }
        public IList<projectComponent> componentList { get; set; }

        public IList<projectUpdate> updateList {get; set; }
        public projectUpdate currentUpdate {get; set; }

        
        public IList<issue> issueList {get; set; }
        public issue currentIssue {get; set; }
        
        public issueResponseViewModel currentIssueResponseVM {get; set; }

        public DI_BasePageModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager) : base()
        {
            Context = context;
            UserManager = userManager;
            AuthorizationService = authorizationService;
            minCardWidth = "440px";
            maxCardWidth = "50rem";

            row5_ComponentSummary = "row mt-2 px-2 py-2 border-bottom border-top";            
            row6_IssueAndUpdateSummary = "row px-2 py-2 border-bottom";
            //row7_DescriptionView = "row mt-2 px-2 py-2 text-info border-bottom border-top";            
        }        

        public string getLoggedInUserName(){
            return User.Identity.Name;
        }
        public string getLoggedInUserId(){
            return UserManager.GetUserId(User);
        }
        
        //TODO; see if this needs to move to the authorization folders somehow              
        // This is probably the correct spot for this though based on the fact that 
        // partial views reference this model for global configuration
        // This is all or none
        public bool viewingUserCanEdit = false;
        public bool viewingUserCanDelete = false;
        public bool viewingUserCanViewDetails = true;

        public string returnUrl;

        /*TODO: Write unit tests to test out individual methods. Investigate various methods of unit testing */
        public bool userIdExists(string userID){
            var user = (from u in UserManager.Users where u.Id == userID select u).Single();
            if(user == null){                
                return false;
            }
            return true;            
        }

        // Returns default string if user Id does not exist
        public string getUserNameFromUserID(string userID){
            // using the Single() method to convert to single variable - throwing an exception here if there is a list should occur because userID is a primary key 
            // and should NOT have duplicate rows
            return (from u in UserManager.Users where u.Id == userID select u.UserName).Single();             
        }

        // Returns default string if user name does not exist https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault?view=netframework-4.8
        public string getUserIdFromUserName(string userName){            
            // TODO: Make sure Identity is enforcing unique userNames. Using First() method to 
            // convert from list to value.
            return (from u in UserManager.Users
                    where u.UserName == userName
                    select u.Id).FirstOrDefault();
        }

        public SelectList projectComponentNameSL { get; set; }

        public void PopulateProjectComponentsDropDownList(ApplicationDbContext _context,
            int parentProject, int? parentComponent, object selectedProjectComponent = null)
        {
            var projectComponentQuery = from pc in _context.projectComponent
                                    where pc.projectID == parentProject
                                   select pc;

            projectComponentNameSL = new SelectList(projectComponentQuery.AsNoTracking(),
                        "projectComponentID", "Title", selectedProjectComponent);
        }

        public async Task<project> GetProjectAsync(int Id, bool includeComponents = false, bool includeUpdates = false, bool includeIssues = false){
            var query = (from p in Context.project where p.ID == Id select p);
            if(includeComponents){
                query = query.Include(p => p.components);
            }
            if(includeUpdates){
                query = query.Include(p => p.projectUpdates);
            }
            if(includeIssues){
                query = query.Include(p => p.projectIssues);
            }
            var project = await query.FirstOrDefaultAsync();
            return project;
        }

        public async Task<projectComponent> GetComponentAsync(int Id, bool includeChildren = false, bool includeUpdates = false, bool includeIssues = false){
            var query = (from c in Context.projectComponent where c.ID == Id select c);
            if(includeChildren){
                query = query.Include(c => c.childComponents);
            }
            if(includeUpdates){
                query = query.Include(c => c.projectUpdates);
            }
            if(includeIssues){
                query = query.Include(c => c.projectIssues);
            }
            var component = await query.FirstOrDefaultAsync();
            return component;
        }
    }
    
}
