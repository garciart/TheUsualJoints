using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheUsualJoints.App_Start;

namespace TheUsualJoints.Admin
{
    public partial class advertiser_add : TheUsualJoints.App_Start.basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.IsInRole("Administrator"))
                {
                    // if (!IsPostBack)
                    // {
                        Label1.Text = String.Format("Add an Advertiser");
                        Page.Title = String.Format("Add an Advertiser");
                        PopulateControls();
                    // }
                }
                else
                {
                    Response.Redirect(Link.ToRoot("default.aspx"));
                }
            }
            else if (HttpContext.Current.User.IsInRole("Advertiser") || HttpContext.Current.User.IsInRole("Manager") || HttpContext.Current.User.IsInRole("Owner"))
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
            Page.Title = "Add an Advertiser";
            TextBox5.Text = "50";
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/temp-ad-image.png")))
            {
                Image1.ImageUrl = "../images/temp-ad-image.png";
            }
            else if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/temp-ad-image.jpg")))
            {
                Image1.ImageUrl = "../images/temp-ad-image.jpg";
            }
            else
            {
                Image1.ImageUrl = "../images/noimageloaded.png";
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/temp-ad-image.png")))
            {
                File.Delete(System.Web.Hosting.HostingEnvironment.MapPath("~/images/temp-ad-image.png"));
            }
            else if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/temp-ad-image.jpg")))
            {
                File.Delete(System.Web.Hosting.HostingEnvironment.MapPath("~/images/temp-ad-image.jpg"));
            }
            /*
            string RestaurantID = Request.QueryString["RestaurantID"];
            bool success = false;
            success = CatalogAccess.UpdateEstablishment(RestaurantID, RestaurantName_TextBox.Text, RestaurantPriority_TextBox.Text, RestaurantTier_DropDownList.SelectedValue, RestaurantMotto_TextBox.Text, RestaurantCuisine_TextBox.Text, RestaurantStreetAddress_TextBox.Text, RestaurantCity_TextBox.Text, RestaurantState_TextBox.Text, RestaurantZipCode_TextBox.Text, RestaurantPhone_TextBox.Text, RestaurantFax_TextBox.Text, RestaurantEmail_TextBox.Text, RestaurantWebSite_TextBox.Text, RestaurantSocialLink1_TextBox.Text, RestaurantSocialLink2_TextBox.Text, RestaurantSocialLink3_TextBox.Text, RestaurantAboutUs_TextBox.Text);
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
            */
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Link.ToRoot("default.aspx"));
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                try
                {
                    if (FileUpload1.PostedFile.ContentType == "image/jpeg" || FileUpload1.PostedFile.ContentType == "image/png")
                    {
                        string newFileName = "";
                        if (CheckImage(FileUpload1.PostedFile, 32768, 250, 250))
                        {
                            newFileName = "temp-ad-image" + Path.GetExtension(FileUpload1.FileName);
                            FileUpload1.SaveAs(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + newFileName));
                            Image1.ImageUrl = "/images/" + newFileName;
                            ClientMessageBox.Show("Upload status: File uploaded!", this);
                        }
                        else
                        {
                            ClientMessageBox.Show("Upload status: Please check the image size or dimensions!", this);
                        }
                    }
                    else
                    {
                        ClientMessageBox.Show("Upload status: Only JPEG and PNG files are accepted!", this);
                    }
                }
                catch (Exception ex)
                {
                    ClientMessageBox.Show("Upload status: The file could not be uploaded. The following error occured: " + ex.Message, this);
                }
            }
        }

        protected bool CheckImage(HttpPostedFile postedImage, int maxSize, int maxHeight, int maxWidth)
        {
            //Determine type and filename of uploaded image
            string UploadedImageType = postedImage.ContentType.ToString().ToLower();
            string UploadedImageFileName = postedImage.FileName;

            //Create an image object from the uploaded file
            System.Drawing.Image UploadedImage = System.Drawing.Image.FromStream(postedImage.InputStream);

            //Determine width and height of uploaded image
            float UploadedImageWidth = UploadedImage.PhysicalDimension.Width;
            float UploadedImageHeight = UploadedImage.PhysicalDimension.Height;

            //Check that image does not exceed maximum dimension settings
            if (postedImage.ContentLength > maxSize || UploadedImageWidth > maxWidth || UploadedImageHeight > maxHeight)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}