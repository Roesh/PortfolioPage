using System;
using System.ComponentModel.DataAnnotations;

namespace PortfolioPage.Models
{
    public class Project
    {
        public int ID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime ConceptionDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime CompletionDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime CompletionDeadline { get; set; }

        public int numberOfComponents { get; set; }
        public int currentProjectStatus{get; set;}
    }
}