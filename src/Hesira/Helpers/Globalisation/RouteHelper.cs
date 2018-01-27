using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Hesira.Helpers.Globalisation
{
    public static class RouteHelper
    {

        public static bool ContainsRouteName(this RouteCollection routes, string name)
        {
            return routes[name] != null;
        }

        public static Route MapRoute(this RouteCollection routes, string name, string url, object defaults, RouteValueDictionary constraints, string[] namespaces, string area)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            if (constraints == null)
            {
                constraints = new RouteValueDictionary();
            }

            var route = new Route(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints),
                DataTokens = new RouteValueDictionary()
            };

            route.DataTokens["namespaces"] = namespaces;
            route.DataTokens["UseNamespaceFallback"] = false;

            if (area != null)
            {
                route.DataTokens["area"] = area;
            }

            routes.Add(name, route);

            return route;
        }

        public static void MapGlobalisationRoutes(this RouteCollection routes, string name, string url, object defaults,
            RouteValueDictionary constraints, string[] namespaces, string area)
        {
            string[] languages = LanguageHelper.Languages;
            string defaultLang = LanguageHelper.DefaultLanguage;

            foreach (var foreignLang in languages.Where(x => x != defaultLang))
            {
                var langConstraints = constraints != null
                    ? new RouteValueDictionary(constraints)
                    : new RouteValueDictionary();
                langConstraints.Add("culture", foreignLang);

                if (!routes.ContainsRouteName(name + foreignLang))
                {
                    MapRoute(routes,
                        name + foreignLang,
                        "{culture}/" + url,
                        defaults,
                        langConstraints,
                        namespaces,
                        area
                        );
                }
            }

            if (!routes.ContainsRouteName(name))
            {
                MapRoute(routes,
                    name,
                    url,
                    defaults,
                    constraints,
                    namespaces,
                    area
                    );
            }
        }


    }
}