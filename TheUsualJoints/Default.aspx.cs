using System;
using System.Web;
using System.Web.UI;
using TheUsualJoints.App_Start;

namespace TheUsualJoints
{
    public partial class Default : basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                UserControl uc = (UserControl)Page.LoadControl(@"~/status-bar.ascx");
                Control status_bar_PlaceHolder1 = FindControl("status_bar_PlaceHolder1");
                status_bar_PlaceHolder1.Controls.Add(uc);
            }
            if (Utilities.isMobile() == true)
            {
                default_Image1.Visible = true;
            }
            else
            {
                default_Image1.Visible = false;
            }
        }
    }
}