using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Hesira.Helpers.General;

namespace Hesira.Areas.Admin.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    [SessionState(SessionStateBehavior.ReadOnly)]
    [AdminAccess]
    public partial class ProfileController : Common.Controllers.ProfileController
    {

    }
}