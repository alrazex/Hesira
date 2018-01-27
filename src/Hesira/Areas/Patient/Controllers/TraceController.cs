using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hesira.Areas.Patient.Models;
using Hesira.Areas.Patient.Repositories;
using Hesira.Controllers;

namespace Hesira.Areas.Patient.Controllers
{
    public class TraceController : BaseController
    {

        #region Properties & Constructor

        private readonly TraceRepository _traceRepository;



        public TraceController()
        {
            _traceRepository = new TraceRepository(HesiraDB);
        }

        #endregion

        public ActionResult Index(long userId)
        {
            var model = _traceRepository.GetTrace(userId);
            return View(model);
        }
    }
}