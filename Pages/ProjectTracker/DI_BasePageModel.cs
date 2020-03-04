using PortfolioPage.Data;
using PortfolioPage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioPage.Pages.ProjectTracker
{
    public class DI_BasePageModel : PageModel
    {
        protected ApplicationDbContext Context { get; }
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<IdentityUser> UserManager { get; }

        public IList<project> projectList { get; set; }
        // Used to temporarily store projects and be referenced by partial views
        public project currentProject {get; set; } 

        public DI_BasePageModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager) : base()
        {
            Context = context;
            UserManager = userManager;
            AuthorizationService = authorizationService;
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
        public bool viewingUserCanEdit = false;
        public bool viewingUserCanDelete = false;
        public bool viewingUserCanViewDetails = true;

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
    }
}
