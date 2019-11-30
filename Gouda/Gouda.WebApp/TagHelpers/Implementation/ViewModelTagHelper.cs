using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Gouda.WebApp.TagHelpers.Implementation
{
    using Application.Abstraction;

    [HtmlTargetElement("gouda-viewModel")]
    public class ViewModelTagHelper : TagHelper
    {
        private IViewComponentHelper ViewComponentHelper { get; }
        private IViewContextAware ViewContextAware { get; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public IViewModel ViewModel { get; set; }

        public ViewModelTagHelper(IViewComponentHelper viewComponentHelper)
        {
            ViewComponentHelper = viewComponentHelper;
            ViewContextAware = (IViewContextAware)viewComponentHelper;
        }

        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            ViewContextAware.Contextualize(ViewContext);
            IHtmlContent html = await ViewComponentHelper.InvokeAsync(ViewModel.ViewConcept, ViewModel);

            output.TagMode = TagMode.SelfClosing;
            output.SuppressOutput();
            output.Content.SetHtmlContent(html);
        }
    }
}
