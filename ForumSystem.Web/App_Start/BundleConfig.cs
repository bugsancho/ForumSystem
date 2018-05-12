using System.Web.Optimization;

namespace ForumSystem.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.DirectoryFilter.Clear(); // apparently, DirectoryFilter ignores all .min.js when debug

            // In order to guarantee that angular will be loaded before any angular modules, we add the explicit file order rule 
            var filesOrder = new BundleFileSetOrdering("angular");
            filesOrder.Files.Add("moment.min.js");
            filesOrder.Files.Add("angular.js");
            filesOrder.Files.Add("angular-*.js");
            filesOrder.Files.Add("ui-bootstrap.js");
            filesOrder.Files.Add("ui-bootstrap-*.js");
            bundles.FileSetOrderList.Add(filesOrder);

            bundles.Add(new ScriptBundle("~/js/libs")
                .IncludeDirectory("~/dist/libs/js", "*.js"));

            bundles.Add(new Bundle("~/css/libs")
                .IncludeDirectory("~/dist/libs/css", "*.css"));

            bundles.Add(new Bundle("~/js/app").IncludeDirectory("~/dist/app", "*.js", searchSubdirectories: true));

        }
    }
}
