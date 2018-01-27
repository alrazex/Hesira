using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Hesira.Helpers.General;

namespace Hesira.Areas.Patient.Controllers
{

    [PatientAccess]
    [SessionState(SessionStateBehavior.ReadOnly)]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public partial class AppointmentsController : Common.Controllers.AppointmentsController
    {



    }
}