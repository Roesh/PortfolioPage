using System;
using System.ComponentModel.DataAnnotations;


namespace PortfolioPage.Models
{
    public abstract class eventHistory
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime eventDate { get; set; }
        public bool isLatest { get; set; }

    }
}