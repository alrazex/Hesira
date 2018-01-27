using Hesira.Entities;
using RequireJS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hesira.Controllers
{
    public class BaseController : RequireJS.RequireJsController
    {

        public HesiraEntities HesiraDB = new HesiraEntities();

        public override void RegisterGlobalOptions()
        {
            RequireJsOptions.Add(
                "homeUrl",
                Url.Action("Index", "Home", new { area = "" }),
                RequireJsOptionsScope.Global);

            RequireJsOptions.Add(
                           "toggleSidebarUrl",
                           Url.Action("ToggleSidebar", "Ajax", new { area = "" }),
                           RequireJsOptionsScope.Global);
        }
	}
}