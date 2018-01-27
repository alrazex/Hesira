using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hesira.Helpers.General;
using Hesira.Repositories;

namespace Hesira.Controllers
{
    [PatientAccess]
    public class HomeController : BaseController
    {

        #region Properties & Constructor

        private readonly HomeRepository _homeRepository;
        public HomeController()
        {
            _homeRepository = new HomeRepository(HesiraDB);
        }
        #endregion

        public ActionResult Index()
        {
            var userData = Session["UserData"] as UserData;
            var model = _homeRepository.GetDashBoardModel(userData);
            return View(model);
        }

    }
}