using TheUsualJoints.App_Start;
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace TheUsualJoints.Admin
{
    public partial class restaurant_schedule : TheUsualJoints.App_Start.basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.IsInRole("Administrator") || HttpContext.Current.User.IsInRole("Manager") || HttpContext.Current.User.IsInRole("Owner"))
                {
                    if (!IsPostBack)
                    {
                        string RestaurantID = Request.QueryString["RestaurantID"];
                        DataTable restaurants = CatalogAccess.GetRestaurantNames(false);
                        if (RestaurantID == null)
                        {
                            Response.Redirect(HttpContext.Current.Request.Url.AbsolutePath + "?RestaurantID=" + Int32.Parse(restaurants.Rows[0]["RestaurantID"].ToString()));
                        }
                        else
                        {
                            DropDownList1.DataSource = restaurants;
                            DropDownList1.DataTextField = "RestaurantName";
                            DropDownList1.DataValueField = "RestaurantID";
                            DropDownList1.SelectedValue = RestaurantID;
                            DropDownList1.DataBind();
                            Label1.Text = String.Format("Now editing {0} schedule", DropDownList1.SelectedItem);
                            Page.Title = String.Format("{0} Schedule", DropDownList1.SelectedItem);
                            InfoQuickLink.NavigateUrl = "~/admin/restaurant-info?RestaurantID=" + RestaurantID;
                            EventQuickLink.NavigateUrl = "~/admin/restaurant-events?RestaurantID=" + RestaurantID;
                        }
                        Populate_GridView1();
                    }
                }
                else if (HttpContext.Current.User.IsInRole("Advertiser"))
                {
                    Response.Redirect(Link.ToRoot("default.aspx"));
                }
                else
                {
                    Response.Redirect(Link.ToRoot("default.aspx"));
                }
            }
            else
            {
                Response.Redirect(Link.ToRoot("default.aspx"));
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList list = (DropDownList)sender;
            Response.Redirect(HttpContext.Current.Request.Url.AbsolutePath + "?RestaurantID=" + list.SelectedValue.ToString(), false);
        }

        protected void Populate_GridView1()
        {
            string RestaurantID = Request.QueryString["RestaurantID"];
            GridView1.DataSource = CatalogAccess.GetRestaurantRoutine(RestaurantID);
            GridView1.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool success = false;
            foreach (GridViewRow row in GridView1.Rows)
            {
                string restaurantId = Request.QueryString["RestaurantID"];
                string restaurantName = DropDownList1.SelectedItem.ToString();
                string regularScheduleID = ((Label)row.FindControl("RestaurantRoutineID_Label")).Text.Trim();
                string hoursOfOperation = ((TextBox)row.FindControl("HoursOfOperation_TextBox")).Text.Trim();
                string happyHourTimes = ((TextBox)row.FindControl("HappyHourTimes_TextBox")).Text.Trim();
                string happyHourSpecials = ((TextBox)row.FindControl("HappyHourSpecials_TextBox")).Text.Trim();
                string foodAndDrinkSpecials = ((TextBox)row.FindControl("FoodAndDrinkSpecials_TextBox")).Text.Trim();
                string specialEvents = ((TextBox)row.FindControl("RestaurantEvents_TextBox")).Text.Trim();
                success = CatalogAccess.UpdateRestaurantRoutine(regularScheduleID, hoursOfOperation, happyHourTimes, happyHourSpecials, foodAndDrinkSpecials, specialEvents);
            }
            // Display status message
            if (success == true)
            {
                Label1.Text = String.Format("{0} successfully updated!", DropDownList1.SelectedItem);
                Label1.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                Label1.Text = String.Format("Failed to update {0}!", DropDownList1.SelectedItem);
                Label1.ForeColor = System.Drawing.Color.Red;
            }
            // Cancel edit mode
            GridView1.EditIndex = -1;
            // Reload the gridview
            Populate_GridView1();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Link.ToRoot("default.aspx"));
        }
    }
}