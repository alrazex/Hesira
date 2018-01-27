using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BForms.Models;
using Hesira.Controllers;
using Hesira.Entities;

namespace Hesira.Helpers.General
{

    public class AdminAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var baseCtrl = (BaseController)filterContext.Controller;
            var Session = filterContext.HttpContext.Session;

            if (Session != null)
            {
                var userData = (UserData)Session["UserData"];

                var url = new UrlHelper(filterContext.RequestContext);

                var request = filterContext.RequestContext.HttpContext.Request;
                var ajaxRequest = request.IsAjaxRequest();


                var returnUrl = ajaxRequest
                    ? new RedirectResult(url.ActionSecure("Index", "Home", new {area = ""})).Url
                    : filterContext.HttpContext.Request.RawUrl;

                var actionResult =
                    new RedirectResult(url.ActionSecure("Login", "Authentication",
                        new {area = "", returnUrl = returnUrl}));

                var cookie = request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket cookieInfo = null;

                var denyUser = cookie == null || string.IsNullOrEmpty(cookie.Value) ||
                                (cookieInfo = FormsAuthentication.Decrypt(cookie.Value)) == null ||
                                cookieInfo.Expired;

                if (!denyUser)
                {

                    if (userData == null)
                    {
                        Session["UserData"] = userData = new UserData(baseCtrl.HesiraDB, cookieInfo.Name);
                    }

                    if (!userData.IsAdmin || !userData.Enabled)
                    {
                        denyUser = true;
                    }

                }

                if (denyUser)
                {
                    if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new BsJsonResult(new
                        {
                            RedirectUrl = actionResult.Url
                        });
                    }
                    else
                    {
                        filterContext.Result = actionResult;
                    }
                }
            }
        }
    }

    public class DoctorAccessAttribute : ActionFilterAttribute
    {  
       public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var baseCtrl = (BaseController)filterContext.Controller;
            var Session = filterContext.HttpContext.Session;
            if (Session != null)
            {
                var userData = (UserData)Session["UserData"];

                var url = new UrlHelper(filterContext.RequestContext);
                var request = filterContext.RequestContext.HttpContext.Request;
                var ajaxRequest = request.IsAjaxRequest();
                var returnUrl = ajaxRequest
                    ? new RedirectResult(url.ActionSecure("Index", "Home", new {area = ""})).Url
                    : filterContext.HttpContext.Request.RawUrl;

                var actionResult =
                    new RedirectResult(url.ActionSecure("Login", "Authentication",
                        new {area = "", returnUrl = returnUrl}));

                var cookie = request.Cookies[FormsAuthentication.FormsCookieName];                
                FormsAuthenticationTicket cookieInfo = null;

                var denyUser = cookie == null || string.IsNullOrEmpty(cookie.Value) ||
                                (cookieInfo = FormsAuthentication.Decrypt(cookie.Value)) == null ||
                                cookieInfo.Expired;
                if (!denyUser)
                {
                    if (userData == null)
                    {
                        Session["UserData"] = userData = 
                            new UserData(baseCtrl.HesiraDB, cookieInfo.Name);
                    }
                    if (userData.IsPatient || !userData.Enabled)
                    {
                        denyUser = true;
                    }
                }
                if (denyUser)
                {
                    if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new BsJsonResult(new
                        {
                            RedirectUrl = actionResult.Url
                        });
                    }
                    else
                    {
                        filterContext.Result = actionResult;
                    }
                }
            }
        }
    }

    public class PatientAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var baseCtrl = (BaseController)filterContext.Controller;
            var Session = filterContext.HttpContext.Session;

            if (Session != null)
            {
                var userData = (UserData)Session["UserData"];

                var url = new UrlHelper(filterContext.RequestContext);

                var request = filterContext.RequestContext.HttpContext.Request;
                var ajaxRequest = request.IsAjaxRequest();


                var returnUrl = ajaxRequest ? new RedirectResult(url.ActionSecure("Index", "Home", new { area = "" })).Url : filterContext.HttpContext.Request.RawUrl;

                var actionResult = new RedirectResult(url.ActionSecure("Login", "Authentication", new { area = "", returnUrl = returnUrl }));

                var cookie = request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket cookieInfo = null;

                var denyUser = cookie == null || string.IsNullOrEmpty(cookie.Value) ||
                                (cookieInfo = FormsAuthentication.Decrypt(cookie.Value)) == null ||
                                cookieInfo.Expired;

                if (!denyUser)
                {
                    if (userData == null)
                    {
                        Session["UserData"] = new UserData(baseCtrl.HesiraDB, cookieInfo.Name);
                    }
                }

                if (denyUser)
                {
                    if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new BsJsonResult(new
                        {
                            RedirectUrl = actionResult.Url
                        });
                    }
                    else
                    {
                        filterContext.Result = actionResult;
                    }
                }
            }
        }
    }

}

