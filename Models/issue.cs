using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Collections.Generic;
using PortfolioPage.Models;

namespace PortfolioPage.Models
{
    
    public class issue
    {
        public enum issueType{
            [Display(Name="Enhancement Request")]
            enhancementRequest,
            [Display(Name="Performance issue")]
            performance,
            [Display(Name="Functionality issue")]
            Functionality
            
        }
        public enum issueStatus{
            [Display(Name="Open")]
            Open,
            [Display(Name="Closed")]
            Closed,
            [Display(Name="In Progress")]
            inProgress
        }
        public enum issuePriority{
            [Display(Name="Low")]
            Low,
            [Display(Name="Medium")]
            Medium,
            [Display(Name="High")]
            High
        }

        [Required]       
        public int ID { get; set; }
        
        [Required]
        [Display(Name="Issue title")]         
        [StringLength(magicNumbers.maxProjectTitleLength, MinimumLength = magicNumbers.minProjectTitleLength,  ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string issueTitle { get; set; }
        
        [Required]
        [Display(Name="Issue text")]
        [StringLength(magicNumbers.maxDescriptionLength, MinimumLength = magicNumbers.minDescriptionLength,  ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string issueText {get; set;}

        // user ID from AspNetUser table.
        [Required]
        public string creatingUserID {get; set;}
                
        public string assignedUsedID {get; set;}

        [Required]
        [Display(Name="Issue type")]
        [EnumDataType(typeof(issueType))]
        public issueType projectIssueType { get; set; }
        
        [Required]
        [Display(Name="Issue status")]
        [EnumDataType(typeof(issueStatus))]
        public issueStatus projectIssueStatus { get; set; }
        
        [Required]
        [Display(Name="Issue priority")]
        [EnumDataType(typeof(issuePriority))]
        public issuePriority projectIssuePriority { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime issueCreationDate{ get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? lastEditDate { get; set; }

        // TODO: See if we can make the requirement here that either projectID or projectcompoenntID must exist        
        public int? projectID {get; set; }
        
        public int? projectComponentID {get; set;}
    }
    
    public class projectIssueViewModel{
        [Required]
        int ID;

        [Required]
        [Display(Name="Issue title")]         
        [StringLength(magicNumbers.maxProjectTitleLength, MinimumLength = magicNumbers.minProjectTitleLength,  ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string issueTitle { get; set; }
        
        [Required]
        [Display(Name="Issue text")]
        [StringLength(magicNumbers.maxDescriptionLength, MinimumLength = magicNumbers.minDescriptionLength,  ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string issueText {get; set;}

        [Required]
        [Display(Name="Issue type")]
        [EnumDataType(typeof(issue.issueType))]
        public issue.issueType projectIssueType { get; set; }

        [Required]
        [Display(Name="Issue priority")]
        [EnumDataType(typeof(issue.issuePriority))]
        public issue.issuePriority projectIssuePriority { get; set; }
        
        public int? projectID {get; set; }
        
        public int? projectComponentID {get; set;}
    }

    public class issuePriorityHistory{
        [Required]
        public int ID {get; set; }

        [Required]
        public int issueID {get; set; }

        [Required]
        public issue.issuePriority pastIssuePriority {get; set; }

        // Time / space tradeoff: Data duplication, but makes it quicker to get the new status        
        [Required]
        public issue.issuePriority newIssuePriority {get; set; } 

        [Required]
        public DateTime priorityChangeDate {get; set; }
        
        [Required]
        public string updatingUserID {get; set; }

    }

    public class issueStatusHistory{
        [Required]
        public int ID {get; set; }

        [Required]
        public int issueID {get; set; }

        [Required]
        public issue.issueStatus pastIssueStatus {get; set; }
        
        [Required]
        public issue.issueStatus newIssueStatus {get; set; }

        [Required]
        public DateTime statusChangeDate {get; set; }
                
        [Required]
        public string updatingUserID {get; set; }

    }

}