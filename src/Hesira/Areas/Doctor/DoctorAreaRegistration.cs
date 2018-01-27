using System.Web.Mvc;
using System.Web.Routing;
using Hesira.Helpers.Globalisation;

namespace Hesira.Areas.Doctor
{
    public class DoctorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Doctor";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.Routes.MapGlobalisationRoutes(
                                             "DoctorProfile",
                                             AreaName + "/{controller}/{userId}",
                                             new { action = "Index", culture = "ro" },
                                             new RouteValueDictionary() { { "userId", "[0-9]+" } },
                                             new[] { "Hesira.Areas.Doctor.Controllers" },
                                             AreaName
                                         );


            context.Routes.MapGlobalisationRoutes(
                            "DoctorDefault",
                            AreaName + "/{controller}/{action}/",
                            new { action = "Index", culture = "ro" },
                            null,
                            new[] { "Hesira.Areas.Doctor.Controllers" },
                            AreaName
                        );



        }
    }
}