using System;
using System.Web;
using System.Web.UI;
using TheUsualJoints.App_Start;

namespace TheUsualJoints.Admin
{
    public partial class event_edit : TheUsualJoints.App_Start.basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.IsInRole("Administrator"))
                {
                    // if (!IsPostBack)
                    // {
                    Label1.Text = String.Format("Edit a City Event");
                    Page.Title = String.Format("Edit a City Event");
                    PopulateControls();
                    // }
                }
                else
                {
                    Response.Redirect(Link.ToRoot("default.aspx"));
                }
            }
            else if (HttpContext.Current.User.IsInRole("Manager") || HttpContext.Current.User.IsInRole("Owner"))
            {
                Response.Redirect(Link.ToRoot("default.aspx"));
            }
            else
            {
                Response.Redirect(Link.ToRoot("default.aspx"));
            }
        }

        protected void PopulateControls()
        {
        }
    }
}