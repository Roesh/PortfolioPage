using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Collections.Generic;
using PortfolioPage.Models;

namespace PortfolioPage.Models
{
    
    public class issueResponse
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public int issueID { get; set; }        
        
        [Required]
        [Display(Name="Response text")]         
        [StringLength(magicNumbers.maxDescriptionLength, MinimumLength = magicNumbers.minDescriptionLength,  ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string responseText { get; set; }
                
        public string creatingUserID {get; set;}
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime creationDate { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? lastEditDate { get; set; }
        
        public string lastEditUser { get; set; }
    }
    
    public class issueResponseViewModel{
        [Required]
        int ID;

        [Required]
        [Display(Name="Response text")]         
        [StringLength(magicNumbers.maxDescriptionLength, MinimumLength = magicNumbers.minDescriptionLength,  ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string responseText { get; set; }     
    }
        
}