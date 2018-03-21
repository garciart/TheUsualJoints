using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using TheUsualJoints.App_Start;

namespace TheUsualJoints
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            /* DISABLED! Used to remove aspx from URL along with App_Start/RouteConfig.cs */
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_Error(Object sender, EventArgs e)
        {
            // Log all unhandled errors
            Utilities.LogError(Server.GetLastError());
        }
    }
}