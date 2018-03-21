using System;
using System.Web;
using System.Web.UI;

namespace TheUsualJoints
{
    public partial class two_column : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                UserControl uc = (UserControl)Page.LoadControl(@"~/status-bar.ascx");
                Control status_bar_PlaceHolder1 = FindControl("status_bar_PlaceHolder1");
                status_bar_PlaceHolder1.Controls.Add(uc);
            }
        }
    }
}