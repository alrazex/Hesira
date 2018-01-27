using System.Web.Optimization;

namespace Hesira.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/BFormsCSS")
                .Include("~/Scripts/BForms/Bundles/css/*.css", new CssRewriteUrlTransform())
                .Include("~/Content/Stylesheets/Site/site.css", new CssRewriteUrlTransform())
                );

            bundles.Add(new StyleBundle("~/LTEAdminCSS")
                .Include("~/Content/Stylesheets/LTEComponents/css/*.css", new CssRewriteUrlTransform()));

        }
    }
}


