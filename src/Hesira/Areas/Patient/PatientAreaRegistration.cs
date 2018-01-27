using System.Web.Mvc;
using System.Web.Routing;
using Hesira.Helpers.Globalisation;

namespace Hesira.Areas.Patient
{
    public class PatientAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Patient";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapGlobalisationRoutes(
                                  "PatientProfile",
                                  AreaName + "/{controller}/{userId}",
                                  new { action = "Index", culture = "ro" },
                                  new RouteValueDictionary() { { "userId", "[0-9]+" } },
                                  new[] { "Hesira.Areas.Patient.Controllers" },
                                  AreaName
                              );


            context.Routes.MapGlobalisationRoutes(
                            "PatientDefault",
                            AreaName + "/{controller}/{action}/",
                            new { action = "Index", culture = "ro" },
                            null,
                            new[] { "Hesira.Areas.Patient.Controllers" },
                            AreaName
                        );

        }
    }
}