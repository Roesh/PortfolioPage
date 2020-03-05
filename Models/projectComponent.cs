using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Collections.Generic;
using PortfolioPage.Models;

namespace PortfolioPage.Models
{
    public class projectComponent
    {
        public enum componentTypeEnum{
            Research,
            Action
        }
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public int projectID { get; set; }

        public int projectComponentID {get; set;}
        
        [Required]        
        [DisplayName("Title")]
        [StringLength(magicNumbers.maxProjectTitleLength, MinimumLength = magicNumbers.minProjectTitleLength,  ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string title {get; set;}

        [Required]
        [DisplayName("Component goal description")]
        [StringLength(magicNumbers.maxDescriptionLength, MinimumLength = magicNumbers.minDescriptionLength,  ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string componentDescription { get; set; }

        [Required]
        [DisplayName("Component Type")]
        public componentTypeEnum componentType {get; set;}
        
        [Required]
        public int nodeDepth {get;set;}

        [Required]
        [DisplayName("Start date")]
        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Completion date")]
        public DateTime? completionDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Completion deadline")]
        public DateTime completionDeadline { get; set; }

        [DisplayName("Completion deadline history")]
        public ICollection<projectComponentCompletionDeadlineHistory> componentDeadlineHistory { get; set; }
        
        [DisplayName("Component's associated updates")]
        public ICollection<projectUpdate> linkedProjectUpdates { get; set; }

        [DisplayName("component Status History ")]
        public ICollection<projectComponentStatusHistory> componentStatusHistory { get; set; }
    }

    public class projectComponentCompletionDeadlineHistory : eventHistory
    {
        public int projectComponentID;
        
        [DataType(DataType.Date)]
        public DateTime newCompletionDeadline;

    }
    public class projectComponentStatusHistory : eventHistory
    {
        public int projectComponentID;
        
        [DataType(DataType.Date)]
        public DateTime newCompletionDeadline;

    }
}