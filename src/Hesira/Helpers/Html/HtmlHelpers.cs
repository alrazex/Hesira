using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using BForms.Html;
using BForms.Models;
using Hesira.Helpers.General;
using Hesira.Models;
using Hesira.Resources;

namespace Hesira.Helpers.Html
{
    public static class HtmlHelpers
    {

        public static string UserAvatar(UserData userData, HttpContextBase currentContext)
        {
            var imgTag = new TagBuilder("img");
            imgTag.AddCssClass("img-circle");
            imgTag.Attributes.Add("alt", Resource.UserAvatar);
            if (userData.Gender == GenderEnum.Female)
            {
                imgTag.Attributes.Add("src",
                    userData.IsAdmin || userData.IsDoctor
                        ? UrlHelper.GenerateContentUrl("~/Content/Stylesheets/LTEComponents/img/femNPatient.png",
                            currentContext)
                        : UrlHelper.GenerateContentUrl("~/Content/Stylesheets/LTEComponents/img/femPatient.png",
                            currentContext));
            }

            if (userData.Gender == GenderEnum.Male)
            {

                imgTag.Attributes.Add("src",
                    userData.IsAdmin
                        ? UrlHelper.GenerateContentUrl("~/Content/Stylesheets/LTEComponents/img/menAdmin.png",
                            currentContext)
                        : (userData.IsDoctor
                            ? UrlHelper.GenerateContentUrl("~/Content/Stylesheets/LTEComponents/img/menDoctor.png",
                                currentContext)
                            : UrlHelper.GenerateContentUrl("~/Content/Stylesheets/LTEComponents/img/menPatient.png",
                                currentContext)));

            }

            return imgTag.ToString();
        }

        public static MvcHtmlString GetRoleIcon<T>(this HtmlHelper<T> helper, UserRoles role)
        {
            var star = helper.BsGlyphicon(Glyphicon.Star);

            switch (role)
            {
                case UserRoles.Admin:
                    return new MvcHtmlString(star.ToString() + star + star);
                case UserRoles.Doctor:
                    return new MvcHtmlString(star.ToString() + star);
                default:
                    return new MvcHtmlString(star.ToString());
            }
        }

    }
}