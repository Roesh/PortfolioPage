using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Collections.Generic;

namespace PortfolioPage.Models
{    
    public static class magicNumbers{
        public const int maxProjectTitleLength = 150;
        public const int minProjectTitleLength = 3;
        public const int maxDescriptionLength = 5000;        
        public const int minDescriptionLength = 10;
        
    }

    public class ProjectEditHelper{
        private project project;

        public void saveProjectState(project project){
            this.project = project;
        }

        public project addNullProjectItemsFromSaved(project updatedProject){
            // Set necessary for this entity here
            if(updatedProject.creatingUserID == null){
                updatedProject.creatingUserID = project.creatingUserID;
            }
            if(updatedProject.projectDeadlineHx == null){
                updatedProject.creatingUserID = project.creatingUserID;
            }
            
            return updatedProject;
        }

        public static project addInitialDefaultValues(project project, DateTime creationDate, string creatingUserID){
            project.creationDate = creationDate;
            project.creatingUserID = creatingUserID;
            return project;
        }

    }

    
    public class project
    {
        public enum projectStatus{
            [Display(Name="On Track")]
            onTrack,
            [Display(Name="Behind")]
            behind,
            [Display(Name="Complete")]
            complete
        }

        public enum projectPhase{
            Initiation,
            Planning,
            Execution,
            Monitoring,
            Closure
        }
    
        [Required]
        public int ID { get; set; }

        // user ID from AspNetUser table.
        [Required]
        public string creatingUserID {get; set;}
        
        [Required]
        [DisplayName("Project Title")]         
        [StringLength(magicNumbers.maxProjectTitleLength, MinimumLength = magicNumbers.minProjectTitleLength,  ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string title { get; set; }

        [Required]
        [StringLength(magicNumbers.maxDescriptionLength, MinimumLength = magicNumbers.minDescriptionLength,  ErrorMessage = "{0} length must be between {2} and {1}.")]
        [DisplayName("Project Goal Description")] 
        public string goalDescription { get; set; }

        [Required]
        [Description("The date that work will begin on this project")]     
        [DisplayName("Project Start Date")] 
        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }

        [Required]
        [Description("The date this project was created by the user")]     
        [DisplayName("Project Creation Date")] 
        [DataType(DataType.Date)]
        public DateTime creationDate { get; set; }
        
        [Description("The date this project was completed")]     
        [DisplayName("Completion Date")] 
        [DataType(DataType.Date)]        
        public DateTime? completionDate { get; set; }

        [Required]
        [Description("The date this project is due")]     
        [DisplayName("Completion Deadline")]     
        [DataType(DataType.Date)]
        public DateTime completionDeadline { get; set; }
        public ICollection<projectCompletionDeadlineHistory> projectDeadlineHx {get; set;}
        
        [Description("All the components and their children that are a part of this project")]
        [DisplayName("Project Components")]
        public ICollection<projectComponent> components {get; set;}

        [Required]
        [DisplayName("Status")]
        [Description("The current status of this project")]
        [EnumDataType(typeof(projectStatus))]
        public projectStatus currentProjectStatus{get; set;}
        public ICollection<projectStatusHistory> projectStatusHx {get;set; }
                
        [Required]
        [DisplayName("Phase")]
        [Description("The current phase that this project is in")]
        [EnumDataType(typeof(projectPhase))]        
        public projectPhase currentProjectPhase{get; set;}
        
        [Required]
        [Description("Controls whether this project can be viewed by the public")]
        [DisplayName("Can the general public see this project's details?")]
        public bool isPublic{get; set;}

        public ICollection<projectUpdate> projectUpdates {get; set;}
        public ICollection<issue> projectIssues {get; set;}
    }

    public class projectViewModel
    {
        [Required]
        [DisplayName("Project Title")]         
        [StringLength(magicNumbers.maxProjectTitleLength, MinimumLength = magicNumbers.minProjectTitleLength,  ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string title { get; set; }

        [Required]
        [StringLength(magicNumbers.maxDescriptionLength, MinimumLength = magicNumbers.minDescriptionLength,  ErrorMessage = "{0} length must be between {2} and {1}.")]
        [DisplayName("Project Goal Description")] 
        public string goalDescription { get; set; }

        [Required]
        [Description("The date that work will begin on this project")]     
        [DisplayName("Project Start Date")] 
        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }
        
        [Description("The date this project was completed")]     
        [DisplayName("Completion Date")] 
        [DataType(DataType.Date)]        
        public DateTime? completionDate { get; set; }

        [Required]
        [Description("The date this project is due")]
        [DisplayName("Completion Deadline")]
        [DataType(DataType.Date)]
        public DateTime completionDeadline { get; set; }

        [Required]
        [DisplayName("Status")]
        [Description("The current status of this project")]
        [EnumDataType(typeof(project.projectStatus))]
        public project.projectStatus currentProjectStatus{get; set;}
                
        [Required]
        [DisplayName("Phase")]
        [Description("The current phase that this project is in")]
        [EnumDataType(typeof(project.projectPhase))]        
        public project.projectPhase currentProjectPhase{get; set;}
        
        [Required]
        [Description("Controls whether this project can be viewed by the public")]
        [DisplayName("Can the general public see this project's details?")]
        public bool isPublic{get; set;}
    }
    
    public class projectPhaseHistory : eventHistory
    {
        [Required]
        public int projectID;
        [Required]
        public project.projectPhase newPhase;
    }
    public class projectStatusHistory : eventHistory
    {
        [Required]
        public int projectID;
        [Required]
        public project.projectStatus newStatus;
    }
    public class projectCompletionDeadlineHistory : eventHistory
    {
        public int projectID;
        [Required]
        [DataType(DataType.Date)]
        public DateTime newCompletionDeadline;

    }
}