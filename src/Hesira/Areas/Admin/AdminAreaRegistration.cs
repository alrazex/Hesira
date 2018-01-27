using System.Web.Mvc;
using System.Web.Routing;
using Hesira.Helpers.Globalisation;

namespace Hesira.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.Routes.MapGlobalisationRoutes(
                "AdminProfile",
                AreaName + "/{controller}/{userId}",
                new {action = "Index", culture = "ro"},
                new RouteValueDictionary() {{"userId", "[0-9]+"}},
                new[] {"Hesira.Areas.Admin.Controllers"},
                AreaName
                );


            context.Routes.MapGlobalisationRoutes(
                            "AdminDefault",
                            AreaName + "/{controller}/{action}/",
                            new { action = "Index", culture = "ro" },
                            null,
                            new[] { "Hesira.Areas.Admin.Controllers" },
                            AreaName
                        );
        }
    }
}