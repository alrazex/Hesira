using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BForms.Mvc;
using Hesira.Helpers.General;
using Hesira.Models;
using Hesira.Resources;

namespace Hesira.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : BaseController
    {

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel();
            model.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginModel)
        {

            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(loginModel.CNP, loginModel.Password))
                {
                    var userData = new UserData(HesiraDB, loginModel.CNP);
                    if (HttpContext.Session != null)
                    {
                        HttpContext.Session["UserData"] = userData;
                    }

                    FormsAuthentication.SetAuthCookie(loginModel.CNP, true);

                    if (LocalAndSafeUrl(loginModel.ReturnUrl))
                    {
                        return Redirect(loginModel.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                var message = Resource.SignInFailed;

                // login message - provider validation method
                if (System.Web.HttpContext.Current.Items["LoginMessage"] != null &&
                    !string.IsNullOrEmpty(System.Web.HttpContext.Current.Items["LoginMessage"].ToString()))
                {
                    message = (System.Web.HttpContext.Current.Items["LoginMessage"]).ToString();
                }

                ModelState.AddFormError("", message);
            }
            return View(loginModel);
        }

        public virtual ActionResult Logout(string returnUrl)
        {
            if (HttpContext.Session != null) HttpContext.Session["UserData"] = null;
            FormsAuthentication.SignOut();
            TempData.Clear();

            return RedirectToAction("Login", "Authentication", new { returnUrl = returnUrl });
        }

        public bool LocalAndSafeUrl(string url)
        {
            return Url.IsLocalUrl(url) && url.Length > 1 && url.StartsWith("/")
                   && !url.StartsWith("//") && !url.StartsWith("/\\");
        }

    }
}