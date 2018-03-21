using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TheUsualJoints.App_Start;

namespace TheUsualJoints
{
    public partial class city : TheUsualJoints.App_Start.basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Retrieve CityID from the query string
            string CityID = Request.QueryString["CityID"];
            if (CityID != null)
            {
                // don't reload data during postbacks
                if (!IsPostBack)
                {
                    // 301 redirect to the proper URL if necessary
                    Link.CheckCityUrl(CityID);
                    // Retrieve city and state details and display them
                    CityDetails cd = CatalogAccess.GetCityDetails(CityID);
                    HttpCapabilitiesBase browser = Request.Browser;
                    if (Utilities.isMobile() == true || browser.IsMobileDevice == true)
                    {
                        Control restaurant_PlaceHolder1 = FindControl("city_PlaceHolder1");
                        city_HyperLink1.NavigateUrl = Link.ToRoot("Default");
                        city_HyperLink1.Visible = true;
                        Control restaurant_PlaceHolder2 = FindControl("city_PlaceHolder2");
                        city_HyperLink1.NavigateUrl = Link.ToRoot("Default");
                        city_HyperLink2.Visible = true;
                        DataList1.RepeatColumns = 0;
                    }
                    else
                    {
                        DataList1.RepeatColumns = 2;
                    }
                    if (Page.Theme == "mobile")
                    {
                        city_Label1.Text = String.Format("{0} Night in {1}, {2}!", DateTime.Today.DayOfWeek.ToString(), HttpUtility.HtmlEncode(cd.CityName), HttpUtility.HtmlEncode(cd.CityState));
                        PlaceHolder1.Controls.Add(new LiteralControl(String.Format("<p>Find out more!</p><p><img alt=\"Social Links!\" src=\"{0}\" usemap=\"#social_media_bar_map\" height=\"32\" width=\"146\" /><map name=\"social_media_bar_map\"><area shape=\"rect\" coords=\"0,0,32,32\" href=\"http://{1}\" target=\"_blank\" title=\"Follow us on Facebook\" alt=\"Facebook\" /><area shape=\"rect\" coords=\"38,0,70,32\" href=\"http://{2}\" target=\"_blank\" title=\"Follow us on Twitter\" alt=\"Twitter\" /><area shape=\"rect\" coords=\"76,0,108,32\" href=\"http://{3}\" target=\"_blank\" title=\"Follow us on Google+\" alt=\"Google+\" /><area shape=\"rect\" coords=\"114,0,146,32\" href=\"mailto:info@{4}\" target=\"_self\" title=\"Email Us!\" alt=\"Email Us!\" /></map></p>", Link.ToImage("social-media-bar-inline.png"), cd.CitySocialLink1, cd.CitySocialLink2, cd.CitySocialLink3, cd.CityDomainName)));
                    }
                    else
                    {
                        city_Label1.Text = String.Format("Tonight in {0}, {1}!", HttpUtility.HtmlEncode(cd.CityName), HttpUtility.HtmlEncode(cd.CityState));
                    }
                    city_Label2.Text = String.Format("Looking for where to go or what to do in {0}? Dinner to dancing: Find {0}'s restaurants, clubs and bars below!", HttpUtility.HtmlEncode(cd.CityName));
                    city_Label4.Text = String.Format("Please remember that bars, clubs and restaurants in {0} may change their hours, events and specials without notice. Always check with your bartender or server for the most current information.", HttpUtility.HtmlEncode(cd.CityName));
                    // Set metadata
                    string city_title = String.Format("What To Do In {0}, {1} Tonight!", HttpUtility.HtmlEncode(cd.CityName), HttpUtility.HtmlEncode(cd.CityState));
                    string city_description = String.Format("Looking for where to go or what to do in {0}? Dinner to dancing: Find {0}'s restaurants, clubs and bars at www.{1}!", HttpUtility.HtmlEncode(cd.CityName), HttpUtility.HtmlEncode(cd.CityDomainName));
                    string city_keywords = String.Format("what to do, {0}, {1}, restaurants, clubs, bars", HttpUtility.HtmlEncode(cd.CityName), HttpUtility.HtmlEncode(cd.CityState));
                    string currentURL = String.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Host);
                    this.Title = city_title;
                    this.MetaDescription = city_description;
                    this.MetaKeywords = city_keywords;
                    this.Page.Header.Controls.Add(new LiteralControl("\n<!-- Google Authorship and Publisher Markup -->\n"));
                    this.Page.Header.Controls.Add(new LiteralControl("<link rel=\"author\" href=\"https://plus.google.com/112206483966107114603\" />\n"));
                    this.Page.Header.Controls.Add(new LiteralControl("<meta name=\"google-site-verification\" content=\"ANEJcWIEYrEBFb7Xm46D0tQlvgC4HQ6G-dhGQsRL7w0\" />\n"));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<link rel=\"publisher\" href=\"https://plus.google.com/{0}\" />\n", cd.CityGooglePubID)));
                    this.Page.Header.Controls.Add(new LiteralControl("<!-- Schema.org markup for Google+ -->\n"));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta itemprop=\"name\" content=\"What's Up, {0}?\" />\n", cd.CityName)));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta itemprop=\"description\" content=\"{0}\" />\n", city_description)));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta itemprop=\"image\" content=\"{0}-google-profile-250.jpg\" />\n", Link.ToImage(String.Format("cities/{0}/{1}-{2}", CityID, Utilities.CleanReplace(cd.CityName, "-"), cd.CityState)))));
                    this.Page.Header.Controls.Add(new LiteralControl("<!-- Twitter Card data -->\n"));
                    this.Page.Header.Controls.Add(new LiteralControl("<meta name=\"twitter:card\" content=\"summary\" />\n"));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta name=\"twitter:site\" content=\"@{0}\" />\n", cd.CityTwitterName)));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta name=\"twitter:title\" content=\"{0}\" />\n", city_title)));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta name=\"twitter:description\" content=\"{0}\" />\n", city_description)));
                    this.Page.Header.Controls.Add(new LiteralControl("<!-- Twitter summary card with large image must be at least 280x150px -->\n"));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta name=\"twitter:image:src\" content=\"{0}-twitter-profile-400.jpg\" />\n", Link.ToImage(String.Format("cities/{0}/{1}-{2}", CityID, Utilities.CleanReplace(cd.CityName, "-"), cd.CityState)))));
                    this.Page.Header.Controls.Add(new LiteralControl("<!-- Open Graph data -->\n"));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta property=\"og:title\" content=\"{0}\" />\n", city_title)));
                    this.Page.Header.Controls.Add(new LiteralControl("<meta property=\"og:type\" content=\"website\" />\n"));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta property=\"og:url\" content=\"http://www.{0}/\" />\n", cd.CityDomainName)));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta property=\"og:image\" content=\"{0}-og-image-400.jpg\" />\n", Link.ToImage(String.Format("cities/{0}/{1}-{2}", CityID, Utilities.CleanReplace(cd.CityName, "-"), cd.CityState)))));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta property=\"og:description\" content=\"{0}\" />\n", city_description)));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta property=\"og:site_name\" content=\"{0}\" />\n", city_title)));
                    this.Page.Header.Controls.Add(new LiteralControl("<meta property=\"fb:admins\" content=\"1289024829\" />\n")); 
                    this.Page.Header.Controls.Add(new LiteralControl("<!-- Additional data -->\n"));
                    this.Page.Header.Controls.Add(new LiteralControl("<!-- JSON-LD markup generated by Google Structured Data Markup Helper. -->\n"));
                    this.Page.Header.Controls.Add(new LiteralControl("<script type=\"application/ld+json\"> { \"@context\" : \"http://schema.org\", \"@type\" : \"LocalBusiness\", \"name\" : \"" + city_title +"\", \"email\" : \"info@" + cd.CityDomainName +"\", \"address\" : { \"@type\" : \"PostalAddress\", \"addressLocality\" : \"" + cd.CityName +"\", \"addressRegion\" : \"" + cd.CityState +"\" } } </script>\n"));
                    DataTable routine = CatalogAccess.GetTodaysScheduleForCity(CityID, false);
                    DataTable special = CatalogAccess.GetTodaysRestaurantEvents();

                    routine.Columns["HappyHourTimes"].MaxLength = 100; // Size of HappyHourTimes(50) + size of SpecialHappyHour(50)
                    routine.Columns["HappyHourSpecials"].MaxLength = 600; // Size of HappyHourSpecials(300) + size of SpecialHHSpecials(300)
                    routine.Columns["FoodAndDrinkSpecials"].MaxLength = 600; // Size of FoodAndDrinkSpecials(300) + size of SpecialFoodDrinkSpecials(300)
                    routine.Columns["RestaurantEvents"].MaxLength = 600; // Size of RestaurantEvents(300) + size of SpecialEvent(300)

                    int maxSpecialRowsCount = special.Rows.Count;
                    for (int i = 0; i < routine.Rows.Count; i++)
                    {
                        if (maxSpecialRowsCount != 0)
                        {
                            for (int j = 0; j < maxSpecialRowsCount; j++)
                            {
                                if (special.Rows[j]["RestaurantID"].ToString().Equals(routine.Rows[i]["RestaurantID"].ToString()))
                                {
                                    if (!String.IsNullOrEmpty(special.Rows[j]["SpecialHours"].ToString()))
                                    {
                                        routine.Rows[i]["HoursOfOperation"] = special.Rows[j]["SpecialHours"].ToString();
                                    }
                                    if (!String.IsNullOrEmpty(special.Rows[j]["SpecialHappyHour"].ToString()))
                                    {
                                        if (special.Rows[j]["SpecialHappyHour"].ToString().ToLower() == "0")
                                        {
                                            routine.Rows[i]["HappyHourTimes"] = "";
                                        }
                                        else
                                        {
                                            routine.Rows[i]["HappyHourTimes"] = special.Rows[j]["SpecialHappyHour"].ToString();
                                        }
                                    }
                                    // 0 to Append, 1 to Replace, 2 to Leave Blank
                                    if (!String.IsNullOrEmpty(special.Rows[j]["SpecialHHSpecials"].ToString()))
                                    {
                                        if (special.Rows[j]["SpecialHH_ARBFlag"].ToString().Equals("0"))
                                        {
                                            routine.Rows[i]["HappyHourSpecials"] = routine.Rows[i]["HappyHourSpecials"].ToString() + " " + special.Rows[j]["SpecialHHSpecials"].ToString();
                                        }
                                        else if (special.Rows[j]["SpecialHH_ARBFlag"].ToString().Equals("1"))
                                        {
                                            routine.Rows[i]["HappyHourSpecials"] = special.Rows[j]["SpecialHHSpecials"].ToString();
                                        }
                                        else
                                        {
                                            routine.Rows[i]["HappyHourSpecials"] = "";
                                        }
                                    }
                                    // 0 to Append, 1 to Replace, 2 to Leave Blank
                                    if (!String.IsNullOrEmpty(special.Rows[j]["SpecialFoodDrinkSpecials"].ToString()))
                                    {
                                        if (special.Rows[j]["SpecialFD_ARBFlag"].ToString().Equals("0"))
                                        {
                                            routine.Rows[i]["FoodAndDrinkSpecials"] = routine.Rows[i]["FoodAndDrinkSpecials"].ToString() + " " + special.Rows[j]["SpecialFoodDrinkSpecials"].ToString();
                                        }
                                        else if (special.Rows[j]["SpecialFD_ARBFlag"].ToString().Equals("1"))
                                        {
                                            routine.Rows[i]["FoodAndDrinkSpecials"] = special.Rows[j]["SpecialFoodDrinkSpecials"].ToString();
                                        }
                                        else
                                        {
                                            routine.Rows[i]["FoodAndDrinkSpecials"] = "";
                                        }
                                    }
                                    // 0 to Append, 1 to Replace, 2 to Leave Blank
                                    if (!String.IsNullOrEmpty(special.Rows[j]["SpecialEvent"].ToString()))
                                    {
                                        if (special.Rows[j]["SpecialEvent_ARBFlag"].ToString().Equals("0"))
                                        {
                                            routine.Rows[i]["RestaurantEvents"] = "<span class=\"special_event\">" + routine.Rows[i]["RestaurantEvents"].ToString() + " " + special.Rows[j]["SpecialEvent"].ToString() + "</span>";
                                        }
                                        else if (special.Rows[j]["SpecialEvent_ARBFlag"].ToString().Equals("1"))
                                        {
                                            routine.Rows[i]["RestaurantEvents"] = "<span class=\"special_event\">" + special.Rows[j]["SpecialEvent"].ToString() + "</span>";
                                        }
                                        else
                                        {
                                            routine.Rows[i]["RestaurantEvents"] = "";
                                        }
                                    }
                                }
                            }
                        }
                    }

                    for (int i = 0; i < routine.Rows.Count; i++)
                    {
                        if ((routine.Rows[i]["HoursOfOperation"].ToString()) != "Closed")
                        {
                            if (!String.IsNullOrEmpty(routine.Rows[i]["HoursOfOperation"].ToString()))
                                routine.Rows[i]["HoursOfOperation"] = routine.Rows[i]["HoursOfOperation"].ToString();

                            if (!String.IsNullOrEmpty(routine.Rows[i]["HappyHourTimes"].ToString()))
                                routine.Rows[i]["HappyHourTimes"] = routine.Rows[i]["HappyHourTimes"].ToString();

                            if (!String.IsNullOrEmpty(routine.Rows[i]["HappyHourSpecials"].ToString())) routine.Rows[i]["HappyHourSpecials"] = routine.Rows[i]["HappyHourSpecials"].ToString();

                            if (!String.IsNullOrEmpty(routine.Rows[i]["FoodAndDrinkSpecials"].ToString()))
                                routine.Rows[i]["FoodAndDrinkSpecials"] = routine.Rows[i]["FoodAndDrinkSpecials"].ToString();

                            if (!String.IsNullOrEmpty(routine.Rows[i]["RestaurantEvents"].ToString()))
                                routine.Rows[i]["RestaurantEvents"] = routine.Rows[i]["RestaurantEvents"].ToString();
                        }
                        else
                        {
                            routine.Rows[i]["HoursOfOperation"] = "Closed";
                            routine.Rows[i]["RestaurantMotto"] = "";
                            routine.Rows[i]["HappyHourTimes"] = "";
                            routine.Rows[i]["HappyHourSpecials"] = "";
                            routine.Rows[i]["FoodAndDrinkSpecials"] = "";
                            routine.Rows[i]["RestaurantEvents"] = "";
                        }
                    }
                    DataList1.DataSource = routine;
                }
                DataList1.DataBind();
            }
            else
            {
                Response.Redirect(Link.ToRoot("default.aspx"));
            }
        }

        protected void DataList1_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        {
            HtmlImage city_img = (HtmlImage)e.Item.FindControl("city_img");
            System.Data.DataRowView drv = (System.Data.DataRowView)(e.Item.DataItem);
            string restaurantID = drv.Row["RestaurantID"].ToString();
            string restaurantName = drv.Row["RestaurantName"].ToString();
            string imageName = "restaurants/" + restaurantID + "/" + Utilities.CleanReplace(restaurantName, "-");
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + imageName + ".png")).Equals(true))
            {
                city_img.Src = Link.ToImage(imageName + ".png");
            }
            else if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + imageName + ".jpg")).Equals(true))
            {
                city_img.Src = Link.ToImage(imageName + ".jpg");
            }
            else
            {
                city_img.Src = Link.ToImage("noimageloaded.png");
            }
            city_img.Alt = drv.Row["RestaurantName"].ToString();
        }
    }
}