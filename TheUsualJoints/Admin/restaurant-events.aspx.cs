using TheUsualJoints.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace TheUsualJoints.Admin
{
    public partial class restaurant_events : TheUsualJoints.App_Start.basepage
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
                            Label1.Text = String.Format("Now editing {0} special events", DropDownList1.SelectedItem);
                            Page.Title = String.Format("{0} Special Events", DropDownList1.SelectedItem);
                            InfoQuickLink.NavigateUrl = "~/admin/restaurant-info?RestaurantID=" + RestaurantID;
                            ScheduleQuickLink.NavigateUrl = "~/admin/restaurant-schedule?RestaurantID=" + RestaurantID;
                            RowsToAdd_TextBox.Attributes.Add("max", "255");
                            RowsToAdd_TextBox.Attributes.Add("min", "1");
                        }
                        Populate_GridView2(RestaurantID);
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

        protected void Populate_GridView2(string RestaurantID)
        {
            GridView2.DataSource = CatalogAccess.GetRestaurantEvents(RestaurantID);
            GridView2.DataBind();
            if (GridView2.Rows.Count != 0)
            {
                GridView2.Visible = true;
            }
            else
            {
                GridView2.Visible = false;
            }
        }

        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            List<int> newRows = new List<int>();
            int rows = 0;
            try
            {
                if (RowsToAdd_TextBox.Text.Trim().Equals(""))
                {
                    rows = 1;
                }
                else
                {
                    int.TryParse(RowsToAdd_TextBox.Text.Trim(), out rows);
                }
                if (rows <= 0)
                {
                    RowsToAdd_TextBox.Text = "";
                }
                else
                {
                    RowsToAdd_TextBox.Enabled = false;
                    GridView2.Enabled = false;
                    for (int i = 0; i < rows; i++)
                    {
                        newRows.Add(i);
                    }
                    GridView1.DataSource = newRows;
                    GridView1.DataBind();
                }
            }
            catch
            {
                RowsToAdd_TextBox.Text = "";
                ClearGridView1();
            }
            if (GridView1.Rows.Count > 0)
            {
                Panel1.Visible = true;
            }
            else
            {
                Panel1.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool success = false;
            foreach (GridViewRow row in GridView1.Rows)
            {
                string restaurantId = Request.QueryString["RestaurantID"];
                string restaurantName = DropDownList1.SelectedItem.ToString();
                string shortDescription = ((TextBox)row.FindControl("ShortDescription_TextBox")).Text.Trim();
                string specialEventStart = ((TextBox)row.FindControl("SpecialEventStart_TextBox")).Text.Trim();
                string specialEventEnd = ((TextBox)row.FindControl("SpecialEventEnd_TextBox")).Text.Trim();
                string specialHours = ((TextBox)row.FindControl("SpecialHours_TextBox")).Text.Trim();
                string specialHappyHour = ((TextBox)row.FindControl("SpecialHappyHour_TextBox")).Text.Trim();
                string specialHHSpecials = ((TextBox)row.FindControl("SpecialHHSpecials_TextBox")).Text.Trim();
                string specialHH_ARBFlag = ((DropDownList)row.FindControl("SpecialHH_ARBFlag_DropDownList")).Text.Trim();
                string specialFoodDrinkSpecials = ((TextBox)row.FindControl("SpecialFoodDrinkSpecials_TextBox")).Text.Trim();
                string specialFD_ARBFlag = ((DropDownList)row.FindControl("SpecialFD_ARBFlag_DropDownList")).Text.Trim();
                string specialEvent = ((TextBox)row.FindControl("SpecialEvent_TextBox")).Text.Trim();
                string specialEvent_ARBFlag = ((DropDownList)row.FindControl("SpecialEvent_ARBFlag_DropDownList")).Text.Trim();
                CheckBox specialHappyHour_CheckBox = ((CheckBox)row.FindControl("SpecialHappyHour_CheckBox"));
                if (specialHappyHour_CheckBox.Checked == true)
                {
                    specialHappyHour = "0";
                    specialHH_ARBFlag = "2";
                }
                success = CatalogAccess.AddRestaurantSpecialEvent(restaurantId, restaurantName, shortDescription, specialEventStart, specialEventEnd, specialHours, specialHappyHour, specialHHSpecials, specialHH_ARBFlag, specialFoodDrinkSpecials, specialFD_ARBFlag, specialEvent, specialEvent_ARBFlag);
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
            ClearGridView1();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Label1.Text = String.Format("{0} update cancelled", DropDownList1.SelectedItem);
            Label1.ForeColor = System.Drawing.Color.Black;
            ClearGridView1();
        }

        protected void ClearGridView1()
        {
            GridView1.DataSource = "";
            GridView1.DataBind();
            Panel1.Visible = false;
            RowsToAdd_TextBox.Text = "";
            RowsToAdd_TextBox.Enabled = true;
            GridView2.Enabled = true;
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            // Bind data to the GridView control.
            Populate_GridView2(Request.QueryString["RestaurantID"]);
        }

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Set the row for which to enable edit mode
            GridView2.EditIndex = e.NewEditIndex;
            // Reload the gridview
            Populate_GridView2(Request.QueryString["RestaurantID"]);
            Label1.Text = String.Format("Now editing {0}", DropDownList1.SelectedItem);
            Label1.ForeColor = System.Drawing.Color.Black;
        }

        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Cancel edit mode
            GridView2.EditIndex = -1;
            // Reload the gridview
            Populate_GridView2(Request.QueryString["RestaurantID"]);
            Label1.Text = String.Format("{0} update cancelled", DropDownList1.SelectedItem);
            Label1.ForeColor = System.Drawing.Color.Black;
        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string specialEventID = ((Label)GridView2.Rows[e.RowIndex].FindControl("SpecialEventID_Label")).Text.Trim();
            string shortDescription = ((TextBox)GridView2.Rows[e.RowIndex].FindControl("ShortDescription_TextBox")).Text.Trim();
            string specialEventStart = ((TextBox)GridView2.Rows[e.RowIndex].FindControl("SpecialEventStart_TextBox")).Text.Trim();
            string specialEventEnd = ((TextBox)GridView2.Rows[e.RowIndex].FindControl("SpecialEventEnd_TextBox")).Text.Trim();
            string specialHours = ((TextBox)GridView2.Rows[e.RowIndex].FindControl("SpecialHours_TextBox")).Text.Trim();
            string specialHappyHour = ((TextBox)GridView2.Rows[e.RowIndex].FindControl("SpecialHappyHour_TextBox")).Text.Trim();
            string specialHHSpecials = ((TextBox)GridView2.Rows[e.RowIndex].FindControl("SpecialHHSpecials_TextBox")).Text.Trim();
            string specialHH_ARBFlag = ((DropDownList)GridView2.Rows[e.RowIndex].FindControl("SpecialHH_ARBFlag_DropDownList")).Text.Trim();
            string specialFoodDrinkSpecials = ((TextBox)GridView2.Rows[e.RowIndex].FindControl("SpecialFoodDrinkSpecials_TextBox")).Text.Trim();
            string specialFD_ARBFlag = ((DropDownList)GridView2.Rows[e.RowIndex].FindControl("SpecialFD_ARBFlag_DropDownList")).Text.Trim();
            string specialEvent = ((TextBox)GridView2.Rows[e.RowIndex].FindControl("SpecialEvent_TextBox")).Text.Trim();
            string specialEvent_ARBFlag = ((DropDownList)GridView2.Rows[e.RowIndex].FindControl("SpecialEvent_ARBFlag_DropDownList")).Text.Trim();
            CheckBox specialHappyHour_CheckBox = ((CheckBox)GridView2.Rows[e.RowIndex].FindControl("SpecialHappyHour_CheckBox"));
            if (specialHappyHour_CheckBox.Checked == true)
            {
                specialHappyHour = "0";
                specialHH_ARBFlag = "2";
            }
            bool success = CatalogAccess.UpdateRestaurantSpecialEvent(specialEventID, shortDescription, specialEventStart, specialEventEnd, specialHours, specialHappyHour, specialHHSpecials, specialHH_ARBFlag, specialFoodDrinkSpecials, specialFD_ARBFlag, specialEvent, specialEvent_ARBFlag);
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
            GridView2.EditIndex = -1;
            // Reload the gridview
            Populate_GridView2(Request.QueryString["RestaurantID"]);
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string specialEventID = ((Label)GridView2.Rows[e.RowIndex].FindControl("SpecialEventID_Label")).Text.Trim();
            bool success = CatalogAccess.DeleteRestaurantSpecialEvent(specialEventID);
            // Display status message
            if (success == true)
            {
                Label1.Text = String.Format("Event for {0} deleted!", DropDownList1.SelectedItem);
                Label1.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                Label1.Text = String.Format("Failed to delete {0}!", DropDownList1.SelectedItem);
                Label1.ForeColor = System.Drawing.Color.Red;
            }
            // Cancel edit mode
            GridView2.EditIndex = -1;
            // Reload the gridview
            Populate_GridView2(Request.QueryString["RestaurantID"]);
        }
    }
}