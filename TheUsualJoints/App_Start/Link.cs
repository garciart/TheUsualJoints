using System;
using System.Web;

namespace TheUsualJoints.App_Start
{
    /// <summary>
    /// Link factory class
    /// </summary>
    public class Link
    {
        // Builds an absolute URL
        private static string BuildAbsolute(string relativeUri)
        {
            // get current uri
            Uri uri = HttpContext.Current.Request.Url;
            // build absolute path
            string app = HttpContext.Current.Request.ApplicationPath;
            if (!app.EndsWith("/")) app += "/";
            /* FOR THE USUAL JOINTS ONLY ON GODADDY */
            if (app.EndsWith("theusualjoints/")) app = "/";
            relativeUri = relativeUri.TrimStart('/');
            // return the absolute path
            return HttpUtility.UrlPathEncode(
            String.Format("http://{0}:{1}{2}{3}", uri.Host, uri.Port, app, relativeUri));
        }

        // Generate link to get rid of virtual directory for Godaddy
        public static string ToRoot(string AspxPage)
        {
            if (!AspxPage.TrimEnd().EndsWith(".aspx")) AspxPage += ".aspx";
            return BuildAbsolute(AspxPage);
        }

        // Generate an admin URL
        public static string ToAdmin(string adminPage)
        {
            // if (!adminPage.TrimEnd().EndsWith(".aspx")) adminPage += ".aspx";
            return BuildAbsolute("/Admin/" + adminPage);
        }

        /*
        // Generate a country URL
        public static string ToCountry(string CountryID)
        {
            return BuildAbsolute(String.Format("test.aspx?CountryID={0}", CountryID));
        }

        // Generate a state URL
        public static string ToState(string CountryID, string StateID)
        {
            return BuildAbsolute(String.Format("test.aspx?CountryID={0}&StateID={1}", CountryID, StateID));
        }
        */

        // Generate a city URL
        public static string ToCity(string CityID)
        {
            // prepare restaurant URL name
            CityDetails cd = CatalogAccess.GetCityDetails(CityID);
            string cityUrlName = Utilities.CleanReplace(cd.CityName, "-");

            // build restaurant URL
            return BuildAbsolute(String.Format("{0}-{1}-c{2}", cityUrlName, cd.CityState.ToLower(), CityID));
            // return BuildAbsolute(String.Format("city.aspx?CountryID={0}&StateID={1}&CityID={2}", CountryID, StateID, CityID));
        }

        // Generate a restaurant URL
        public static string ToRestaurant(string CityID, string RestaurantID)
        {
            // prepare restaurant URL name
            CityDetails cd = CatalogAccess.GetCityDetails(CityID);
            RestaurantDetails rd = CatalogAccess.GetRestaurantDetails(RestaurantID);
            string cityUrlName = Utilities.CleanReplace(cd.CityName, "-");
            string restaurantUrlName = Utilities.CleanReplace(rd.RestaurantName, "-");

            // build restaurant URL
            return BuildAbsolute(String.Format("{0}-{1}-c{2}/{3}-r{4}", cityUrlName, cd.CityState.ToLower(), CityID, restaurantUrlName, RestaurantID));
            // return BuildAbsolute(String.Format("restaurant.aspx?CountryID={0}&StateID={1}&CityID={2}&RestaurantID={3}", CountryID, StateID, CityID, RestaurantID));
        }

        public static string ToImage(string fileName)
        {
            // build product URL
            return BuildAbsolute("/images/" + fileName);
        }

        // 301 redirects to correct product URL if not already there
        public static void CheckCityUrl(string CityID)
        {
            // get requested URL
            HttpContext context = HttpContext.Current;
            string requestedUrl = context.Request.RawUrl;
            // get last part of proper URL
            string properUrl = Link.ToCity(CityID);
            string properUrlTrunc = properUrl.Substring(Math.Abs((properUrl.Length) - (requestedUrl.Length)));
            // 301 redirect to the proper URL if necessary
            if (requestedUrl != properUrlTrunc)
            {
                context.Response.Status = "301 Moved Permanently";
                context.Response.AddHeader("Location", properUrl);
            }
        }

        // 301 redirects to correct product URL if not already there
        public static void CheckRestaurantUrl(string CityID, string RestaurantID)
        {
            // get requested URL
            HttpContext context = HttpContext.Current;
            string requestedUrl = context.Request.RawUrl;
            // get last part of proper URL
            string properUrl = Link.ToRestaurant(CityID, RestaurantID);
            string properUrlTrunc = properUrl.Substring(Math.Abs((properUrl.Length) - (requestedUrl.Length)));
            // 301 redirect to the proper URL if necessary
            if (requestedUrl != properUrlTrunc)
            {
                context.Response.Status = "301 Moved Permanently";
                context.Response.AddHeader("Location", properUrl);
            }
        }
    }
}