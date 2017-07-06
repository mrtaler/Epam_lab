using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace TicketSaleCore.CustomTagHelper
{
    /// <summary>
    /// Tag Helper for language swith with Cookie
    /// </summary>
    [HtmlTargetElement("language-switcher")]
    public class LanguageSwitcherTagHelper : TagHelper
    {
        private readonly IOptions<RequestLocalizationOptions> locOptions;

        public LanguageSwitcherTagHelper(IOptions<RequestLocalizationOptions> options)
        {
            locOptions = options;
        }
        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext
        {
            get; set;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var cultureItems = locOptions.Value.SupportedUICultures
                //.Select(c => new SelectListItem { Value = c.Name, Text = c.DisplayName })
                .Select(c => new { Value = c.Name, Text = c.DisplayName,shValue =c.TwoLetterISOLanguageName,cul=c.NativeName})
                .ToList();
            output.TagName = null;

            var strBld = new StringBuilder();

            strBld.Append("<div class=\"ui icon top left pointing dropdown button\">");
                 strBld.Append("<i class='world icon'></i>");
                 strBld.Append("<div class='menu'>");
            foreach(var culture in cultureItems)
            {
                string flag="";
                switch (culture.Value)
                {
                    case "en": flag = "us"; break;
                    case "be": flag = "by"; break;
                    case "ru": flag = "ru"; break;
                }
                strBld.Append($"<div class='item' " +
                            $"onclick=\"useCookieToChangeLanguage('{culture.Value}')\">" +
                              $"<i class=\"{flag} flag\"></i>" +
                              $"{culture.Text}</div>");
            }
            strBld.Append("</div>");
            strBld.Append("</div>");


            strBld.Append("<script type='text/javascript'>");
            strBld.Append("$('.ui.dropdown')");
            strBld.Append(".dropdown()");

            strBld.Append(";</script>");
            output.Content.AppendHtml(strBld.ToString());
            
        }
    }

}
