using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BForms.Models;
using Hesira.Helpers.General;
using Hesira.Repositories;

namespace Hesira.Controllers
{
    public class AjaxController : BaseController
    {
        #region Properties & Constructor

        private AjaxRepository _ajaxRepository { get; set; }

        public AjaxController()
        {
            _ajaxRepository = new AjaxRepository(HesiraDB);
        }


        #endregion

        #region Sidebar

        [PatientAccess]
        public BsJsonResult ToggleSidebar()
        {
            var msg = string.Empty;
            var status = BsResponseStatus.Success;
            bool? sidebarOpen = null;

            try
            {
                var userData = Session["UserData"] as UserData;
                if (userData != null)
                {
                    sidebarOpen = _ajaxRepository.ToggleSidebarMode(userData);

                    if (sidebarOpen.HasValue)
                    {
                        Session["UserData"] = null;

                        userData.SideBarOpen = sidebarOpen.Value;
                        Session["UserData"] = userData;

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
                SidebarOpen = sidebarOpen
            }, status, msg);


        }

        #endregion

    }
}