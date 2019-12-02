using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Gouda.WebApp.TagHelpers.Implementation
{
    using Application.Abstraction;
    using WebApp.Domain;
    using Application.ViewModels.Navigation.Abstraction;

    [HtmlTargetElement("gouda-viewModel")]
    public class ViewModelTagHelper : TagHelper
    {
        private IViewComponentHelper ViewComponentHelper { get; }
        private IViewContextAware ViewContextAware { get; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public string ID { get; set; }
        public string Class { get; set; }
        public IViewModel ViewModel { get; set; }

        public ViewModelTagHelper(IViewComponentHelper viewComponentHelper)
        {
            ViewComponentHelper = viewComponentHelper;
            ViewContextAware = (IViewContextAware)viewComponentHelper;
        }

        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            ViewContextAware.Contextualize(ViewContext);
            WebAppViewModel viewModel = WrapViewModel;
            IHtmlContent html = await ViewComponentHelper.InvokeAsync(viewModel.ViewConcept, viewModel);

            output.TagMode = TagMode.SelfClosing;
            output.SuppressOutput();
            output.Content.SetHtmlContent(html);
        }
        private WebAppViewModel WrapViewModel => new WebAppViewModel(ViewModel)
        {
            ID = ID,
            Class = Class,
        };
    }
}
