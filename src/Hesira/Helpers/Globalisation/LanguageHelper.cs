using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Hesira.Helpers.Globalisation
{
    public static class LanguageHelper
    {

        public static string[] Cultures
        {
            get
            {
                return ConfigurationManager.AppSettings["cultures"].Split(new[] { '|' });
            }
        }

        public static string[] Languages
        {
            get
            {
                return ConfigurationManager.AppSettings["languages"].Split(new[] { '|' });
            }
        }

        public static string DefaultLanguage
        {
            get
            {
                return ConfigurationManager.AppSettings["defaultLanguage"];
            }
        }

        public static string ChangeLanguage(this UrlHelper instance, string lang)
        {


            var culture = lang == DefaultLanguage ? DefaultLanguage : lang;

            var rd = instance.RequestContext.RouteData;
            var request = instance.RequestContext.HttpContext.Request;
            var values = new RouteValueDictionary(rd.Values);
            foreach (string key in request.QueryString.Keys)
            {
                values[key] = request.QueryString[key];
            }
            values["culture"] = culture;

            return instance.RouteUrl(values);

        }
    }
}