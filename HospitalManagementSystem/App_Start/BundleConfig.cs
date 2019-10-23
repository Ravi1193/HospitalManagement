using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace HospitalManagementSystem.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundle(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                 "~/Scripts/app.js",
                 "~/Scripts/bootstrap.min.js",
                 "~/Scripts/jquery-1.10.2.min.js"
             ));

            bundles.Add(new StyleBundle("~/bundles/style").Include(
                "~/Content/style.css"

                ));
        }
    }
}