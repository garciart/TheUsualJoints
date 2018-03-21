using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheUsualJoints.App_Start;

namespace TheUsualJoints.Admin {
    public partial class restaurant_add : TheUsualJoints.App_Start.basepage {
        protected void Page_Load(object sender, EventArgs e) {
            if (HttpContext.Current.User.Identity.IsAuthenticated) {
                if (HttpContext.Current.User.IsInRole("Administrator")) {
                    if (!IsPostBack) {
                        Label1.Text = String.Format("Add a Restaurant");
                        Page.Title = String.Format("Add a Restaurant");
                        PopulateControls();
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

        protected void PopulateControls() {
            string imageName = "restaurants/temp-restaurant-logo.png";
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + imageName + ".png")) == true) {
                RestaurantLogo_Image.ImageUrl = Link.ToImage(imageName + ".png");
            } else if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + imageName + ".jpg")) == true) {
                RestaurantLogo_Image.ImageUrl = Link.ToImage(imageName + ".jpg");
            } else {
                RestaurantLogo_Image.ImageUrl = Link.ToImage("noimageloaded.png");
            }

            for (int photoCount = 1; photoCount <= 5; photoCount++) {
                string imageLabel = String.Format("photoLabel{0}", photoCount);
                string imageID = String.Format("Restaurant_Photo_0{0}", photoCount);
                string restaurantPhotoName = String.Format("restaurants/temp-restaurant-photo-0{0}", photoCount);
                Label photoLabel = (Label)this.Master.FindControl("ContentPlaceHolder1").FindControl(imageLabel);
                photoLabel.Text = String.Format("Restaurant Photo {0}", photoCount);
                Image photoImage = (Image)this.Master.FindControl("ContentPlaceHolder1").FindControl(imageID);
                if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + restaurantPhotoName + ".png")) == true) {
                    photoImage.ImageUrl = Link.ToImage(restaurantPhotoName + ".png");
                } else if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + restaurantPhotoName + ".jpg")) == true) {
                    photoImage.ImageUrl = Link.ToImage(restaurantPhotoName + ".jpg");
                } else {
                    photoImage.ImageUrl = Link.ToImage("noimageloaded.png");
                }
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
            } else {
                Label1.Text = String.Format("Failed to update {0}!", RestaurantName_TextBox.Text);
                Label1.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect(Link.ToRoot("default.aspx"));
        }

        protected void RestaurantImageButton_Click(object sender, EventArgs e) {
            Button btn = (Button)sender;
            // string newURL = String.Format("restaurant-image?RestaurantID={0}&Position={1}&Referrer={2}", Request.QueryString["RestaurantID"], btn.CommandArgument, HttpContext.Current.Request.Url.AbsoluteUri);
            string newURL = Link.ToAdmin(String.Format("restaurant-image.aspx?RestaurantID={0}&Position={1}&Referrer=add", Request.QueryString["RestaurantID"], btn.CommandArgument));
            Response.Redirect(newURL);
        }
    }
}