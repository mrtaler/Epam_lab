using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Reflection;

namespace TicketSaleCore.CustomTagHelper
{
    /// <summary>
    /// DisplayName for table
    /// </summary>
    [HtmlTargetElement("th")]
    public class DisplayNameTagHelper : TagHelper
    {
        public DisplayNameTagHelper(IModelMetadataProvider metadataProvider)
        {
            MetadataProvider = metadataProvider;
        }
        private const string DisplayForAttributeName = "display-for";
        [HtmlAttributeName(DisplayForAttributeName)]
        public string DisplayFor
        {
            get; set;
        }

        [ViewContext]
        public ViewContext ViewContext
        {
            get; set;
        }

        protected internal IModelMetadataProvider MetadataProvider
        {
            get; set;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if(output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            var modelMetadata = ViewContext.ViewData.ModelMetadata;

            if(DisplayFor == null)
            {
                return;
            }

            foreach(var elementType in modelMetadata.ModelType.GetGenericArguments())
            {
                var props = MetadataProvider.GetMetadataForProperties(elementType);
                var chosen = props.SingleOrDefault(p => p.PropertyName.Equals(DisplayFor, StringComparison.OrdinalIgnoreCase));
                if(chosen != null)
                {
                    output.Content.SetContent(chosen.GetDisplayName());
                    return;
                }
            }
        }

    }
}
