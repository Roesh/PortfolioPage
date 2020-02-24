using System;
using System.ComponentModel.DataAnnotations;

namespace PortfolioPage.Models
{
    
    public class projectUpdate
    {
        public enum updateType{
            roadBlock,
            newSolution,
            issue,
            note
        }

        public int ID { get; set; }
        
        [StringLength(magicNumbers.maxProjectTitleLength,  ErrorMessage = magicNumbers.errorMessage_maxProjectTitleLength)]
        public string updateTitle { get; set; }
        
        [StringLength(magicNumbers.maxDescriptionLength,  ErrorMessage = magicNumbers.errorMessage_maxDescriptionLength)]
        public string updateText {get; set;}

        [EnumDataType(typeof(updateType))]
        public updateType projectUpdateType { get; set; }

        [DataType(DataType.Date)]
        public DateTime updateDate{ get; set; }

        [DataType(DataType.Date)]
        public DateTime lastEditDate { get; set; }        
    }
    
    //TODO: Optimize this:
    public class projectUpdateEditHistory : eventHistory{
        public int projectUpdateID;
            
        [DataType(DataType.Date)]
        public DateTime updateDate;
        
        [StringLength(magicNumbers.maxProjectTitleLength,  ErrorMessage = magicNumbers.errorMessage_maxProjectTitleLength)]
        public string updateTitle { get; set; }
        
        [StringLength(magicNumbers.maxDescriptionLength,  ErrorMessage = magicNumbers.errorMessage_maxDescriptionLength)]
        public string updateText {get; set;}

        [EnumDataType(typeof(projectUpdate.updateType))]
        public projectUpdate.updateType projectUpdateType { get; set; }
    }
}