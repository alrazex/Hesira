using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hesira.Helpers.General
{
    public static class UrlHelpers
    {

        public static string ActionSecure(this UrlHelper instance, string actionName, string controllerName, object routeValues = null)
        {
            string area = string.Empty;
            bool hasArea = false;
            var requestDataTokens = instance.RequestContext.RouteData.DataTokens;

            if (routeValues != null)
            {
                var routeValuesType = routeValues.GetType();

                var routeValueArea = routeValuesType.GetProperty("area");

                if (routeValueArea != null)
                {
                    area = routeValueArea.GetValue(routeValues, null) as string;
                    hasArea = true;
                }
            }

            if (!hasArea)
            {
                area = (requestDataTokens.ContainsKey("area") ? requestDataTokens["area"] : string.Empty).ToString();
            }

            return instance.Action(actionName, controllerName, routeValues, "http");
        }

        
    }
}