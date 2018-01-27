using BForms.Models;
using BForms.Mvc;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Hesira.App_Start;
using Hesira.Helpers.MVC;
using Hesira.Resources;

namespace Hesira
{
    public class MvcApplication : HttpApplication
    {

        protected internal static Dictionary<string, string> CulturesDictionary = new Dictionary<string, string>();

        protected void Application_Start()
        {

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewExtension());

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelValidatorProviders.Providers.Add(new BsModelValidatorProvider());
            BForms.Utilities.BsResourceManager.Register(Resource.ResourceManager);


            BForms.Utilities.BsUIManager.Theme(BsTheme.Purple);
        }
    }
}
