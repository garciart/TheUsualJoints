using System;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace TheUsualJoints.App_Start
{
    /// <summary>
    /// Summary description for basepage
    /// </summary>
    public class accountpage : System.Web.UI.Page
    {
        public accountpage()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected override void OnLoad(EventArgs e)
        {
            // Check if the user is authenticated, i.e., are they even allowed to use the system //
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                // Check if the user is authorized, i.e., are they allowed to access these resources //
                if (!HttpContext.Current.User.IsInRole("Administrator"))
                {
                    // Send back to the login page. Don't even show them this page's contents //
                    Response.Redirect(Link.ToRoot("account/login.aspx"));
                }
            }
            else
            {
                // Send back to the login page. Don't even show them this page's contents //
                Response.Redirect(Link.ToRoot("account/login.aspx"));
            }
            base.OnLoad(e);
        }
    }
}