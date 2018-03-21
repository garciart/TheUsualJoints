using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheUsualJoints.App_Start;

namespace TheUsualJoints.Admin
{
    public partial class restaurant_info : TheUsualJoints.App_Start.basepage
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
                            Label1.Text = String.Format("Now editing {0} information", DropDownList1.SelectedItem);
                            Page.Title = String.Format("{0} Information", DropDownList1.SelectedItem);
                            ScheduleQuickLink.NavigateUrl = "~/admin/restaurant-schedule?RestaurantID=" + RestaurantID;
                            EventQuickLink.NavigateUrl = "~/admin/restaurant-events?RestaurantID=" + RestaurantID;
                        }
                        PopulateControls();
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

        protected void PopulateControls()
        {
            // Get data
            string RestaurantID = Request.QueryString["RestaurantID"];
            RestaurantDetails rd = CatalogAccess.GetRestaurantDetails(RestaurantID);
            RestaurantName_TextBox.Text = rd.RestaurantName;

            string imageName = "restaurants/" + RestaurantID + "/" + Utilities.CleanReplace(rd.RestaurantName, "-");
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + imageName + ".png")) == true)
            {
                RestaurantLogo_Image.ImageUrl = Link.ToImage(imageName + ".png");
            }
            else if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + imageName + ".jpg")) == true)
            {
                RestaurantLogo_Image.ImageUrl = Link.ToImage(imageName + ".jpg");
            }
            else
            {
                RestaurantLogo_Image.ImageUrl = Link.ToImage("noimageloaded.png");
            }
            
            RestaurantActive_CheckBox.Checked = ((bool)rd.RestaurantActive) ? true : false;
            RestaurantPriority_TextBox.Text = rd.RestaurantPriority.ToString();
            RestaurantPriority_TextBox.Attributes.Add("max", "100");
            RestaurantPriority_TextBox.Attributes.Add("min", "1");
            RestaurantTier_DropDownList.SelectedValue = rd.RestaurantTier.ToString();
            RestaurantMotto_TextBox.Text = rd.RestaurantMotto;
            RestaurantCuisine_TextBox.Text = rd.RestaurantCuisine;
            RestaurantStreetAddress_TextBox.Text = rd.RestaurantStreetAddress;
            RestaurantCity_TextBox.Text = rd.RestaurantCity;
            RestaurantState_TextBox.Text = rd.RestaurantState;
            RestaurantZipCode_TextBox.Text = rd.RestaurantZipCode;
            RestaurantPhone_TextBox.Text = rd.RestaurantPhone;
            RestaurantFax_TextBox.Text = rd.RestaurantFax;
            RestaurantEmail_TextBox.Text = rd.RestaurantEmail;
            RestaurantWebSite_TextBox.Text = rd.RestaurantWebSite;
            RestaurantSocialLink1_TextBox.Text = rd.RestaurantSocialLink1;
            RestaurantSocialLink2_TextBox.Text = rd.RestaurantSocialLink2;
            RestaurantSocialLink3_TextBox.Text = rd.RestaurantSocialLink3;
            RestaurantAboutUs_TextBox.Text = rd.RestaurantAboutUs;
            for (int photoCount = 1; photoCount <= 5; photoCount++)
            {
                string imageLabel = String.Format("photoLabel{0}", photoCount);
                string imageID = String.Format("Restaurant_Photo_0{0}", photoCount);
                string restaurantPhotoName = String.Format("restaurants/{0}/{1}-photo-0{2}", RestaurantID, Utilities.CleanReplace(rd.RestaurantName, "-"), photoCount);
                Label photoLabel = (Label)this.Master.FindControl("ContentPlaceHolder1").FindControl(imageLabel);
                photoLabel.Text = String.Format("{0} Photo {1}", rd.RestaurantName, photoCount);
                Image photoImage = (Image)this.Master.FindControl("ContentPlaceHolder1").FindControl(imageID);
                if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + restaurantPhotoName + ".png")) == true)
                {
                    photoImage.ImageUrl = Link.ToImage(restaurantPhotoName + ".png");
                }
                else if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + restaurantPhotoName + ".jpg")) == true)
                {
                    photoImage.ImageUrl = Link.ToImage(restaurantPhotoName + ".jpg");
                }
                else
                {
                    photoImage.ImageUrl = Link.ToImage("noimageloaded.png");
                }
            }
            Random rand = new Random((int)DateTime.Now.Ticks);
            
            number1.Text = rand.Next(0, 99).ToString();
            number2.Text = rand.Next(0, 99).ToString();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string RestaurantID = Request.QueryString["RestaurantID"];
            bool success = false;
            success = CatalogAccess.UpdateEstablishment(RestaurantID, RestaurantName_TextBox.Text, RestaurantActive_CheckBox.Checked, RestaurantPriority_TextBox.Text, RestaurantTier_DropDownList.SelectedValue, RestaurantMotto_TextBox.Text, RestaurantCuisine_TextBox.Text, RestaurantStreetAddress_TextBox.Text, RestaurantCity_TextBox.Text, RestaurantState_TextBox.Text, RestaurantZipCode_TextBox.Text, RestaurantPhone_TextBox.Text, RestaurantFax_TextBox.Text, RestaurantEmail_TextBox.Text, RestaurantWebSite_TextBox.Text, RestaurantSocialLink1_TextBox.Text, RestaurantSocialLink2_TextBox.Text, RestaurantSocialLink3_TextBox.Text, RestaurantAboutUs_TextBox.Text);
            // Display status message
            if (success == true)
            {
                // ClientMessageBox.Show("Woo Hoo!", this);
                Label1.Text = String.Format("{0} successfully updated!", DropDownList1.SelectedItem);
                Label1.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                Label1.Text = String.Format("Failed to update {0}!", DropDownList1.SelectedItem);
                Label1.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Link.ToRoot("default.aspx"));
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int answer = int.Parse(number1.Text) + int.Parse(number2.Text);
            try
            {
                if (int.Parse(deleteAnswer.Text) == answer)
                {
                    ClientMessageBox.Show("Woo Hoo!", this);
                    // Response.Redirect(Link.ToRoot("default.aspx"));
                }
                ClientMessageBox.Show("Delete Failed! Verification answer is incorrect!", this);
            }
            catch
            {
                ClientMessageBox.Show("Delete Failed! Verification answer cannot be blank!", this);
            }

        }

        protected void RestaurantImageButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            // string newURL = Link.ToAdmin(String.Format("restaurant-image?RestaurantID={0}&Position={1}&Referrer={2}", Request.QueryString["RestaurantID"], btn.CommandArgument, HttpContext.Current.Request.Url.AbsoluteUri));
            string newURL = Link.ToAdmin(String.Format("restaurant-image.aspx?RestaurantID={0}&Position={1}&Referrer=info", Request.QueryString["RestaurantID"], btn.CommandArgument));
            Response.Redirect(newURL);
        }
    }
}