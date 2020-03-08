using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortfolioPage.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PortfolioPage.Models;
using PortfolioPage.Pages;

namespace PortfolioPage.Pages.ProjectTracker.Project
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
        public projectViewModel projectVM { get; set; }

        public project project { get; set; }

        // TODO: Make this more efficient        
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            project = await Context.project.FirstOrDefaultAsync(m => m.ID == id);
            
            if (project == null)
            {
                return NotFound();
            }
            // TODO: Move this to authorization folder
            if(project.creatingUserID != getLoggedInUserId()){
                return Forbid();
            }

            projectVM = populateVitrualModelFromProject(project);
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {            
            var project = await Context.project.FindAsync(id);
                        
            if(project == null){
                return NotFound();
            }
            if(project.creatingUserID != getLoggedInUserId()){
                return Forbid(); 
            }

            // Make sure view model is valid first
            if (!ModelState.IsValid)
            {
                return Page();
            }

            
            // Update project entry with view model values            
            var projectEntry = Context.Attach(project);
            projectEntry.CurrentValues.SetValues(projectVM);
            
            ModelState.Clear();
            if (!TryValidateModel(projectEntry.Entity))
            {
                return Page();
            }
            // TODO: Drive actions after successfully saving data
            await Context.SaveChangesAsync();                
            
            return RedirectToPage("/ProjectTracker/MyProjects");
            
        }

        private projectViewModel populateVitrualModelFromProject(project project){
            projectViewModel projectVM = new projectViewModel();            
            projectVM.title = project.title;
            projectVM.goalDescription = project.goalDescription;
            projectVM.startDate = project.startDate;
            projectVM.completionDate = project.completionDate;
            projectVM.completionDeadline = project.completionDeadline;
            projectVM.currentProjectStatus = project.currentProjectStatus;
            projectVM.currentProjectPhase = project.currentProjectPhase;
            projectVM.isPublic = project.isPublic;
            return projectVM;
        }
    }
}
