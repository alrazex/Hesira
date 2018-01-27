using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Hesira.Helpers.Globalisation;

namespace Hesira.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {


            string[] cultures = LanguageHelper.Cultures;
            string[] languages = LanguageHelper.Languages;

            HttpContext.Current.Application.Add("languages", languages);

            MvcApplication.CulturesDictionary = new Dictionary<string, string>();
            for (int i = 0; i < languages.Length; i++)
            {
                MvcApplication.CulturesDictionary.Add(languages[i], cultures[i]);
            }

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapGlobalisationRoutes(
                            "Default",
                            "{controller}/{action}",
                            new { controller = "Home", action = "Index", culture = "ro" },
                            null,
                            new[] { "Hesira.Controllers" },
                            null
                        );

            foreach (Route r in routes.Where(r => (r as Route) != null && (r as Route).RouteHandler is MvcRouteHandler))
            {
                r.RouteHandler = new GlobalisationRouteHandler();
            }


        }
    }
}
