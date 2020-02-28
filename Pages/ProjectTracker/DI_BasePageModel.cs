using PortfolioPage.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace PortfolioPage.Pages.ProjectTracker
{
    public class DI_BasePageModel : PageModel
    {
        protected ApplicationDbContext Context { get; }
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<IdentityUser> UserManager { get; }

        public DI_BasePageModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager) : base()
        {
            Context = context;
            UserManager = userManager;
            AuthorizationService = authorizationService;
        } 

        /*TODO: Write unit tests to test out individual methods. Investigate various methods of unit testing */
        public bool userIdExists(string userID){
            var user = (from u in UserManager.Users where u.Id == userID select u).Single();
            if(user == null){                
                return false;
            }
            return true;            
        }

        // Returns empty string if user Id does not exist
        public string getUserNameFromUserID(string userID){
            string userName = string.Empty;
            /* using the Single() method to convert to single variable - throwing an exception here is OK because
            userID is a primary key and should NOT have duplicate rows */
            userName = (from u in UserManager.Users where u.Id == userID select u.UserName).Single();
            return userName;
        }

        // Returns empty string if user name does not exist
        public string getUserIdFromUserName(string userName){
            string userId = string.Empty;
            
            // TODO: Make sure Identity is enforcing unique userNames. Using First() method to 
            // convert from list to sinvlesingle value.
            userId = (from u in UserManager.Users
                                where u.UserName == userName
                                select u.Id).First();             
            return userId;
        }
    }
}
