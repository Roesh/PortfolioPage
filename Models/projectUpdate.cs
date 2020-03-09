using System;
using System.ComponentModel.DataAnnotations;

namespace PortfolioPage.Models
{
    
    public class projectUpdate
    {
        public enum updateType{
            [Display(Name="Road block")]
            roadBlock,
            [Display(Name="New solution")]
            newSolution,
            [Display(Name="Issue")]
            issue,
            [Display(Name="Note")]
            note
        }

        [Required]       
        public int ID { get; set; }
        
        [Required]
        [Display(Name="Update Title")]         
        [StringLength(magicNumbers.maxProjectTitleLength, MinimumLength = magicNumbers.minProjectTitleLength,  ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string updateTitle { get; set; }
        

        [Required]
        [Display(Name="Update text")]         
        [StringLength(magicNumbers.maxDescriptionLength, MinimumLength = magicNumbers.maxDescriptionLength,  ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string updateText {get; set;}

        [Required]
        [Display(Name="Update type")]
        [EnumDataType(typeof(updateType))]
        public updateType projectUpdateType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime updateCreationDate{ get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? lastEditDate { get; set; }      

        [Required]
        public int projectID {get; set; }

        [Required]
        public int projectComponentID {get; set;}
    }
    
    public class projectUpdateViewModel{        
        int ID;
        [Required]
        [Display(Name="Update Title")]         
        [StringLength(magicNumbers.maxProjectTitleLength, MinimumLength = magicNumbers.minProjectTitleLength,  ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string updateTitle { get; set; }

        [Required]
        [Display(Name="Update text")]         
        [StringLength(magicNumbers.maxDescriptionLength, MinimumLength = magicNumbers.maxDescriptionLength,  ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string updateText {get; set;}

        [Required]
        [Display(Name="Update type")]
        [EnumDataType(typeof(projectUpdate.updateType))]
        public projectUpdate.updateType projectUpdateType { get; set; }
    }

    public class projectUpdateEditHistory : eventHistory{
        public int projectUpdateID;
            
        [DataType(DataType.Date)]
        public DateTime updateDate;
        
        [StringLength(magicNumbers.maxProjectTitleLength)]
        public string updateTitle { get; set; }
        
        [StringLength(magicNumbers.maxDescriptionLength)]
        public string updateText {get; set;}

        [EnumDataType(typeof(projectUpdate.updateType))]
        public projectUpdate.updateType projectUpdateType { get; set; }
    }
}