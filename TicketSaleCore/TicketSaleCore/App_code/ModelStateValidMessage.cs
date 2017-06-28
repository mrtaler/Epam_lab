using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TicketSaleCore.App_code
{
    public static class ModelStateValidMessage
    {
        /// <summary>
        /// HTML Helper for show ModelState in list
        /// </summary>
        /// <param name="html"></param>
        /// <param name="modelStateDictionary">modelStateDictionary</param>
        /// <param name="errorLocalizer">state localizator</param>
        /// <returns></returns>
        public static HtmlString ModelStateValidMsge(
            this IHtmlHelper html, 
            ModelStateDictionary modelStateDictionary,
            IViewLocalizer errorLocalizer)
        {
            StringBuilder strRes = new StringBuilder();
            if (modelStateDictionary.ErrorCount > 0)
            {
                strRes.Append("<div class='ui error message'>");

                foreach (var item in modelStateDictionary)
                {
                    if (item.Value.ValidationState == ModelValidationState.Invalid)
                    {
                        strRes.Append("<li>");
                        var qq = item.Key;
                        var q1 = errorLocalizer[qq].Value;

                        strRes.Append(q1);
                        strRes.Append("</li>");
                    }
                    }
                strRes.Append("</div>");
            }
            return new HtmlString(
                strRes.ToString()
                );
        }
    }
}
