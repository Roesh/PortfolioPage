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
        private readonly ILogger<IndexModel> _logger;

        public summaryModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
        }
    }
}
