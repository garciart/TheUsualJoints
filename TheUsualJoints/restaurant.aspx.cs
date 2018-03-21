using TheUsualJoints.App_Start;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;

namespace TheUsualJoints
{
    public partial class restaurant : TheUsualJoints.App_Start.basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // don't reload data during postbacks
            if (!IsPostBack)
            {
                // Retrieve IDs from the query string
                string CityID = Request.QueryString["CityID"];
                string RestaurantID = Request.QueryString["RestaurantID"];
                if (CityID != null && RestaurantID != null)
                {
                    // 301 redirect to the proper URL if necessary
                    Link.CheckRestaurantUrl(CityID, RestaurantID);
                    // Retrieve city and state details and display them
                    CityDetails cd = CatalogAccess.GetCityDetails(CityID);
                    RestaurantDetails rd = CatalogAccess.GetRestaurantDetails(RestaurantID);
                    HttpCapabilitiesBase browser = Request.Browser;
                    if (Utilities.isMobile() == true || browser.IsMobileDevice == true)
                    {
                        if (Request.QueryString["RestaurantID"] != null)
                        {
                            Control restaurant_PlaceHolder1 = FindControl("restaurant_HyperLink1");
                            restaurant_HyperLink1.Text = String.Format("[Back to {0}, {1}]", cd.CityName, cd.CityState);
                            restaurant_HyperLink1.ToolTip = String.Format("[Back to {0}, {1}]", cd.CityName, cd.CityState);
                            restaurant_HyperLink1.NavigateUrl = Link.ToCity(CityID);
                            restaurant_HyperLink1.Visible = true;
                            Control restaurant_PlaceHolder2 = FindControl("restaurant_HyperLink2");
                            restaurant_HyperLink2.Text = String.Format("[Back to {0}, {1}]", cd.CityName, cd.CityState);
                            restaurant_HyperLink2.ToolTip = String.Format("[Back to {0}, {1}]", cd.CityName, cd.CityState);
                            restaurant_HyperLink2.NavigateUrl = Link.ToCity(CityID);
                            restaurant_HyperLink2.Visible = true;
                        }
                        else
                        {
                            Control restaurant_PlaceHolder1 = FindControl("restaurant_PlaceHolder1");
                            restaurant_HyperLink1.Visible = true;
                            Control restaurant_PlaceHolder2 = FindControl("restaurant_PlaceHolder2");
                            restaurant_HyperLink2.Visible = true;
                        }
                    }
                    restaurant_Label1.Text = String.Format("Welcome to {0}!", rd.RestaurantName);
                    restaurant_img1.Alt = rd.RestaurantName;
                    string imageName = GetImageType("restaurants/" + RestaurantID + "/" + Utilities.CleanReplace(rd.RestaurantName, "-"));
                    restaurant_img1.Src = imageName;
                    restaurant_Label2.Text = Null_Check(rd.RestaurantMotto) != null ? String.Format("{0}<br /><br />", rd.RestaurantMotto) : null;
                    restaurant_Label3.Text = Null_Check(rd.RestaurantCuisine) != null ? String.Format("<b>Cuisine:</b> {0}<br />", rd.RestaurantCuisine) : null;
                    restaurant_Label4.Text = rd.RestaurantStreetAddress + "<br />";
                    restaurant_Label5.Text = String.Format("{0}, {1} {2}<br />", rd.RestaurantCity, rd.RestaurantState, rd.RestaurantZipCode);
                    string google_address = Utilities.CleanReplace((String.Format("{0} {1} {2} {3}", rd.RestaurantStreetAddress, rd.RestaurantCity, rd.RestaurantState, rd.RestaurantZipCode)), "+");
                    restaurant_a1.HRef = String.Format("https://www.google.com/maps/place/{0}", google_address);
                    restaurant_Image1.ImageUrl = String.Format("https://maps.google.com/maps/api/staticmap?center=q={0}&zoom=15&markers={0}&size=300x300&sensor=false", google_address);
                    restaurant_Image1.AlternateText = String.Format("{0},\n{1}, {2} {3}", rd.RestaurantStreetAddress, rd.RestaurantCity, rd.RestaurantState, rd.RestaurantZipCode);
                    restaurant_Image1.ToolTip = String.Format("{0},\n{1}, {2} {3}", rd.RestaurantStreetAddress, rd.RestaurantCity, rd.RestaurantState, rd.RestaurantZipCode);
                    restaurant_Label6.Text = String.Format("<b>Phone:</b> <a href=\"tel:+{0}\" itemprop=\"telephone\" title=\"Call Us!\">{1:# (###) ###-####}</a><br />", rd.RestaurantPhone, Convert.ToInt64(rd.RestaurantPhone));
                    restaurant_Label7.Text = Null_Check(rd.RestaurantFax) != null ? String.Format("<b>Fax:</b> <a href=\"fax:+{0}\" itemprop=\"fax\" title=\"Fax Us!\">{1:# (###) ###-####}</a><br />", rd.RestaurantFax, Convert.ToInt64(rd.RestaurantFax)) : null;
                    restaurant_Label8.Text = Null_Check(rd.RestaurantEmail) != null ? String.Format("<b>E-Mail:</b> <a href=\"mailto:{0}\" itemprop=\"email\" title=\"Email Us!\" />{0}</a><br />", rd.RestaurantEmail) : null;

                    if (Null_Check(rd.RestaurantWebSite) != null || Null_Check(rd.RestaurantSocialLink1) != null || Null_Check(rd.RestaurantSocialLink2) != null || Null_Check(rd.RestaurantSocialLink3) != null)
                    {
                        restaurant_Label9.Text = "<h2>Web Site and Links:</h2>";
                        restaurant_Label10.Text = Null_Check(rd.RestaurantWebSite) != null ? String.Format("<a href=\"{0}\" title=\"{0}\" target=\"_blank\"/>{0}</a><br />", rd.RestaurantWebSite) : null;
                        restaurant_Label11.Text = Null_Check(rd.RestaurantSocialLink1) != null ? String.Format("<a href=\"{0}\" title=\"{0}\" target=\"_blank\"/>{0}</a><br />", rd.RestaurantSocialLink1) : null;
                        restaurant_Label12.Text = Null_Check(rd.RestaurantSocialLink2) != null ? String.Format("<a href=\"{0}\" title=\"{0}\" target=\"_blank\"/>{0}</a><br />", rd.RestaurantSocialLink2) : null;
                        restaurant_Label13.Text = Null_Check(rd.RestaurantSocialLink3) != null ? String.Format("<a href=\"{0}\" title=\"{0}\" target=\"_blank\"/>{0}</a><br />", rd.RestaurantSocialLink3) : null;
                    }
                    restaurant_Label14.Text = Null_Check(rd.RestaurantAboutUs) != null ? String.Format("<h2>About {0}:</h2>{1}<br />", rd.RestaurantName, HttpUtility.HtmlEncode(rd.RestaurantAboutUs)) : null;

                    for (int photoCount = 1; photoCount <= 5; photoCount++)
                    {
                        System.Web.UI.WebControls.Label imageLabel = new System.Web.UI.WebControls.Label();
                        string restaurantImageName = String.Format("restaurants/{0}/{1}-photo-0{2}", RestaurantID, Utilities.CleanReplace(rd.RestaurantName, "-"), photoCount);
                        if ((File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + restaurantImageName + ".png")).Equals(true) || File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + restaurantImageName + ".jpg")).Equals(true)) && photoCount == 1)
                        {
                            restaurant_Label15.Text = String.Format("<h2>Photos of {0}:</h2>", rd.RestaurantName);
                            Panel1.CssClass = "restaurant_Panel1";
                        }

                        if (GetImageType(restaurantImageName) != null)
                        {
                            imageLabel.Text = "<a href=\"" + GetImageType(restaurantImageName) + "\" data-lightbox=\"" + Utilities.CleanReplace(rd.RestaurantName, "-") + "\" title=\"" + rd.RestaurantName + " Photo " + photoCount + "\" data-title=\"" + rd.RestaurantName + " Photo " + photoCount + "\"><img src=\"" + GetImageType(restaurantImageName) + "\" class=\"restaurant_thumb\" alt=\"" + rd.RestaurantName + " Photo " + photoCount + "\"" + "/></a>";
                            Panel1.Controls.Add(imageLabel);
                        }
                    }

                    DataTable routine = CatalogAccess.GetRestaurantRoutine(RestaurantID);

                    routine.Columns["HappyHourTimes"].MaxLength = 100; // Size of HappyHourTimes(50) + size of SpecialHappyHour(50)
                    routine.Columns["HappyHourSpecials"].MaxLength = 600; // Size of HappyHourSpecials(300) + size of SpecialHHSpecials(300)
                    routine.Columns["FoodAndDrinkSpecials"].MaxLength = 600; // Size of FoodAndDrinkSpecials(300) + size of SpecialFoodDrinkSpecials(300)
                    routine.Columns["RestaurantEvents"].MaxLength = 600; // Size of RestaurantEvents(300) + size of SpecialEvent(300)

                    // Get date of the first day (Sunday) of the current week
                    DateTime today = DateTime.Now.Date;
                    int difference = (int)today.DayOfWeek - (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
                    if (difference < 0)
                        difference = 7 + difference;
                    DateTime firstDay = today.AddDays(-difference);

                    routine.Columns.Add("Date", typeof(DateTime));

                    for (int i = 0; i < routine.Rows.Count; i++)
                    {
                        routine.Rows[i]["Date"] = firstDay.AddDays(i);
                        // routine.Rows[i]["Date"] = DateTime.Parse("3/22/15").AddDays(i);
                    }

                    DataTable special = CatalogAccess.GetSpecialScheduleRange(RestaurantID, firstDay.ToShortDateString(), firstDay.AddDays(7).ToShortDateString());

                    int maxSpecialRowsCount = special.Rows.Count;
                    for (int i = 0; i < routine.Rows.Count; i++)
                    {
                        if (maxSpecialRowsCount != 0)
                        {
                            for (int j = 0; j < maxSpecialRowsCount; j++)
                            {
                                if (String.IsNullOrEmpty(special.Rows[j]["SpecialEventEnd"].ToString()))
                                {
                                    special.Rows[j]["SpecialEventEnd"] = special.Rows[j]["SpecialEventStart"];
                                }
                                if (
                                        (
                                            /* String.IsNullOrEmpty(special.Rows[j]["SpecialEventEnd"].ToString())
                                            && */
                                            special.Rows[j]["SpecialEventStart"].ToString().Equals(routine.Rows[i]["Date"].ToString())
                                        ) ||
                                        (
                                            (DateTime)routine.Rows[i]["Date"] >= (DateTime)special.Rows[j]["SpecialEventStart"]
                                            &&
                                            (DateTime)routine.Rows[i]["Date"] <= (DateTime)special.Rows[j]["SpecialEventEnd"]
                                        )
                                    )
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
                                            routine.Rows[i]["RestaurantEvents"] = "<span class=\"restaurant_events\">" + routine.Rows[i]["RestaurantEvents"].ToString() + " " + special.Rows[j]["SpecialEvent"].ToString() + "</span>";
                                        }
                                        else if (special.Rows[j]["SpecialEvent_ARBFlag"].ToString().Equals("1"))
                                        {
                                            routine.Rows[i]["RestaurantEvents"] = "<span class=\"restaurant_events\">" + special.Rows[j]["SpecialEvent"].ToString() + "</span>";
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
                            routine.Rows[i]["HappyHourTimes"] = "";
                            routine.Rows[i]["HappyHourSpecials"] = "";
                            routine.Rows[i]["FoodAndDrinkSpecials"] = "";
                            routine.Rows[i]["RestaurantEvents"] = "";
                        }
                    }
                    DataList1.DataSource = routine;
                    DataList1.DataBind();
                    if (Page.Theme == "mobile")
                    {
                        PlaceHolder1.Controls.Add(new LiteralControl(String.Format("<p>Find out more!</p><p><img alt=\"Links!\" src=\"{0}\" usemap=\"#social_media_bar_map\" height=\"32\" width=\"146\" /><map name=\"social_media_bar_map\"><area shape=\"rect\" coords=\"0,0,32,32\" href=\"http://{1}\" target=\"_blank\" title=\"Follow us on Facebook\" alt=\"Facebook\" /><area shape=\"rect\" coords=\"38,0,70,32\" href=\"http://{2}\" target=\"_blank\" title=\"Follow us on Twitter\" alt=\"Twitter\" /><area shape=\"rect\" coords=\"76,0,108,32\" href=\"http://{3}\" target=\"_blank\" title=\"Follow us on Google+\" alt=\"Google+\" /><area shape=\"rect\" coords=\"114,0,146,32\" href=\"mailto:info@{4}\" target=\"_self\" title=\"Email Us!\" alt=\"Email Us!\" /></map></p>", Link.ToImage("social-media-bar-inline.png"), cd.CitySocialLink1, cd.CitySocialLink2, cd.CitySocialLink3, cd.CityDomainName)));
                    }
                    restaurant_Label20.Text = String.Format("Please remember that {0} may change their information, hours, entertainment and specials without notice. For the most current information, contact {0} using the phone number or email listed above.", HttpUtility.HtmlEncode(rd.RestaurantName));
                    // Set metadata
                    string restaurant_title = String.Format("Welcome to {0} in {1}, {2}!", HttpUtility.HtmlEncode(rd.RestaurantName), HttpUtility.HtmlEncode(rd.RestaurantCity), HttpUtility.HtmlEncode(rd.RestaurantState));
                    string restaurant_description = String.Format("Welcome to {0} in {1}, {2}! {3}", HttpUtility.HtmlEncode(rd.RestaurantName), HttpUtility.HtmlEncode(rd.RestaurantCity), HttpUtility.HtmlEncode(rd.RestaurantState), HttpUtility.HtmlEncode(rd.RestaurantMotto));
                    string restaurant_keywords = String.Format("{0}, {1}, {2}, restaurant", HttpUtility.HtmlEncode(rd.RestaurantName), HttpUtility.HtmlEncode(rd.RestaurantCity), HttpUtility.HtmlEncode(rd.RestaurantState));
                    string currentURL = Link.ToRestaurant(CityID, RestaurantID);
                    this.Title = restaurant_title;
                    this.MetaDescription = restaurant_description;
                    this.MetaKeywords = restaurant_keywords;
                    this.Page.Header.Controls.Add(new LiteralControl("<!-- Schema.org markup for Google+ -->\n"));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta itemprop=\"name\" content=\"What's Up, {0}?\" />\n", cd.CityName)));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta itemprop=\"description\" content=\"{0}\" />\n", restaurant_description)));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta itemprop=\"image\" content=\"{0}-google-profile-250.jpg\" />\n", Link.ToImage(String.Format("cities/{0}/{1}-{2}", CityID, Utilities.CleanReplace(cd.CityName, "-"), cd.CityState)))));

                    this.Page.Header.Controls.Add(new LiteralControl("\n<!-- Google Authorship and Publisher Markup -->\n"));
                    this.Page.Header.Controls.Add(new LiteralControl("<meta name=\"google-site-verification\" content=\"ANEJcWIEYrEBFb7Xm46D0tQlvgC4HQ6G-dhGQsRL7w0\" />\n"));
                    this.Page.Header.Controls.Add(new LiteralControl("<!-- Open Graph data -->\n"));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta property=\"og:title\" content=\"{0}\" />\n", restaurant_title)));
                    this.Page.Header.Controls.Add(new LiteralControl("<meta property=\"og:type\" content=\"website\" />\n"));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta property=\"og:url\" content=\"{0}/\" />\n", currentURL)));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta property=\"og:image\" content=\"{0}\" />\n", imageName)));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta property=\"og:description\" content=\"{0}\" />\n", restaurant_description)));
                    this.Page.Header.Controls.Add(new LiteralControl(String.Format("<meta property=\"og:site_name\" content=\"What's Up, {0}?\" />\n", cd.CityName)));
                    this.Page.Header.Controls.Add(new LiteralControl("<meta property=\"fb:admins\" content=\"1289024829\" />\n"));
                    this.Page.Header.Controls.Add(new LiteralControl("<!-- Additional data -->\n"));
                    this.Page.Header.Controls.Add(new LiteralControl("<!-- JSON-LD markup generated by Google Structured Data Markup Helper. -->\n"));
                    this.Page.Header.Controls.Add(new LiteralControl("<script type=\"application/ld+json\"> { \"@context\" : \"http://schema.org\", \"@type\" : \"LocalBusiness\", \"name\" : \"" + rd.RestaurantName + "\", \"email\" : \"info@" + cd.CityDomainName + "\", \"address\" : { \"@type\" : \"PostalAddress\", \"addressLocality\" : \"" + rd.RestaurantCity + "\", \"addressRegion\" : \"" + rd.RestaurantState + "\", \"postalCode\" : \"" + rd.RestaurantZipCode + "\", \"streetAddress\" : \"" + rd.RestaurantStreetAddress + "\"} } </script>\n"));
                }
                else
                {
                    Response.Redirect(Link.ToRoot("default.aspx"));
                }
            }
        }

        public string Null_Check(object value)
        {
            if (value.ToString() != "")
            {
                return value.ToString() + "<br />";
            }
            else
            {
                return null;
            }
        }

        public string GetImageType(string imageName)
        {
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + imageName + ".png")).Equals(true))
            {
                return (Link.ToImage(imageName + ".png"));
            }
            else if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + imageName + ".jpg")).Equals(true))
            {
                return (Link.ToImage(imageName + ".jpg"));
            }
            else
            {
                return null;
            }
        }

        public string GetImageType(string imageName, bool showNoImage)
        {
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + imageName + ".png")).Equals(true))
            {
                return (Link.ToImage(imageName + ".png"));
            }
            else if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + imageName + ".jpg")).Equals(true))
            {
                return (Link.ToImage(imageName + ".jpg"));
            }
            else
            {
                return (Link.ToImage("noimageloaded.png"));
            }
        }
    }
}