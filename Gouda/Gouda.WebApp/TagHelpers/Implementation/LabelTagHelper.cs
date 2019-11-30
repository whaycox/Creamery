using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Gouda.WebApp.TagHelpers.Implementation
{
    using Application.DeferredValues.Abstraction;
    using Application.DeferredValues.Domain;

    [HtmlTargetElement("gouda-label")]
    public class LabelTagHelper : TagHelper
    {
        private ILabelDeferredValue LabelDeferredValue { get; }

        public LabelDeferredKey Key { get; set; }

        public LabelTagHelper(ILabelDeferredValue labelDeferredValue)
        {
            LabelDeferredValue = labelDeferredValue;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.SelfClosing;
            output.SuppressOutput();
            output.Content.Append(LabelDeferredValue[Key]);
        }
    }
}
