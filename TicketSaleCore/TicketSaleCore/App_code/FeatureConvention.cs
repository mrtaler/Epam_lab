using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace TicketSaleCore.App_code
{
    public class FeatureConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            controller.Properties.Add("feature", GetFeatureName(controller.ControllerType));
        }

        private static string GetFeatureName(TypeInfo controllerType)
        {
            var tokens = controllerType.FullName.Split('.');
            if(tokens.All(t => t != "Features"))
                return "";
            var featureName = tokens
            .SkipWhile(t => !t.Equals("features", StringComparison.CurrentCultureIgnoreCase))
            .Skip(1)
            .Take(1)
            .FirstOrDefault();

            return featureName;
        }
    }
    public class FeatureFoldersRazorViewEngine : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context,
              IEnumerable<string> viewLocations)
        {
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if(viewLocations == null)
            {
                throw new ArgumentNullException(nameof(viewLocations));
            }

            var controllerActionDescriptor = context.ActionContext.ActionDescriptor as ControllerActionDescriptor;
            if(controllerActionDescriptor == null)
            {
                throw new NullReferenceException("ControllerActionDescriptor cannot be null.");
            }

            string featureName = controllerActionDescriptor.Properties["feature"] as string;
            foreach(var location in viewLocations)
            {
                yield return location.Replace("{3}", featureName);
            }
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}
