using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using BForms.Models;
using BForms.Mvc;
using DocumentFormat.OpenXml.EMMA;
using Hesira.Areas.Common.Models;
using Hesira.Areas.Common.Repositories;
using Hesira.Controllers;
using Hesira.Helpers.General;
using RequireJS;

namespace Hesira.Areas.Common.Controllers
{

    [SessionState(SessionStateBehavior.ReadOnly)]
    public partial class AppointmentsController : BaseController
    {

        #region Properties & Constructor

        private readonly AppointmentsRepository _appointmentsRepository;


        public AppointmentsController()
        {
            _appointmentsRepository = new AppointmentsRepository(HesiraDB);
        }

        #endregion

        public ActionResult Index()
        {
            var userData = Session["UserData"] as UserData;

            var options = new Dictionary<string, object>
            {
                {"getEvents", Url.Action("GetEvents")},
                {"currentCulture", Thread.CurrentThread.CurrentCulture.Name.Split('-')[0]},
                
            };

            RequireJsOptions.Add("Index", options);

            var model = new AppointmentsPageModel
            {
                AppointmentsListModel = new AppointmentsListModel(),
                NewAppoitmentModel = _appointmentsRepository.GetNewAppoitmentModel()
            };
            return View(model);
        }

        public ActionResult Create(AppointmentsPageModel pageModel)
        {
            bool? succeeded = null;

            var newModel = pageModel.NewAppoitmentModel;

            var userData = Session["UserData"] as UserData;

            ModelState.Clear();

            if (ModelState.IsValid)
            {
                succeeded = _appointmentsRepository.Create(newModel, userData);
            }
            else
            {
                return new BsJsonResult(
                    new Dictionary<string, object> { { "Errors", ModelState.GetErrors() } },
                    BsResponseStatus.ValidationError);
            }

            return Redirect(Url.Action("Index", "Appointments"));
        }

        public BsJsonResult GetEvents()
        {
            var userData = Session["UserData"] as UserData;

            var currentAppointments = _appointmentsRepository.GetAppointments(userData).Appointments;

            var events = _appointmentsRepository.ConvertAppointmentsToEvents(currentAppointments);

            return new BsJsonResult(new
            {
                Events = events

            });

        }
    }
}