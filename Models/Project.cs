using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Collections.Generic;

namespace PortfolioPage.Models
{    
    public static class magicNumbers{
        public const int maxProjectTitleLength = 150;
        public const String errorMessage_maxProjectTitleLength = "Title cannot be longer than 150 characters.";

        public const int maxDescriptionLength = 5000;
        public const String errorMessage_maxDescriptionLength = "Description is too long";

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

    

        public int ID { get; set; }

        // user ID from AspNetUser table.
        public string creatingUserID {get; set;}

        [StringLength(magicNumbers.maxProjectTitleLength,  ErrorMessage = magicNumbers.errorMessage_maxProjectTitleLength)]
        public string title { get; set; }
        public string description { get; set; }

        [Description("The date this project was created")]     
        [DisplayName("Creation deadline")] 
        [DataType(DataType.Date)]
        public DateTime creationDate { get; set; }

        [Description("The date this project was completed")]     
        [DisplayName("Completion date")] 
        [DataType(DataType.Date)]        
        public DateTime? completionDate { get; set; }

        [Description("The date this project is due")]     
        [DisplayName("Completion deadline")]     
        [DataType(DataType.Date)]
        public DateTime completionDeadline { get; set; }
        public ICollection<projectCompletionDeadlineHistory> projectDeadlineHx {get; set;}
        
        public ICollection<projectComponent> components {get; set;}

        [DisplayName("Project Status")]        
        [Description("The current status of this project")]
        [EnumDataType(typeof(projectStatus))]
        public projectStatus currentProjectStatus{get; set;}
        
        
        [DisplayName("Project Phase")]
        [Description("The current phase that this project is in")]
        [EnumDataType(typeof(projectPhase))]        
        public projectPhase currentProjectPhase{get; set;}
        
        [Description("Controls whether this project can be viewed by the public")]
        [DisplayName("Is public?")]
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