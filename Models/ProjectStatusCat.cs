using System;
using System.ComponentModel.DataAnnotations;

namespace PortfolioPage.Models
{
    public class ProjectStatusCat
    {
        public int ID { get; set; }

        // On track, Behind, complete
        public string StatusTitle { get; set; }
    }
}