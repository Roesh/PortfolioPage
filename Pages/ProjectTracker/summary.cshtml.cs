using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace PortfolioPage.ProjectTracker.Pages
{
    public class summaryModel : PageModel
    {
        private readonly ILogger<summaryModel> _logger;

        public summaryModel(ILogger<summaryModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
        }
    }
}
