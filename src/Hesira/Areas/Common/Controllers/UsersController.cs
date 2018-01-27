using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BForms.Grid;
using BForms.Models;
using BForms.Mvc;
using DocumentFormat.OpenXml.Spreadsheet;
using Hesira.Areas.Common.Extensions;
using Hesira.Areas.Common.Models;
using Hesira.Areas.Common.Repositories;
using Hesira.Controllers;
using Hesira.Helpers.General;
using Hesira.Models;
using Hesira.Models.Address;
using Hesira.Models.User;
using Hesira.Repositories;
using Hesira.Repositories.Nucleus.Interfaces;
using Hesira.Resources;
using Hesira.Services.Services.Interfaces;
using RequireJS;
using UserProfileModel = Hesira.Models.User.UserProfileModel;

namespace Hesira.Areas.Common.Controllers
{
    public partial class UsersController : BaseController
    {

        #region Properties & Constructor

        private readonly UsersRepository _usersRepository;
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;
        private readonly IUserProfileService _userProfileService;
        private readonly IUnitOfWork _unitOfWork;


        public UsersController(IUserService userService, IAddressService addressService,
            IUserProfileService userProfileService)
        {
            _usersRepository = new UsersRepository(HesiraDB);
            this._userService = userService;
            this._addressService = addressService;
            this._userProfileService = userProfileService;
        }

        #endregion

        #region Pages

        public ActionResult Index()
        {

            var userData = Session["UserData"] as UserData;

            var gridSettings = new UsersSettings
            {
                PageSize = 5,
                Page = 1,
                UserData = userData
            };

            var gridModel = _usersRepository.ToBsGridViewModel(gridSettings);

            var model = new UsersPageModel
            {
                Grid = gridModel,
                Toolbar = new BsToolbarModel<UserSearchModel, UserNewModel>
                {
                    Search = _usersRepository.GetSearchForm(gridSettings),
                    New = _usersRepository.GetNewForm(gridSettings),
                }
            };

            var options = new Dictionary<string, object>
            {
                {"pagerUrl", Url.Action("Pager")},
                {"getRowsUrl", Url.Action("GetRows")},
                {"enableDisableUrl", Url.Action("EnableDisable")},
                {"deleteUrl", Url.Action("Delete")},
                {"updateComponentListUrl", Url.Action("UpdateComponentList")},
                {"currentCulture", Thread.CurrentThread.CurrentCulture.Name.Split('-')[0]},
                {"autocompleteComponents", RequireJsHtmlHelpers.ToJsonDictionary<UserAutocompleteComponents>()},
                {"userRoles", RequireJsHtmlHelpers.ToJsonDictionary<UserRoles>()}    
            };

            RequireJsOptions.Add("Index", options);

            return View(model);
        }

        #endregion

        #region Ajax Calls

        public BsJsonResult New(BsToolbarModel<UserSearchModel, UserNewModel> model)
        {
            var msg = string.Empty;
            var status = BsResponseStatus.Success;
            long? userId = null;
            var row = string.Empty;

            ClearModelStateOnAdd(ModelState, model.New);

            if (ModelState.IsValid)
            {
                var userDomainModel = new UserModel();
                var userProfileDomainModel = new UserProfileModel();
                var addressDomainModel = new AddressModel();
                var newModel = model.New;

                newModel.ToUserProfileDomainModel(userProfileDomainModel);
                newModel.ToAddressDomainModel(addressDomainModel);
                newModel.ToUserDomainModel(userDomainModel);

                userId = _userService.Create(userDomainModel, userProfileDomainModel, addressDomainModel);

                var rowModel = _usersRepository.ReadRow(userId);
                var viewModel = _usersRepository.ToBsGridViewModel(rowModel).Wrap<UsersPageModel>(x => x.Grid);

                row = this.BsRenderPartialView("Grid/_Grid", viewModel);
                msg = Resource.SaveSuccess;
            }
            else
            {
                return new BsJsonResult(
                                       new Dictionary<string, object> { { "Errors", ModelStateExtensions.GetErrors(ModelState) } },
                                       BsResponseStatus.ValidationError);
            }


            return new BsJsonResult(new
            {
                Message = msg,
                UserId = userId,
                Row = row
            }, status,  msg);

        }

        public BsJsonResult Pager(UsersSettings settings)
        {
            var msg = string.Empty;
            var status = BsResponseStatus.Success;
            var html = string.Empty;
            var count = 0;

            try
            {
                settings.UserData = Session["UserData"] as UserData;

                var pagerModel = _usersRepository.ToBsGridViewModel(settings, out count).Wrap<UsersPageModel>(x => x.Grid);

                html = this.BsRenderPartialView("Grid/_Grid", pagerModel);

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                status = BsResponseStatus.ServerError;
            }

            return new BsJsonResult(new
            {
                Count = count,
                Html = html
            }, status, msg);
        }

        public BsJsonResult GetRows(List<BsGridRowData<long>> items)
        {
            var msg = string.Empty;
            var status = BsResponseStatus.Success;
            var rowsHtml = string.Empty;

            try
            {
                rowsHtml = GetRowsHtml(items);
            }
            catch (Exception ex)
            {
                msg = "<strong>" + Resource.ServerError + "!</strong> " + ex.Message;
                status = BsResponseStatus.ServerError;
            }

            return new BsJsonResult(new
            {
                RowsHtml = rowsHtml
            }, status, msg);
        }

        public BsJsonResult Delete(List<BsGridRowData<long>> items)
        {
            var msg = string.Empty;
            var status = BsResponseStatus.Success;

            try
            {

                foreach (var item in items)
                {
                    _usersRepository.Delete(item.Id);
                }

            }
            catch (Exception ex)
            {
                msg = "<strong>" + Resource.ServerError + "!</strong> " + ex.Message;
                status = BsResponseStatus.ServerError;
            }

            return new BsJsonResult(null, status, msg);
        }

        public BsJsonResult EnableDisable(List<BsGridRowData<long>> items, bool? enable)
        {
            var msg = string.Empty;
            var status = BsResponseStatus.Success;
            var rowsHtml = string.Empty;

            try
            {
                foreach (var item in items)
                {
                    _usersRepository.EnableDisable(item.Id, enable);
                }

                rowsHtml = GetRowsHtml(items);
            }
            catch (Exception ex)
            {
                msg = "<strong>" + Resource.ServerError + "!</strong> " + ex.Message;
                status = BsResponseStatus.ServerError;
            }

            return new BsJsonResult(new
            {
                RowsHtml = rowsHtml
            }, status, msg);
        }

        public virtual BsJsonResult UpdateComponentList(UserComponentListUpdateModel userComponentListUpdateModel)
        {
            var msg = String.Empty;
            var status = BsResponseStatus.Success;
            var results = new UserComponentListModel();

            #region Init Settings

            var currentUserData = Session["UserData"] as UserData;
            var settings = new UsersSettings();

            settings.UserData = currentUserData;

            #endregion

            results = _usersRepository.UpdateUserComponentList(settings, userComponentListUpdateModel);

            return new BsJsonResult(new
            {
                results = results.HtmlType == RegComponentHtmlType.Dropdown ? results.UserComponentDropdown.Items : results.UserComponentListBox.Items,
                total = results.Total
            }, status, msg);

        }

        #endregion

        #region Non-Actions

        [NonAction]
        private string GetRowsHtml(List<BsGridRowData<long>> items)
        {
            var usersIds = items.Select(x => x.Id).ToList();
            var rowsModel = _usersRepository.ReadRows(usersIds);
            var viewModel = _usersRepository.ToBsGridViewModel(rowsModel, row => row.Id, items)
                    .Wrap<UsersPageModel>(x => x.Grid);

            return this.BsRenderPartialView("Grid/_Grid", viewModel);
        }

        public void ClearModelStateOnAdd(ModelStateDictionary modelState, UserNewModel model)
        {

            modelState.Clear();

            #region CNP

            if (!Validation.CNPValidator(model.CNP))
            {
                modelState.AddModelError("New.CNP", Resource.InvalidCNP);

            }
            else if (_usersRepository.NotUniqueCNP(model.CNP))
            {
                modelState.AddModelError("New.CNP", Resource.NotUniqueCNP);

            }

            #endregion

            #region User State 

            if(model.RoleBtnList != null && model.RoleBtnList.SelectedValues != null)
            {
                var selected = model.RoleBtnList.SelectedValues;
                if (selected == UserRoles.Patient && (model.State == null || model.State.SelectedValues == null) )
                {
                    modelState.AddModelError("New.State", Resource.RequiredField);
                }

            }

            #endregion

        }


        #endregion
    }
}