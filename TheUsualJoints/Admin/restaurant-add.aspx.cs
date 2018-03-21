using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using TheUsualJoints.App_Start;

namespace TheUsualJoints.Admin {
    public partial class restaurant_add_plain : TheUsualJoints.App_Start.basepage {
        protected void Page_Load(object sender, EventArgs e) {
            if (HttpContext.Current.User.Identity.IsAuthenticated) {
                if (HttpContext.Current.User.IsInRole("Administrator")) {
                    if (!IsPostBack) {
                        Label1.Text = String.Format("Add a Restaurant");
                        Page.Title = String.Format("Add a Restaurant");
                        DataTable cities = CatalogAccess.GetCities(true);
                        CityDropDownList.DataSource = cities;
                        CityDropDownList.DataTextField = "CityName";
                        CityDropDownList.DataValueField = "CityID";
                        CityDropDownList.SelectedValue = cities.Rows[0]["CityID"].ToString();
                        CityDropDownList.DataBind();
                    }
                } else {
                    Response.Redirect(Link.ToRoot("default.aspx"));
                }
            } else if (HttpContext.Current.User.IsInRole("Advertiser") || HttpContext.Current.User.IsInRole("Manager") || HttpContext.Current.User.IsInRole("Owner")) {
                Response.Redirect(Link.ToRoot("default.aspx"));
            } else {
                Response.Redirect(Link.ToRoot("default.aspx"));
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e) {
            int RestaurantID = 0;
            bool success = false;
            success = CatalogAccess.AddRestaurant(RestaurantName_TextBox.Text, RestaurantActive_CheckBox.Checked, RestaurantPriority_TextBox.Text, RestaurantTier_DropDownList.SelectedValue, RestaurantMotto_TextBox.Text, RestaurantCuisine_TextBox.Text, RestaurantStreetAddress_TextBox.Text, RestaurantCity_TextBox.Text, RestaurantState_TextBox.Text, RestaurantCountry_TextBox.Text, RestaurantZipCode_TextBox.Text, RestaurantPhone_TextBox.Text, RestaurantFax_TextBox.Text, RestaurantEmail_TextBox.Text, RestaurantWebSite_TextBox.Text, RestaurantSocialLink1_TextBox.Text, RestaurantSocialLink2_TextBox.Text, RestaurantSocialLink3_TextBox.Text, RestaurantAboutUs_TextBox.Text, RestaurantPhone_TextBox.Text, CityDropDownList.SelectedValue, out RestaurantID);
            // Display status message
            if (success == true) {
                String restaurantPath = System.Web.Hosting.HostingEnvironment.MapPath("~/images/restaurants/" + RestaurantID);
                if (!Directory.Exists(restaurantPath)) {
                    Directory.CreateDirectory(restaurantPath);
                }
                Label1.Text = String.Format("{0} successfully updated!", RestaurantName_TextBox.Text);
                Label1.ForeColor = System.Drawing.Color.Green;
                ClientMessageBox.Show(String.Format("{0} successfully updated!", RestaurantName_TextBox.Text), this);
                Response.Redirect(Link.ToAdmin("restaurant-image-edit.aspx?RestaurantID=" + RestaurantID));
            } else {
                Label1.Text = String.Format("Failed to update {0}!", RestaurantName_TextBox.Text);
                Label1.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect(Link.ToRoot("restaurant-info.aspx"));
        }
    }
}