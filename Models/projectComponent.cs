using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using PortfolioPage.Models;

namespace PortfolioPage.Models
{
    public class projectComponent
    {
        public int ID { get; set; }

        public int projectID { get; set; } // how to represent a many to one relationship? Or a one to many relationship?
        // Assuming one to one relationship is mapped via class name and one to many is via the ICollections method.
        [StringLength(magicNumbers.maxProjectTitleLength,  ErrorMessage = magicNumbers.errorMessage_maxProjectTitleLength)]
        public string title {get; set;}
        public string description { get; set; }

        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? completionDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime completionDeadline { get; set; }
        public ICollection<projectCompletionDeadlineHistory> componentDeadlineHistory { get; set; }

    }

    public class projectComponentCompletionDeadlineHistory : eventHistory
    {
        public int projectComponentID;
        
        [DataType(DataType.Date)]
        public DateTime newCompletionDeadline;

    }
}