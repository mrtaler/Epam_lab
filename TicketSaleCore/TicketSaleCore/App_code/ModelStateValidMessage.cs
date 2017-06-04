using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TicketSaleCore.App_code
{
    public static class ModelStateValidMessage
    {
        public static HtmlString ModelStateValidMsge(this IHtmlHelper html, ModelStateDictionary modelStateDictionary, IViewLocalizer errorLocalizer)
        {
            string result = string.Empty;
            if (modelStateDictionary.ErrorCount > 0)
            {
                StringBuilder strRes = new StringBuilder();

                var error = string.Empty;
                foreach (var item in modelStateDictionary)
                {
                    if (item.Value.ValidationState == ModelValidationState.Invalid)
                    {
                        strRes.Append("<li>");
                        var qq = item.Key.ToString();
                        var q1 = errorLocalizer[qq].Value;
                        var q2 = errorLocalizer[qq].Name;

                        strRes.Append(q1);
                        strRes.Append("</li>");
                    }

                }
                result = "<div class='ui error message'>"
                    + strRes.ToString()
                    + "</div>";
            }

            return new HtmlString(result);
        }
    }
}
