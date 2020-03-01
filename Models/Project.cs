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
        public const String errorMessage_maxProjectTitleLength = "Title must be between 3 than 150 characters";

        public const int maxDescriptionLength = 5000;
        public const String errorMessage_maxDescriptionLength = "Description is too long";

    }

    // TODO: Add creation date separately
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

    

        public int ID { get; set; }

        // user ID from AspNetUser table.
        public string creatingUserID {get; set;}
        
        // TODO: Make sure project title is required, at least 3 characters. It doesn't seem to be working now
        [DisplayName("Project Title")] 
        [StringLength(magicNumbers.maxProjectTitleLength, MinimumLength = magicNumbers.minProjectTitleLength,  ErrorMessage = magicNumbers.errorMessage_maxProjectTitleLength)]
        public string title { get; set; }

        [DisplayName("Project Description")] 
        public string description { get; set; }

        [Description("The date that work will begin on this project")]     
        [DisplayName("Project Start Date")] 
        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }

        [Description("The date this project was created by the user")]     
        [DisplayName("Project Creation Date")] 
        [DataType(DataType.Date)]
        public DateTime creationDate { get; set; }

        [Description("The date this project was completed")]     
        [DisplayName("Completion Date")] 
        [DataType(DataType.Date)]        
        public DateTime? completionDate { get; set; }

        [Description("The date this project is due")]     
        [DisplayName("Completion Deadline")]     
        [DataType(DataType.Date)]
        public DateTime completionDeadline { get; set; }
        public ICollection<projectCompletionDeadlineHistory> projectDeadlineHx {get; set;}
        
        [Description("All the components and their children that are a part of this project")]
        [DisplayName("Project Components")]
        public ICollection<projectComponent> components {get; set;}

        [DisplayName("Status")]
        [Description("The current status of this project")]
        [EnumDataType(typeof(projectStatus))]
        public projectStatus currentProjectStatus{get; set;}
                
        [DisplayName("Phase")]
        [Description("The current phase that this project is in")]
        [EnumDataType(typeof(projectPhase))]        
        public projectPhase currentProjectPhase{get; set;}
        
        [Description("Controls whether this project can be viewed by the public")]
        [DisplayName("Can the general public see this project's details?")]
        public bool isPublic{get; set;}
    }
    public class projectPhaseHistory : eventHistory
    {
        public int projectID;
        public project.projectPhase newPhase;
    }
    public class projectStatusHistory : eventHistory
    {
        public int projectID;
        public project.projectStatus newStatus;
    }
    public class projectCompletionDeadlineHistory : eventHistory
    {
        public int projectID;
        
        [DataType(DataType.Date)]
        public DateTime newCompletionDeadline;

    }
}