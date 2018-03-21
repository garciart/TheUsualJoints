using System;
using System.IO;
using System.Web;
using System.Web.UI;
using TheUsualJoints.App_Start;

namespace TheUsualJoints.Admin {
    public partial class restaurant_image : TheUsualJoints.App_Start.basepage {
        protected void Page_Load(object sender, EventArgs e) {
            if (HttpContext.Current.User.Identity.IsAuthenticated) {
                if (HttpContext.Current.User.IsInRole("Administrator") || HttpContext.Current.User.IsInRole("Manager") || HttpContext.Current.User.IsInRole("Owner")) {
                    // if (!IsPostBack) {
                    string RestaurantID = Request.QueryString["RestaurantID"];
                    if (RestaurantID == null) {
                        goBack();
                    } else {
                        // Get data
                        RestaurantDetails rd = CatalogAccess.GetRestaurantDetails(RestaurantID);
                        Label1.Text = String.Format("Now editing the logo and photos for {0}", rd.RestaurantName);
                        Page.Title = String.Format("Now editing the logo and photos for {0}", rd.RestaurantName);
                        string imageName = (Request.QueryString["Position"] == "0") ?
                            String.Format("restaurants/{0}/{1}", RestaurantID, Utilities.CleanReplace(rd.RestaurantName, "-")) :
                            String.Format("restaurants/{0}/{1}-photo-0{2}", RestaurantID, Utilities.CleanReplace(rd.RestaurantName, "-"), Request.QueryString["Position"]);
                        WarningLabel.Text = (Request.QueryString["Position"] == "0") ?
                            "(Max image size is 32 KB. Max image dimensions are 250 x 250 pixels)" :
                            "(Max image size is 512 KB. Max image dimensions are 768 x 768 pixels)";
                        if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + imageName + ".png")) == true) {
                            RestaurantImage.ImageUrl = Link.ToImage(imageName + ".png");
                        } else if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + imageName + ".jpg")) == true) {
                            RestaurantImage.ImageUrl = Link.ToImage(imageName + ".jpg");
                        } else {
                            RestaurantImage.ImageUrl = Link.ToImage("noimageloaded.png");
                        }
                    }
                    // }
                } else if (HttpContext.Current.User.IsInRole("Advertiser")) {
                    Response.Redirect(Link.ToRoot("default.aspx"));
                } else {
                    Response.Redirect(Link.ToRoot("default.aspx"));
                }
            } else {
                Response.Redirect(Link.ToRoot("default.aspx"));
            }
        }

        protected void UploadButton_Click(object sender, EventArgs e) {
            ClientMessageBox.Show("FileUploadComplete!", this);
            if (FileUpload1.HasFile) {
                try {
                    string RestaurantID = Request.QueryString["RestaurantID"];
                    RestaurantDetails rd = CatalogAccess.GetRestaurantDetails(RestaurantID);
                    if (FileUpload1.PostedFile.ContentType == "image/jpeg" || FileUpload1.PostedFile.ContentType == "image/png") {
                        string imageName = (Request.QueryString["Position"] == "0") ?
                            String.Format("restaurants/{0}/{1}", RestaurantID, Utilities.CleanReplace(rd.RestaurantName, "-")) :
                            String.Format("restaurants/{0}/{1}-photo-0{2}", RestaurantID, Utilities.CleanReplace(rd.RestaurantName, "-"), Request.QueryString["Position"]);
                        string newFileName = "";
                        if ((!imageName.Contains("-photo-")) && CheckImage(FileUpload1.PostedFile, 32768, 250, 250)) {
                            deletePhoto(RestaurantImage.ImageUrl);
                            newFileName = imageName + Path.GetExtension(FileUpload1.FileName);
                            FileUpload1.SaveAs(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + newFileName));
                            RestaurantImage.ImageUrl = Link.ToImage(newFileName);
                            ClientMessageBox.Show("Upload status: File uploaded!", this);
                        } else if (CheckImage(FileUpload1.PostedFile, 512000, 768, 768)) {
                            deletePhoto(RestaurantImage.ImageUrl);
                            newFileName = imageName + Path.GetExtension(FileUpload1.FileName);
                            FileUpload1.SaveAs(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + newFileName));
                            RestaurantImage.ImageUrl = Link.ToImage(newFileName);
                            ClientMessageBox.Show("Upload status: File uploaded!", this);
                        } else {
                            ClientMessageBox.Show("Upload status: Please check the image size or dimensions!", this);
                        }
                    } else {
                        ClientMessageBox.Show("Upload status: Only JPEG and PNG files are accepted!", this);
                    }
                } catch (Exception ex) {
                    ClientMessageBox.Show("Upload status: The file could not be uploaded. The following error occured: " + ex.Message, this);
                }
            }
        }

        protected bool CheckImage(HttpPostedFile postedImage, int maxSize, int maxHeight, int maxWidth) {
            //Determine type and filename of uploaded image
            string UploadedImageType = postedImage.ContentType.ToString().ToLower();
            string UploadedImageFileName = postedImage.FileName;

            //Create an image object from the uploaded file
            System.Drawing.Image UploadedImage = System.Drawing.Image.FromStream(postedImage.InputStream);

            //Determine width and height of uploaded image
            float UploadedImageWidth = UploadedImage.PhysicalDimension.Width;
            float UploadedImageHeight = UploadedImage.PhysicalDimension.Height;

            //Check that image does not exceed maximum dimension settings
            if (postedImage.ContentLength > maxSize || UploadedImageWidth > maxWidth || UploadedImageHeight > maxHeight) {
                return false;
            } else {
                return true;
            }
        }

        protected void deletePhotoButton_Click(object sender, EventArgs e) {
            string imageURL = RestaurantImage.ImageUrl;
            if (imageURL.Contains("noimageloaded")) {
                ClientMessageBox.Show("No image to delete!", this);
            } else {
                deletePhoto(imageURL);
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void UploadCompleteButton_Click(object sender, EventArgs e) {
            // Response.Redirect(Request.QueryString["Referrer"]);
            goBack();
        }

        protected void deletePhoto(string imageURL) {
            System.Uri url = new System.Uri(imageURL);
            string imageFilePath = url.LocalPath;
            System.IO.File.Delete(System.Web.Hosting.HostingEnvironment.MapPath("~" + imageFilePath));
        }

        protected void goBack() {
            switch (Request.QueryString["Referrer"]) {
                case "add": {
                        Response.Redirect(Link.ToAdmin(String.Format("restaurant-add.aspx?RestaurantID={0}", Request.QueryString["RestaurantID"])));
                        break;
                    }
                case "info":
                default: {
                        Response.Redirect(Link.ToAdmin(String.Format("restaurant-info.aspx?RestaurantID={0}", Request.QueryString["RestaurantID"])));
                        break;
                    }
            }
        }
    }
}