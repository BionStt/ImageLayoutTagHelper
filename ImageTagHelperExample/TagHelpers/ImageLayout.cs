using System;
using System.Threading.Tasks;
using ImageTagHelperExample.Utilities;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ImageTagHelperExample.TagHelpers
{
    [HtmlTargetElement("image-layout")] 
    public class ImageLayout : TagHelper
    {
        [HtmlAttributeName("src")]
        public string ImageUrl { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        private readonly IHtmlHelper _html;

        public ImageLayout(IHtmlHelper helper)
        {
            _html = helper;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            // Contextualize the ViewContext
            ((IViewContextAware) _html).Contextualize(ViewContext);

            // Make sure we don't have any tags associated with this TagHelper.
            output.TagName = String.Empty;

            // DI'd into the constructor
            IHtmlContent content = null;

            // If we don't have an image, return the noImage.cshtml
            if (String.IsNullOrEmpty(ImageUrl))
            {
                content = await _html.PartialAsync("noimage");
            }
            else
            {
                var uri = new Uri(ImageUrl);
                var imageSize = ImageUtilities.GetWebDimensions(uri);

                // only 250px 
                if (imageSize.Width <= 300 && imageSize.Height <= 300)
                {
                    content = await _html.PartialAsync("smallimage", uri);
                } 
                // Image larger than 700px
                else if (imageSize.Width >= 700)
                {
                    content = await _html.PartialAsync("largeimage", uri);
                }
            }
            
            output.Content.SetHtmlContent(content);
        }
    }
}
