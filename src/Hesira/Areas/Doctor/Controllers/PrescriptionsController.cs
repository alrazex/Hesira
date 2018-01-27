using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using BForms.Models;
using BForms.Mvc;
using Hesira.Areas.Common.Models;
using Hesira.Areas.Doctor.Models;
using Hesira.Areas.Doctor.Repositories;
using Hesira.Controllers;
using Hesira.Helpers.General;

namespace Hesira.Areas.Doctor.Controllers
{

    [DoctorAccess]
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class PrescriptionsController : BaseController
    {

        #region Properties & Constructor

        private readonly PrescriptionsRepository _prescriptionsRepository;
        private readonly PrescriptionGroupEditorRepository _prescriptionGroupEditorRepository;



        public PrescriptionsController()
        {
            _prescriptionsRepository = new PrescriptionsRepository(HesiraDB);
            _prescriptionGroupEditorRepository = new PrescriptionGroupEditorRepository(HesiraDB);
        }

        #endregion

        public ActionResult Index()
        {

            var options = new Dictionary<string, object>
            {
                {"drugsBuilderPager", Url.Action("DrugsBuilderPager")},
                {"newPrescriptionUrl", Url.Action("Create")}
                 
            };

            RequireJsOptions.Add("Index", options);

            var userData = Session["UserData"] as UserData;

            var model = new PrescriptionsPageModel
            {
                NewPrescriptionModel = _prescriptionsRepository.GetNewForm(userData)
            };

            return View(model);
        }

        public BsJsonResult Create(PrescriptionsPageModel pageModel)
        {
            var html = string.Empty;
            bool? succeeded = null;

            var newModel = pageModel.NewPrescriptionModel;

            var userData = Session["UserData"] as UserData;

            if (ModelState.IsValid)
            {
                succeeded = _prescriptionsRepository.Create(newModel, userData);
            }
            else
            {
                return new BsJsonResult(
                    new Dictionary<string, object> { { "Errors", ModelState.GetErrors() } },
                    BsResponseStatus.ValidationError);
            }

            return new BsJsonResult(new
            {
                Succeeded = succeeded

            });
        }

        public virtual BsJsonResult DrugsBuilderPager(PrescriptionGroupEditorRepositorySettings settings)
        {
            var html = string.Empty;
            var count = 0;

            html = RenderTab(settings, out count);

            return new BsJsonResult(new
            {
                Count = count,
                Html = html
            });

        }

        [NonAction]
        public string RenderTab(PrescriptionGroupEditorRepositorySettings settings, out int count)
        {
            var html = string.Empty;
            count = 0;

            var model = new PrescriptionBuilderModel();

            var grid = _prescriptionGroupEditorRepository.ToBsGridViewModel(settings, out count);

            model.DrugsTab = new PrescriptionBuilderTabModel
            {
                Grid = grid,
            };

            html = this.BsRenderPartialView("TeamBuilder/_Index", model);

            return html;
        }

    }
}