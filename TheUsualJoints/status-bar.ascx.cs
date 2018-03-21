using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using TheUsualJoints.App_Start;

namespace TheUsualJoints
{
    public partial class status_bar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.IsInRole("Administrator"))
                    {
                        DropDownList DropDownList1 = (DropDownList)LoginView1.FindControl("DropDownList1");
                        DropDownList1.Items.Add(new ListItem("Select an action...", "0"));
                        DropDownList1.Items.Add(new ListItem("Add Restaurant", "1"));
                        DropDownList1.Items.Add(new ListItem("Edit Restaurant Info", "2"));
                        DropDownList1.Items.Add(new ListItem("Edit Restaurant Schedule", "3"));
                        DropDownList1.Items.Add(new ListItem("Edit Restaurant Special Events", "4"));
                        DropDownList1.Items.Add(new ListItem("Add Advertiser", "5"));
                        DropDownList1.Items.Add(new ListItem("Edit Advertiser", "6"));
                        DropDownList1.Items.Add(new ListItem("Add City Event", "7"));
                        DropDownList1.Items.Add(new ListItem("Edit City Event", "8"));
                        DropDownList1.Items.Add(new ListItem("Add City Link", "9"));
                        DropDownList1.Items.Add(new ListItem("Edit City Link", "10"));
                        DropDownList1.Items.Add(new ListItem("Upload Check", "11"));
                    }
                    else if (HttpContext.Current.User.IsInRole("Manager"))
                    {

                    }
                    else if (HttpContext.Current.User.IsInRole("Owner"))
                    {

                    }
                    else if (HttpContext.Current.User.IsInRole("Advertiser"))
                    {

                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
        }

        protected void LoginStatus1_LoggingOut(object sender, System.Web.UI.WebControls.LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut();
        }

        protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect(Link.ToRoot("/account/login.aspx"));
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList DropDownList1 = (DropDownList)LoginView1.FindControl("DropDownList1");
            switch (DropDownList1.SelectedValue)
            {
                case "1":
                    {
                        Response.Redirect(Link.ToAdmin("restaurant-add.aspx"));
                        break;
                    }
                case "2":
                    {
                        Response.Redirect(Link.ToAdmin("restaurant-info.aspx"));
                        break;
                    }
                case "3":
                    {
                        Response.Redirect(Link.ToAdmin("restaurant-schedule.aspx"));
                        break;
                    }
                case "4":
                    {
                        Response.Redirect(Link.ToAdmin("restaurant-events.aspx"));
                        break;
                    }
                case "5":
                    {
                        Response.Redirect(Link.ToAdmin("advertiser-add.aspx"));
                        break;
                    }
                case "6":
                    {
                        Response.Redirect(Link.ToAdmin("advertiser-edit.aspx"));
                        break;
                    }
                case "7":
                    {
                        Response.Redirect(Link.ToAdmin("event-add.aspx"));
                        break;
                    }
                case "8":
                    {
                        Response.Redirect(Link.ToAdmin("event-edit.aspx"));
                        break;
                    }
                case "9":
                    {
                        Response.Redirect(Link.ToAdmin("link-add.aspx"));
                        break;
                    }
                case "10":
                    {
                        Response.Redirect(Link.ToAdmin("link-edit.aspx"));
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}