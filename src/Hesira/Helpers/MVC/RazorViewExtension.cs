using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hesira.Helpers.MVC
{

    public class RazorViewExtension : RazorViewEngine
    {
        public RazorViewExtension()
        {
            ViewLocationFormats = new[]
                                  {
                                      "~/Views/{1}/{0}.cshtml",
                                      "~/Areas/Common/Views/{1}/{0}.cshtml",
                                      "~/Views/Shared/{0}.cshtml",
                               
                                  };

            PartialViewLocationFormats = new[]
                                         {
                                             "~/Views/{1}/{0}.cshtml",
                                             "~/Areas/Common/Views/{0}.cshtml",
                                             "~/Areas/Common/Views/{1}/{0}.cshtml",
                                             "~/Areas/Common/Views/Shared/{0}.cshtml",
                                             "~/Views/Shared/{0}.cshtml"
                                             
                                         };
        }
    }

}