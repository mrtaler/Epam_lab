using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;

namespace TicketSaleCore
{
    public class FeatureConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var featureName = GetFeatureName(controller.ControllerType);
            controller.Properties.Add("feature", featureName);
        }

        private string GetFeatureName(TypeInfo controllerType)
        {
            string[] tokens = controllerType.FullName.Split('.');
            if(tokens.All(t => t != "Features"))
                return "";
            string featureName = tokens
                .SkipWhile(t => !t.Equals("features", StringComparison.CurrentCultureIgnoreCase))
                .Skip(1)
                .Take(1)
                .FirstOrDefault();

            return featureName;
        }
    }


    public class FeatureViewLocationExpander : IViewLocationExpander
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
    public static class RazorExtensions
    {
        public static void ConfigureFeatureFolders(this RazorViewEngineOptions options)
        {
            // {0} - Action Name
            // {1} - Controller Name
            // {2} - Area Name
            // {3} - Feature Name
            options.ViewLocationFormats.Clear();
            options.ViewLocationFormats.Add("/Features/{3}/{1}/{0}.cshtml");
            options.ViewLocationFormats.Add("/Features/{3}/{1}/Views/{0}.cshtml");

            options.ViewLocationFormats.Add("/Features/{3}/{0}.cshtml");
            options.ViewLocationFormats.Add("/Features/{3}/Views/{0}.cshtml");
            options.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");

            //options.ViewLocationFormats.Add("/Features/{3}/{0}.cshtml");
            //options.ViewLocationFormats.Add("/Features/{3}/Views/{0}.cshtml");
            //options.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");

            options.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
        }




    }
    ////public class FeatureViewLocationExpander : IViewLocationExpander
    ////{
    //    public IEnumerable<string> ExpandViewLocations(
    //        ViewLocationExpanderContext context,
    //        IEnumerable<string> viewLocations)
    //    {
    //        var controllerActionDescriptor = (context.ActionContext.ActionDescriptor as ControllerActionDescriptor);
    //        if(controllerActionDescriptor != null && controllerActionDescriptor.ControllerTypeInfo.FullName.Contains("Features"))
    //            return new List<string> { GetFeatureLocation(controllerActionDescriptor.ControllerTypeInfo.FullName) };
    //        return viewLocations;
    //    }
    //    private string GetFeatureLocation(string fullControllerName)
    //    {
    //        var words = fullControllerName.Split('.');
    //        var path = "";
    //        bool isInFeature = false;
    //        foreach(var word in words.Take(words.Count() - 1))
    //        {
    //            if(word.Equals("features", StringComparison.CurrentCultureIgnoreCase))
    //                isInFeature = true;
    //            if(isInFeature)
    //                path = System.IO.Path.Combine(path, word);
    //        }
    //        return System.IO.Path.Combine(path, "views", "{0}.cshtml");
    //    }
    //    public void PopulateValues(ViewLocationExpanderContext context)
    //    {
    //    }
    //}
}
