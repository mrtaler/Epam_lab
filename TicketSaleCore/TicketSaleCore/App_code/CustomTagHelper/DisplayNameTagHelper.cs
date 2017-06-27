using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq.Expressions;
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
            var model = ViewContext.ViewData.Model;




            //if(modelMetadata.IsCollectionType)
            //{
            var qt = modelMetadata.ElementType;
            var qt1 = qt.GetRuntimeProperties();

            if(DisplayFor == null)
            {
                return;
            }

            //   ModelExplorer.ModelType;
            // var chosen = qt1.SingleOrDefault(p => p.Name == DisplayFor.ModelExplorer.Model);
            //if(chosen != null)
            //{
            //    output.Content.SetContent(chosen.Name);
            //    return;
            //}
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
            // }


            //var tagBuilder = Generator.GenerateSelect(
            //    ViewContext,
            //    DisplayFor.ModelExplorer,
            //    optionLabel: null,
            //    expression: DisplayFor.Name,
            //    selectList: items,
            //    currentValues: _currentValues,
            //    allowMultiple: _allowMultiple,
            //    htmlAttributes: null);

            //if(tagBuilder != null)
            //{
            //    output.MergeAttributes(tagBuilder);
            //    if(tagBuilder.HasInnerHtml)
            //    {
            //        output.PostContent.AppendHtml(tagBuilder.InnerHtml);
            //    }
            //}
        }

    }
}
