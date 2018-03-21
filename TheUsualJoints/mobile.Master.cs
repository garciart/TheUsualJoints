using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheUsualJoints.App_Start;

namespace TheUsualJoints
{
    public partial class mobile : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // don't reload data during postbacks
            if (!IsPostBack)
            {
                // Retrieve CityID from the query string
                string CityID = Request.QueryString["CityID"];
                if (CityID != null)
                {
                    // Retrieve city and state details and display them
                    CityDetails cd = CatalogAccess.GetCityDetails(CityID);
                    mobile_Image1.ImageUrl = Link.ToImage(String.Format("clear-{0}-{1}-{2}-300.png", Utilities.CleanReplace(cd.CityName, "-"), cd.CityState.ToLower(), cd.CityCountry.ToLower()));
                    // Image1.ImageUrl = Link.ToImage("smaller-header-logo.png");
                    mobile_Image1.AlternateText = String.Format("Welcome to What's Up, {0}, {1}!", HttpUtility.HtmlEncode(cd.CityName), HttpUtility.HtmlEncode(cd.CityState));
                    mobile_Image1.ToolTip = String.Format("Welcome to What's Up, {0}, {1}!", HttpUtility.HtmlEncode(cd.CityName), HttpUtility.HtmlEncode(cd.CityState));
                    if (Request.QueryString["RestaurantID"] != null)
                    {
                        // tricolumn_HyperLink1.Text = String.Format("[Back to {0}, {1}]", cd.CityName, cd.CityState);
                        // tricolumn_HyperLink1.ToolTip = String.Format("[Back to {0}, {1}]", cd.CityName, cd.CityState);
                        // tricolumn_HyperLink1.NavigateUrl = Link.ToCity(CityID);
                        DisplayTopAds(mobile_ad, CityID, "4", "1", true);
                    }
                    else
                    {
                        DisplayTopAds(mobile_ad, CityID, "3", "1", true);
                    }
                }
                else
                {
                    mobile_Image1.ImageUrl = Link.ToImage("clear-default-header-logo.png");
                    mobile_Image1.AlternateText = "Welcome to The Usual Joints - What To Do Tonight!";
                    mobile_Image1.ToolTip = "Welcome to The Usual Joints - What To Do Tonight!";
                    Panel1.Visible = true;
                }
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