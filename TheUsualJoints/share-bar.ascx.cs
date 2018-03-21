using System;
using System.Web;
using System.Web.UI;
using TheUsualJoints.App_Start;

namespace TheUsualJoints
{
    public partial class share_bar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve CityID from the query string
                string CityID = Request.QueryString["CityID"];
                if (CityID != null)
                {
                    // Retrieve city details and display them
                    CityDetails cd = CatalogAccess.GetCityDetails(CityID);
                    string twitterHash = String.Format("https://twitter.com/intent/tweet?url=http%3A%2F%2Fwww.{0}&text=What%20to%20do%20in%20{1}?%20Check%20out&via={2}!", cd.CityDomainName, HttpUtility.UrlEncode(cd.CityHashTag), cd.CityTwitterName);
                    PlaceHolder1.Controls.Add(new LiteralControl(String.Format("<img alt=\"Links!\" src=\"/images/share-bar-horizontal-2.png\" usemap=\"#share_bar_map\" height=\"32\" width=\"146\" /><map name=\"share_bar_map\"><area shape=\"rect\" coords=\"0,0,32,32\" href=\"https://www.facebook.com/sharer/sharer.php?u=http%3A%2F%2Fwww.{0}\" target=\"_blank\" title=\"Share on Facebook!\" alt=\"Share on Facebook!\" /><area shape=\"rect\" coords=\"38,0,70,32\" href=\"{1}\" target=\"_blank\" title=\"Tweet!\" alt=\"Tweet!\" /><area shape=\"rect\" coords=\"76,0,108,32\" href=\"https://plus.google.com/share?url=http%3A%2F%2Fwww.{0}\" target=\"_blank\" title=\"Share on Google+!\" alt=\"Share on Google+!\" /><area shape=\"rect\" coords=\"114,0,146,32\" href=\"https://pinterest.com/pin/create/button/?url=http%3A%2F%2Fwww.{0}&media=http%3A%2F%2Fwww.{0}%2Fimages%2Ftuj-pinterest-736.png&description=What%20to%20do%20in%20{2}!\" target=\"_blank\" title=\"Pin It!\" alt=\"Pin It!\" /></map>", cd.CityDomainName, twitterHash, Utilities.CleanReplace(cd.CityName, "%20"))));
                }
                else
                {
                    PlaceHolder1.Controls.Add(new LiteralControl("<img alt=\"Links!\" src=\"/images/share-bar-horizontal-2.png\" usemap=\"#share_bar_map\" height=\"32\" width=\"146\" /><map name=\"share_bar_map\"><area shape=\"rect\" coords=\"0,0,32,32\" href=\"https://www.facebook.com/sharer/sharer.php?u=http%3A%2F%2Fwww.theusualjoints.com\" target=\"_blank\" title=\"Share on Facebook!\" alt=\"Share on Facebook!\" /><area shape=\"rect\" coords=\"38,0,70,32\" href=\"https://twitter.com/intent/tweet?url=http%3A%2F%2Fwww.theusualjoints.com&text=What%20to%20do%20in%20%23sbymd,%20%23ocmd%20and%20%23rehobothbeach?%20Check%20out&via=theusualjoints\" target=\"_blank\" title=\"Tweet!\" alt=\"Tweet!\" /><area shape=\"rect\" coords=\"76,0,108,32\" href=\"https://plus.google.com/share?url=http%3A%2F%2Fwww.theusualjoints.com\" target=\"_blank\" title=\"Share on Google+!\" alt=\"Share on Google+!\" /><area shape=\"rect\" coords=\"114,0,146,32\" href=\"https://pinterest.com/pin/create/button/?url=http%3A%2F%2Fwww.theusualjoints.com&media=http%3A%2F%2Fwww.theusualjoints.com%2Fimages%2Ftuj-pinterest-736.png&description=What%20to%20do%20in%20Ocean%20City%2C%20Salisbury%20and%20Rehoboth%20Beach!\" target=\"_blank\" title=\"Pin It!\" alt=\"Pin It!\" /></map>"));
                }
            }
        }
    }
}