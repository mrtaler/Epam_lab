

using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TicketSaleCore.TagHelper
{
    [HtmlElementName("th")]
    public class DisplayNameTagHelper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
    {
        private const string DisplayForAttributeName = "display-for";

        [HtmlAttributeName(DisplayForAttributeName)]
        public string DisplayFor
        {
            get; set;
        }

     //   [Activate]
        protected internal ViewContext ViewContext
        {
            get; set;
        }

      //  [Activate]
        protected internal IModelMetadataProvider MetadataProvider
        {
            get; set;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if(string.IsNullOrEmpty(DisplayFor))
                return;

            var modelMetadata = ViewContext.ViewData.ModelMetadata;
            var model = ViewContext.ViewData.Model;
            if(modelMetadata.IsCollectionType)
            {
                foreach(var elementType in modelMetadata.RealModelType.GetGenericArguments())
                {
                    var props = MetadataProvider.GetMetadataForProperties(model, elementType);
                    var chosen = props.SingleOrDefault(p => p.PropertyName == DisplayFor);
                    if(chosen != null)
                    {
                        output.Content = chosen.GetDisplayName();
                        return;
                    }
                }
                throw new ArgumentException($"DisplayNameTagHelper: Property {{{DisplayFor}}} is not found on {model.ToString()}");
            }
        }
    }
}
