using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using BForms.Models;
using BForms.Mvc;
using Hesira.Areas.Common.Models;
using Hesira.Areas.Common.Repositories;
using Hesira.Controllers;
using Hesira.Helpers.General;
using Hesira.Repositories;

namespace Hesira.Areas.Common.Controllers
{
    public partial class ProfileController : BaseController
    {


        #region Properties & Constructor

        private readonly UsersRepository _usersRepository;
        private readonly UserProfileRepository _userProfileRepository;
        private readonly BaseRepository _baseRepository;


        public ProfileController()
        {
            _usersRepository = new UsersRepository(HesiraDB);
            _userProfileRepository = new UserProfileRepository(HesiraDB);
            _baseRepository = new BaseRepository(HesiraDB);
        }

        #endregion

        #region Pages
        public ActionResult Index(long userId)
        {

            var model = _userProfileRepository.GetUserProfile(userId);

            return View(model);
        }
        #endregion

        public BsJsonResult GetReadonlyContent(long objId, PanelComponentsEnum componentId)
        {
            var html = string.Empty;
            var msg = string.Empty;
            var status = BsResponseStatus.Success;

            try
            {
                switch (componentId)
                {
                    case PanelComponentsEnum.UserInfo:
                        {
                            var userInfoModel = _userProfileRepository.GetUserProfile(objId).UserInfo;
                            html = this.BsRenderPartialView("Readonly/_UserInfo", userInfoModel);
                            break;

                        }

                    case PanelComponentsEnum.Contact:
                        {
                            var userContactModel = _userProfileRepository.GetUserProfile(objId).Contact;
                            html = this.BsRenderPartialView("Readonly/_Contact", userContactModel);
                            break;
                        }

                }

            }

            catch (Exception ex)
            {
                msg = ex.Message;
                status = BsResponseStatus.ServerError;
            }

            return new BsJsonResult(new
            {
                Html = html
            }, status, msg);
        }

        public BsJsonResult GetEditableContent(long objId, PanelComponentsEnum componentId)
        {
            var html = string.Empty;
            var model = _userProfileRepository.GetEditableUserProfile(objId, componentId);

            switch (componentId)
            {
                case PanelComponentsEnum.UserInfo:
                    {
                        html = this.BsRenderPartialView("Editable/_UserInfo", model.UserInfo, model.GetPropertyName(x => x.UserInfo));
                        break;
                    }

                case PanelComponentsEnum.Contact:
                    {
                        html = this.BsRenderPartialView("Editable/_Contact", model.Contact, model.GetPropertyName(x => x.Contact));
                        break;
                    }
            }

            return new BsJsonResult(new
            {
                Html = html
            });
        }

        public BsJsonResult SetContent(UserProfileEditableModel model, PanelComponentsEnum componentId, long objId)
        {
            var msg = string.Empty;
            var status = BsResponseStatus.Success;
            var html = string.Empty;

            if (ModelState.IsValid)
            {
                var profileModel = _userProfileRepository.Update(model, objId);
                switch (componentId)
                {
                    case PanelComponentsEnum.UserInfo:
                        html = this.BsRenderPartialView("Readonly/_UserInfo", profileModel.UserInfo);
                        break;

                    case PanelComponentsEnum.Contact:
                        html = this.BsRenderPartialView("Readonly/_Contact", profileModel.Contact);
                        break;
                }

            }
            else
            {
                return new BsJsonResult(
                    new Dictionary<string, object> { { "Errors", ModelState.GetErrors() } },
                    BsResponseStatus.ValidationError);
            }

            var userData = Session["UserData"] as UserData;

            if (userData != null)
            {
                var cnp = userData.CNP;
                Session["UserData"] = new UserData(HesiraDB, cnp);
            }

            return new BsJsonResult(
                new
                {
                    Html = html,
                },
                status,
                msg);
        }



    }
}