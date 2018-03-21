using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TheUsualJoints.App_Start;

namespace TheUsualJoints
{
    public partial class three_column : System.Web.UI.MasterPage
    {
        // Call at the beginning and then only once, or it will regenerate the same set of numbers...
        private Random getRandom = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                UserControl uc = (UserControl)Page.LoadControl(@"~/status-bar.ascx");
                Control status_bar_PlaceHolder1 = FindControl("status_bar_PlaceHolder1");
                status_bar_PlaceHolder1.Controls.Add(uc);
            }

            // don't reload data during postbacks
            if (!IsPostBack)
            {
                // Retrieve CityID from the query string
                string CityID = Request.QueryString["CityID"];
                if (CityID != null)
                {
                    // Retrieve city and state details and display them
                    CityDetails cd = CatalogAccess.GetCityDetails(CityID);
                    tricolumn_Image1.ImageUrl = Link.ToImage(String.Format("{0}-{1}.png", Utilities.CleanReplace(cd.CityName, "-"), cd.CityState.ToLower()));
                    // Image1.ImageUrl = Link.ToImage("smaller-header-logo.png");
                    tricolumn_Image1.AlternateText = String.Format("Welcome to What's Up, {0}, {1}!", HttpUtility.HtmlEncode(cd.CityName), HttpUtility.HtmlEncode(cd.CityState));
                    tricolumn_Image1.ToolTip = String.Format("Welcome to What's Up, {0}, {1}!", HttpUtility.HtmlEncode(cd.CityName), HttpUtility.HtmlEncode(cd.CityState));
                    weather_a1.HRef = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:" + cd.CityZipCode + ".1.99999&bannertypeclick=wu_clean2day";
                    weather_a1.Title = String.Format("{0}, {1} Weather Forecast", cd.CityName, cd.CityState);
                    weather_img.Src = "http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_clean2day_cond&ForcedCity=" + cd.CityName + "&ForcedState=" + cd.CityState + "&zip=" + cd.CityZipCode + "&language=EN";
                    weather_img.Alt = String.Format("{0}, {1} Weather Forecast", cd.CityName, cd.CityState);
                    weather_HyperLink1.Text = String.Format("Click for more {0}, {1} weather!", cd.CityName, cd.CityState);
                    weather_HyperLink1.NavigateUrl = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:" + cd.CityZipCode + ".1.99999&bannertypeclick=wu_clean2day";
                    weather_HyperLink1.ToolTip = String.Format("Click for more {0}, {1} weather!", cd.CityName, cd.CityState);
                    if (Request.QueryString["RestaurantID"] != null)
                    {
                        tricolumn_HyperLink1.Text = String.Format("[Back to {0}, {1}]", cd.CityName, cd.CityState);
                        tricolumn_HyperLink1.ToolTip = String.Format("[Back to {0}, {1}]", cd.CityName, cd.CityState);
                        tricolumn_HyperLink1.NavigateUrl = Link.ToCity(CityID);
                        Repeater1.DataSource = MixAdvertisers(CatalogAccess.GetAdvertisersInCity(CityID, "4", "3", true));
                        Repeater1.DataBind();
                        DisplayTopAds(HyperLink2, CityID, "4", "1", true);
                    }
                    else
                    {
                        tricolumn_HyperLink1.NavigateUrl = Link.ToRoot("Default");
                        Repeater1.DataSource = MixAdvertisers(CatalogAccess.GetAdvertisersInCity(CityID, "3", "3", true));
                        Repeater1.DataBind();
                        DisplayTopAds(HyperLink2, CityID, "3", "1", true);
                    }
                    DataTable Events = CatalogAccess.GetUpcomingEventsInCity(CityID);
                    if (Events.Rows.Count != 0)
                    {
                        Repeater2.DataSource = Events;
                        Panel1.Visible = true;
                        Label1.Text = String.Format("Coming to {0}, {1}!", cd.CityName, cd.CityState);
                        Label1.Visible = true;
                    }
                    Repeater2.DataBind();
                    HtmlIframe facebook_iframe = (HtmlIframe)FindControl("facebook_iframe");
                    facebook_iframe.Src = "//www.facebook.com/plugins/likebox.php?href=" + cd.CitySocialLink1 + "&amp;width=292&amp;height=590&amp;show_faces=true&amp;colorscheme=light&amp;stream=true&amp;border_color&amp;header=true";
                    DataTable Links = CatalogAccess.GetLinksInCity(CityID, true);
                    if (Links.Rows.Count != 0)
                    {
                        Repeater3.DataSource = Links;
                        Panel2.Visible = true;
                        Label5.Text = String.Format("{0}, {1} Links:", cd.CityName, cd.CityState);
                        Label5.Visible = true;
                    }
                    Repeater3.DataBind();
                }
                else
                {
                    tricolumn_Image1.ImageUrl = Link.ToImage("smaller-header-logo.png");
                    tricolumn_Image1.AlternateText = "Welcome to The Usual Joints - What To Do Tonight!";
                    tricolumn_Image1.ToolTip = "Welcome to The Usual Joints - What To Do Tonight!";
                }
            }
        }

        protected void Repeater1_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            HyperLink repeater_HyperLink1 = (HyperLink)e.Item.FindControl("Repeater1_HyperLink1");

            System.Data.DataRowView drv = (System.Data.DataRowView)(e.Item.DataItem);
            string advertiserID = drv.Row["AdvertiserID"].ToString();
            string advertiserName = drv.Row["AdvertiserName"].ToString();
            string imageName;
            HtmlImage Repeater1_img = (HtmlImage)e.Item.FindControl("Repeater1_img");

            if(String.IsNullOrEmpty(drv.Row["AdvertiserAltImage"].ToString()))
            {
                imageName = "advertisers/" + advertiserID + "/" + Utilities.CleanReplace(advertiserName, "-");
                if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + imageName + ".png")) == true)
                {
                    Repeater1_img.Src = Link.ToImage(imageName + ".png");
                }
                else if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + imageName + ".jpg")) == true)
                {
                    Repeater1_img.Src = Link.ToImage(imageName + ".jpg");
                }
                else
                {
                    Repeater1_img.Src = Link.ToImage("noimageloaded.png");
                }                
            }
            else
            {
                Repeater1_img.Src = Link.ToImage("noimageloaded.png");
                Repeater1_img.Src = drv.Row["AdvertiserAltImage"].ToString();
            }
        }

        protected DataTable MixAdvertisers(DataTable dt)
        {
            /* Thanks to LazyAssCoder at http://www.lazyasscoder.com/Article.aspx?id=22&title=How+To%3A+Randomize+the+order+of+the+DataRows+in+a+DataTable */
            dt.Columns.Add(new DataColumn("RandomNum", Type.GetType("System.Int32")));

            Random random = new Random();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["RandomNum"] = random.Next(1000);
            }

            DataView dv = new DataView(dt);
            dv.Sort = "AdvertiserPriority ASC, RandomNum DESC";
            dt = dv.ToTable();
            return dt;
        }

        protected void DisplayTopAds(WebControl myControl, string CityID, string PageID, string BlockID, bool AdvertiserActive)
        {
            DataTable topAds = MixAdvertisers(CatalogAccess.GetAdvertisersInCity(CityID, PageID, BlockID, AdvertiserActive));
            if (topAds.Rows.Count != 0)
            {

                HyperLink control = (HyperLink)myControl;
                control.Text = topAds.Rows[0]["AdvertiserName"].ToString();
                if (String.IsNullOrEmpty(topAds.Rows[0]["AdvertiserAltText"].ToString()) == false)
                {
                    control.ToolTip = topAds.Rows[0]["AdvertiserAltText"].ToString();
                }
                if (String.IsNullOrEmpty(topAds.Rows[0]["AdvertiserAltImage"].ToString()) == false)
                {
                    control.ImageUrl = topAds.Rows[0]["AdvertiserAltImage"].ToString();
                }
                if (String.IsNullOrEmpty(topAds.Rows[0]["AdvertiserNavigateURL"].ToString()) == false)
                {
                    control.NavigateUrl = topAds.Rows[0]["AdvertiserNavigateURL"].ToString();
                }
                control.Visible = true;
            }
        }
    }
}