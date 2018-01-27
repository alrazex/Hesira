using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Hesira.Helpers.Globalisation
{
    public class GlobalisationRouteHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            object culture;

            if (!requestContext.RouteData.Values.TryGetValue("culture", out culture) ||
                culture == null)
            {
                culture = LanguageHelper.DefaultLanguage;
            }

            var ci = new CultureInfo(MvcApplication.CulturesDictionary[culture.ToString()]);
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);

            return base.GetHttpHandler(requestContext);
        }
    }
}