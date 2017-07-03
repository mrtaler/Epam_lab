using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using System.Linq;

namespace TicketSaleCore
{
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
                .Select(c => new SelectListItem { Value = c.Name, Text = c.DisplayName })
                .ToList();



            output.TagName = null;

            var strBld = new StringBuilder();
            
            strBld.Append("<div class=\"ui floating dropdown labeled search icon button\">");
                 strBld.Append("<i class='world icon'></i>");
                 strBld.Append("<span class='text'>Select Language</span>");
                 strBld.Append("<div class='menu'>");
            foreach(var culture in cultureItems)
            {
                strBld.Append($"<div class='item' " +
                            //  $"data-value='{culture.Value}'" +
                              $"onclick=\"useCookieToChangeLanguage('{culture.Value}')\">{culture.Text}</div>");
               // output.Content.AppendHtml($"<li><a href='#' onclick=\"useCookie('{culture.TwoLetterISOLanguageName}')\">{culture.EnglishName}</a></li>");
            }
            strBld.Append("</div>");
            strBld.Append("</div>");


            strBld.Append("<script type='text/javascript'>");
            strBld.Append("$('.ui.dropdown')");
            strBld.Append(".dropdown('set selected','1');");
            strBld.Append("</script>");
            //   strBld.Append("<script type='text/javascript'>");


            output.Content.AppendHtml(strBld.ToString());
            /**
             *
             *                          
             */

            output.Content.AppendHtml(@"<script type='text/javascript'>
 function useCookieToChangeLanguage(code) {
 var culture = code;
 var uiCulture = code;
 var cookieValue = '" + CookieRequestCultureProvider.DefaultCookieName + @"=c='+code+'|uic='+code; 
 document.cookie = cookieValue; 
 window.location.reload();
 }
 </script>");
        }
    }

}
