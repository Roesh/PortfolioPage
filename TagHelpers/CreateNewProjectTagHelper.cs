using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace PortfolioPage.TagHelpers
{
    public class CreateNewProjectTagHelper : TagHelper
    {
        private const string NewProjectHTML = "<i>New Project</i>";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            output.Attributes.SetAttribute("class", "col col-md-2 btn btn-success align-self-center ml-auto my-2");
            output.Attributes.SetAttribute("asp-page","/ProjectTracker/Project/Create");
            output.Content.SetHtmlContent(NewProjectHTML);
        }
    }
}