using System.Web.Optimization;

namespace Portal
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Common Admin
            bundles.Add(new ScriptBundle("~/bundles/CommonAdminJQuery").Include(
                      "~/Asset/Common/Admin/Libraries/jquery/dist/jquery-3.3.1.js"
                      ));

            //bundles.Add(new ScriptBundle("~/bundles/ckjs").Include(
            //    "~/Content/ckeditor/ckeditor.js",
            //    "~/Content/ckfinder/ckfinder.js"
            //));

            bundles.Add(new ScriptBundle("~/bundles/CommonAdminBootstrapJs").Include(
                    "~/Asset/Common/Admin/Libraries/bootstrap/dist/js/bootstrap.min.js",
                    "~/Asset/Common/Admin/Libraries/raphael/raphael.min.js",
                    "~/Asset/Common/Admin/Libraries/morris.js/morris.min.js",
                    "~/Asset/Common/Admin/Libraries/jquery-sparkline/dist/jquery.sparkline.min.js",
                    "~/Asset/Common/Admin/Libraries/jquery-knob/dist/jquery.knob.min.js",
                    "~/Asset/Common/Admin/Libraries/moment/min/moment.min.js",
                    "~/Asset/Common/Admin/Libraries/bootstrap-daterangepicker/daterangepicker.js",
                    "~/Asset/Common/Admin/Libraries/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js",
                    "~/Asset/Common/Admin/Libraries/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                    "~/Asset/Common/Admin/Libraries/jquery-slimscroll/jquery.slimscroll.min.js",
                    "~/Asset/Common/Admin/Libraries/fastclick/lib/fastclick.js",
                    "~/Asset/Common/Admin/Libraries/datatables.net/js/jquery.dataTables.js",
                    "~/Asset/Common/Admin/Libraries/datatables.net-bs/js/dataTables.bootstrap.js",
                    "~/Asset/Common/Admin/Libraries/dist/js/adminlte.min.js",
                    "~/Asset/Common/Admin/Libraries/dist/js/demo.js",
                    "~/Asset/Common/Admin/Libraries/jquery/dist/jquery.validate.min.js",
                    "~/Asset/Common/Admin/Libraries/jquery-ui/jquery-ui.js",
                    "~/Asset/Common/Admin/Libraries/jquery-ui/jquery.dialogextend.js",
                    "~/Asset/Common/Admin/Script/common.js"
                    ));

            bundles.Add(new StyleBundle("~/Content/CommonAdminCss").Include(
               "~/Asset/Common/Admin/Libraries/jquery-ui/themes/start/jquery-ui.css",
               "~/Asset/Common/Admin/Libraries/jquery-ui/themes/start/theme.css",
               "~/Asset/Common/Admin/Libraries/jquery-ui/themes/base/dialog.css",
               "~/Asset/Common/Admin/Libraries/bootstrap/dist/css/bootstrap.min.css",
               "~/Asset/Common/Admin/Libraries/font-awesome/css/font-awesome.min.css",
               "~/Asset/Common/Admin/Libraries/Ionicons/css/ionicons.min.css",
               "~/Asset/Common/Admin/Libraries/bootstrap/dist/css/bootstrap.min.css",
               "~/Asset/Common/Admin/Libraries/dist/css/AdminLTE.css",
               "~/Asset/Common/Admin/Libraries/dist/css/skins/_all-skins.min.css",
               "~/Asset/Common/Admin/Libraries/morris.js/morris.css",
               "~/Asset/Common/Admin/Libraries/jvectormap/jquery-jvectormap.css",
               "~/Asset/Common/Admin/Libraries/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css",
               "~/Asset/Common/Admin/Libraries/bootstrap-daterangepicker/daterangepicker.css",
               "~/Asset/Common/Admin/Libraries/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css",
               "~/Asset/Common/Admin/Libraries/css/fonts.googleapis.com.css",
               "~/Asset/Common/Admin/Libraries/datatables.net-bs/css/dataTables.bootstrap.css",
               "~/Asset/Common/Admin/Libraries/Drop-Down-Combo-Tree/style.css"
                ));
            #endregion Common Admin

            #region eLearning Client

            bundles.Add(new ScriptBundle("~/bundles/ClientELearningJQuery").Include(
                "~/Asset/eLearning/Client/Libraries/js/jquery-3.4.1.min.js",
                "~/Asset/eLearning/Client/Libraries/js/bootstrap.bundle.min.js",
                "~/Asset/eLearning/Client/Libraries/js/bootstrap-select.min.js",
                "~/Asset/eLearning/Client/Libraries/js/owl.carousel.min.js",
                "~/Asset/eLearning/Client/Libraries/js/isotope.js",
                "~/Asset/eLearning/Client/Libraries/js/waypoint.min.js",
                "~/Asset/eLearning/Client/Libraries/js/jquery.counterup.min.js",
                "~/Asset/eLearning/Client/Libraries/js/fancybox.js",
                "~/Asset/eLearning/Client/Libraries/js/jquery.lazy.min.js",
                "~/Asset/eLearning/Client/Libraries/js/datedropper.min.js",
                "~/Asset/eLearning/Client/Libraries/js/emojionearea.min.js",
                "~/Asset/eLearning/Client/Libraries/js/tooltipster.bundle.min.js",
                "~/Asset/eLearning/Client/Libraries/js/main.js"
            ));

            bundles.Add(new StyleBundle("~/Content/ClientELearningCss").Include(
                "~/Asset/eLearning/Client/Libraries/css/googleapis.css",
                "~/Asset/eLearning/Client/Libraries/css/bootstrap.min.css",
                "~/Asset/eLearning/Client/Libraries/css/line-awesome.css",
                "~/Asset/eLearning/Client/Libraries/css/owl.carousel.min.css",
                "~/Asset/eLearning/Client/Libraries/css/owl.theme.default.min.css",
                "~/Asset/eLearning/Client/Libraries/css/bootstrap-select.min.css",
                "~/Asset/eLearning/Client/Libraries/css/fancybox.css",
                "~/Asset/eLearning/Client/Libraries/css/tooltipster.bundle.css",
                "~/Asset/eLearning/Client/Libraries/css/animated-headline.css",
                "~/Asset/eLearning/Client/Libraries/css/style.css"
            ));
            #endregion
        }
    }
}