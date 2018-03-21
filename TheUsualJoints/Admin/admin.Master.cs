using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheUsualJoints.Admin
{
    public partial class admin : System.Web.UI.MasterPage
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