using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSaleCore.App_code.CustomTagHelper
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("SemanticUi", Attributes = ("StLabel"))]
    public class SemanticUiStLabelTagHelper : TagHelper
    {
        [HtmlAttributeName("label-title")]
        public string Label
        {
            get; set;
        }
        [HtmlAttributeName("label-value")]
        public string Value
        {
            get; set;
        }

        /*
         <div class="ui statistic">
            <div class="label">
                available tikets:
            </div>
            <div class="value">
                @item.Tickets.Count(p => p.Order == null)
            </div>
         </div>
         */


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var panelHeadingContent = new TagBuilder("div");
            panelHeadingContent.Attributes.Add("class", "label");

            panelHeadingContent.InnerHtml.SetContent(Label);
           
            // header
            var panelHeading = new TagBuilder("div");
            panelHeading.AddCssClass("value");
            panelHeading.InnerHtml.SetContent(Value);



            output.TagName = "div";
            output.Attributes.RemoveAll("StLabel");

            output.Attributes.Add("class", "ui statistic");

           // var qq =new string(panelHeading,TagRenderMode.SelfClosing);
            var content = panelHeading.InnerHtml;
            //content += panelHeadingContent.InnerHtml.ToString();

           // output.Content.SetContent(content);

            //.ToHtmlString(TagRenderMode.Normal).ToString();


        }
    }
}
