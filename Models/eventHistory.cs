using System;
using System.ComponentModel.DataAnnotations;


namespace PortfolioPage.Models
{
    public abstract class eventHistory
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime eventDate { get; set; }
        public bool isLatest { get; set; }

    }
}