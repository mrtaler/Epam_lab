using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TicketSaleCore.CustomTagHelper
{

    [HtmlTargetElement(
        "SemanticUi", 
        Attributes = ("StLabel"), 
        TagStructure = TagStructure.WithoutEndTag)]
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
            var labelSb=new StringBuilder();
            labelSb.Append("<div class='label'>");
            labelSb.Append(Label);
            labelSb.Append("</div>");


            var valueSb=new StringBuilder();
            valueSb.Append( "<div class='value'>");
            valueSb.Append(Value);
            valueSb.Append("</div>");

            labelSb.Append(valueSb);

            output.TagName = "div";
            output.TagMode=TagMode.StartTagAndEndTag;
            output.Attributes.RemoveAll("StLabel");


            output.Attributes.Add("class", "ui statistic");

            output.Content.SetHtmlContent(labelSb.ToString());
        }
    }
}
