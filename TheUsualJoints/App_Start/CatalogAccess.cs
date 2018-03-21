using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace TheUsualJoints.App_Start {
    /// <summary>
    /// Wraps country details data
    /// </summary>
    public struct CountryDetails {
        public string CountryName;
        public string CountryAbbr;
        public bool CountryActive;
    }

    /// <summary>
    /// Wraps state details data
    /// </summary>
    public struct StateDetails {
        public string StateName;
        public string StateAbbr;
        public string StateCountry;
        public bool StateActive;
        public int CountryID;
    }

    /// <summary>
    /// Wraps city details data
    /// </summary>
    public struct CityDetails {
        public string CityName;
        public string CityState;
        public string CityZipCode;
        public string CityCountry;
        public string CitySocialLink1;
        public string CitySocialLink2;
        public string CitySocialLink3;
        public string CityGooglePubID;
        public string CityTwitterName;
        public string CityFacebookPageID;
        public string CityDomainName;
        public string CityHashTag;
        public bool CityActive;
        public int StateID;
    }

    /// <summary>
    /// Wraps restaurant details data
    /// </summary>
    public struct RestaurantDetails {
        public string RestaurantName;
        public int RestaurantPriority;
        public int RestaurantTier;
        public string RestaurantMotto;
        public string RestaurantCuisine;
        public string RestaurantStreetAddress;
        public string RestaurantCity;
        public string RestaurantState;
        public string RestaurantZipCode;
        public string RestaurantCountry;
        public string RestaurantPhone;
        public string RestaurantFax;
        public string RestaurantEmail;
        public string RestaurantWebSite;
        public string RestaurantSocialLink1;
        public string RestaurantSocialLink2;
        public string RestaurantSocialLink3;
        public string RestaurantAboutUs;
        public string RestaurantUserName;
        public bool RestaurantActive;
        public int CityID;
    }

    /// <summary>
    /// Wraps advertiser details data
    /// </summary>
    public struct AdvertiserDetails {
        public string AdvertiserName;
        public string AdvertiserNavigateURL;
        public string AdvertiserAltImage;
        public string AdvertiserAltText;
        public int AdvertiserPriority;
        public int AdvertiserPage;
        public int AdvertiserBlock;
        public bool AdvertiserActive;
        public string AdvertiserScript;
    }

    /// <summary>
    /// Wraps advertiser details data
    /// </summary>
    public struct EventDetails {
        public string EventName;
        public string EventNavigateURL;
        public string EventAltImage;
        public string EventAltText;
        public int EventPriority;
        public int EventPage;
        public int EventBlock;
        public bool EventActive;
        public string EventScript;
    }

    /// <summary>
    /// Wraps advertiser details data
    /// </summary>
    public struct LinkDetails {
        public string LinkName;
        public string LinkNavigateURL;
        public string LinkAltImage;
        public string LinkAltText;
        public int LinkPriority;
        public int LinkPage;
        public int LinkBlock;
        public bool LinkActive;
        public string LinkScript;
    }

    /// <summary>
    /// Product catalog business tier component.
    /// </summary>
    public static class CatalogAccess {
        static CatalogAccess() {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Add a non-routine event to the restaurant's schedule.
        /// </summary>
        public static bool AddRestaurantSpecialEvent(string RestaurantID, string RestaurantName, string ShortDescription, string SpecialEventStart, string SpecialEventEnd, string SpecialHours, string SpecialHappyHour, string SpecialHHSpecials, string SpecialHH_ARBFlag, string SpecialFoodDrinkSpecials, string SpecialFD_ARBFlag, string SpecialEvent, string SpecialEvent_ARBFlag) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "INSERT INTO RestaurantEvents (RestaurantID, RestaurantName, ShortDescription, SpecialEventStart, SpecialEventEnd, SpecialHours, SpecialHappyHour, SpecialHHSpecials, SpecialHH_ARBFlag, SpecialFoodDrinkSpecials, SpecialFD_ARBFlag, SpecialEvent, SpecialEvent_ARBFlag) " +
                "SELECT @RestaurantID, @RestaurantName, @ShortDescription, @SpecialEventStart, @SpecialEventEnd, @SpecialHours, @SpecialHappyHour, @SpecialHHSpecials, @SpecialHH_ARBFlag, @SpecialFoodDrinkSpecials, @SpecialFD_ARBFlag, @SpecialEvent, @SpecialEvent_ARBFlag " +
                "WHERE NOT EXISTS (SELECT * FROM RestaurantEvents WHERE RestaurantEvents.RestaurantID = @RestaurantID AND RestaurantEvents.SpecialEventStart = @SpecialEventStart)";
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@RestaurantID", CheckAndConvertInt(RestaurantID)));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantName", RestaurantName));
            comm.Parameters.Add(new SQLiteParameter("@ShortDescription", ShortDescription));
            comm.Parameters.Add(new SQLiteParameter("@SpecialEventStart", CheckAndConvertDateTime(SpecialEventStart)));
            comm.Parameters.Add(new SQLiteParameter("@SpecialEventEnd", CheckAndConvertDateTime(String.IsNullOrEmpty(SpecialEventEnd) ? null : SpecialEventEnd)));
            comm.Parameters.Add(new SQLiteParameter("@SpecialHours", String.IsNullOrEmpty(SpecialHours) ? null : SpecialHours));
            comm.Parameters.Add(new SQLiteParameter("@SpecialHappyHour", String.IsNullOrEmpty(SpecialHappyHour) ? null : SpecialHappyHour));
            comm.Parameters.Add(new SQLiteParameter("@SpecialHHSpecials", String.IsNullOrEmpty(SpecialHHSpecials) ? null : SpecialHHSpecials));
            comm.Parameters.Add(new SQLiteParameter("@SpecialHH_ARBFlag", String.IsNullOrEmpty(SpecialHH_ARBFlag) ? null : SpecialHH_ARBFlag));
            comm.Parameters.Add(new SQLiteParameter("@SpecialFoodDrinkSpecials", String.IsNullOrEmpty(SpecialFoodDrinkSpecials) ? null : SpecialFoodDrinkSpecials));
            comm.Parameters.Add(new SQLiteParameter("@SpecialFD_ARBFlag", String.IsNullOrEmpty(SpecialFD_ARBFlag) ? null : SpecialFD_ARBFlag));
            comm.Parameters.Add(new SQLiteParameter("@SpecialEvent", String.IsNullOrEmpty(SpecialEvent) ? null : SpecialEvent));
            comm.Parameters.Add(new SQLiteParameter("@SpecialEvent_ARBFlag", String.IsNullOrEmpty(SpecialEvent_ARBFlag) ? null : SpecialEvent_ARBFlag));
            // result will represent the number of changed rows
            int result = -1;
            try {
                // execute the procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            } catch {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result >= 1);
        }

        /// <summary>
        /// Delete a non-routine event from the restaurant's schedule.
        /// </summary>
        public static bool DeleteRestaurantSpecialEvent(string SpecialEventID) {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "DELETE FROM RestaurantEvents WHERE SpecialEventID = @SpecialEventID";
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@SpecialEventID", CheckAndConvertInt(SpecialEventID)));
            // result will represent the number of changed rows
            int result = -1;
            try {
                // execute the procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            } catch {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        /// <summary>
        /// Retrieve a list of advertisers for a city. Set active to TRUE to retrieve only active restaurants.
        /// </summary>
        public static DataTable GetAdvertisersInCity(string CityID, bool AdvertiserActive) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "SELECT * FROM Advertiser INNER JOIN AdvertiserCity ON Advertiser.AdvertiserID = AdvertiserCity.AdvertiserID WHERE AdvertiserCity.CityID = @CityID AND Advertiser.AdvertiserActive = @AdvertiserActive";
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@CityID", CheckAndConvertInt(CityID)));
            comm.Parameters.Add(new SQLiteParameter("@AdvertiserActive", CheckAndConvertBool(AdvertiserActive)));
            // execute the procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve a list of advertisers for a city.
        /// PageID codes are 0 for Info, 1 for Country, 2 for State, 3 for City and 4 for Restaurant Pages.
        /// BlockID codes are 1 for center top, 2 for center bottom and 3 for right side.
        /// Set active to TRUE to retrieve only active restaurants.
        /// </summary>
        public static DataTable GetAdvertisersInCity(string CityID, string PageID, string BlockID, bool AdvertiserActive) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "SELECT * FROM Advertiser INNER JOIN AdvertiserCity ON Advertiser.AdvertiserID = AdvertiserCity.AdvertiserID WHERE AdvertiserCity.CityID = @CityID AND Advertiser.AdvertiserPage = @AdvertiserPage AND Advertiser.AdvertiserBlock = @AdvertiserBlock AND Advertiser.AdvertiserActive = @AdvertiserActive";
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@CityID", CheckAndConvertInt(CityID)));
            comm.Parameters.Add(new SQLiteParameter("@AdvertiserPage", CheckAndConvertInt(PageID)));
            comm.Parameters.Add(new SQLiteParameter("@AdvertiserBlock", CheckAndConvertInt(BlockID)));
            comm.Parameters.Add(new SQLiteParameter("@AdvertiserActive", CheckAndConvertBool(AdvertiserActive)));
            // execute the procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve a list of links for a city. Set active to TRUE to retrieve only active links.
        /// </summary>
        public static DataTable GetLinksInCity(string CityID, bool LinkActive) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "SELECT * FROM Links INNER JOIN LinkCity ON Links.LinkID = LinkCity.LinkID WHERE LinkCity.CityID = @CityID AND Links.LinkActive = @LinkActive";
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@CityID", CheckAndConvertInt(CityID)));
            comm.Parameters.Add(new SQLiteParameter("@LinkActive", CheckAndConvertBool(LinkActive)));
            // execute the procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve a list of cities in a state. Set active to TRUE to retrieve only active cities.
        /// </summary>
        public static DataTable GetCitiesInState(string StateID, bool CityActive) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            if (CityActive == true) {
                comm.CommandText = "SELECT * FROM City WHERE StateID = @StateID AND CityActive = @CityActive ORDER BY CityName ASC";
            } else {
                comm.CommandText = "SELECT * FROM City WHERE StateID = @StateID ORDER BY CityName ASC";
            }
            comm.Parameters.Add(new SQLiteParameter("@StateID", CheckAndConvertInt(StateID)));
            comm.Parameters.Add(new SQLiteParameter("@CityActive", CheckAndConvertBool(CityActive)));
            // execute the procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve the details of a city.
        /// </summary>
        public static CityDetails GetCityDetails(string CityID) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "SELECT * FROM City WHERE CityID = @CityID";
            comm.Parameters.Add(new SQLiteParameter("@CityID", CheckAndConvertInt(CityID)));
            // execute the procedure
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // wrap retrieved data in a CityDetails object
            CityDetails details = new CityDetails();
            if (table.Rows.Count > 0) {
                details.CityName = table.Rows[0]["CityName"].ToString();
                details.CityState = table.Rows[0]["CityState"].ToString();
                details.CityCountry = table.Rows[0]["CityCountry"].ToString();
                details.CityZipCode = table.Rows[0]["CityZipCode"].ToString();
                details.CitySocialLink1 = table.Rows[0]["CitySocialLink1"].ToString();
                details.CitySocialLink2 = table.Rows[0]["CitySocialLink2"].ToString();
                details.CitySocialLink3 = table.Rows[0]["CitySocialLink3"].ToString();
                details.CityGooglePubID = table.Rows[0]["CityGooglePubID"].ToString();
                details.CityTwitterName = table.Rows[0]["CityTwitterName"].ToString();
                details.CityFacebookPageID = table.Rows[0]["CityFacebookPageID"].ToString();
                details.CityDomainName = table.Rows[0]["CityDomainName"].ToString();
                details.CityHashTag = table.Rows[0]["CityHashTag"].ToString();
                details.CityActive = Convert.ToBoolean(int.Parse(table.Rows[0]["CityActive"].ToString()));
            }
            // return City details
            return details;
        }

        /// <summary>
        /// Retrieve a list of countries. Set active to TRUE to retrieve only active countries.
        /// </summary>
        public static DataTable GetCountries(bool CountryActive) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            if (CountryActive == true) {
                comm.CommandText = "SELECT * FROM Country WHERE CountryActive = @CountryActive ORDER BY CountryName ASC";
            } else {
                comm.CommandText = "SELECT * FROM Country ORDER BY CountryName ASC";
            }
            comm.Parameters.Add(new SQLiteParameter("@CountryActive", CheckAndConvertBool(CountryActive)));
            // execute the procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve the details of a restaurant.
        /// </summary>
        public static RestaurantDetails GetRestaurantDetails(string RestaurantID) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "SELECT * FROM Restaurant WHERE RestaurantID = @RestaurantID";
            comm.Parameters.Add(new SQLiteParameter("@RestaurantID", CheckAndConvertInt(RestaurantID)));
            // execute the procedure
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // wrap retrieved data in a RestaurantDetails object
            RestaurantDetails details = new RestaurantDetails();
            if (table.Rows.Count > 0) {
                details.RestaurantName = table.Rows[0]["RestaurantName"].ToString();
                details.RestaurantPriority = int.Parse(table.Rows[0]["RestaurantPriority"].ToString());
                details.RestaurantTier = int.Parse(table.Rows[0]["RestaurantTier"].ToString());
                details.RestaurantMotto = table.Rows[0]["RestaurantMotto"].ToString();
                details.RestaurantCuisine = table.Rows[0]["RestaurantCuisine"].ToString();
                details.RestaurantStreetAddress = table.Rows[0]["RestaurantStreetAddress"].ToString();
                details.RestaurantCity = table.Rows[0]["RestaurantCity"].ToString();
                details.RestaurantState = table.Rows[0]["RestaurantState"].ToString();
                details.RestaurantZipCode = table.Rows[0]["RestaurantZipCode"].ToString();
                details.RestaurantCountry = table.Rows[0]["RestaurantCountry"].ToString();
                details.RestaurantPhone = table.Rows[0]["RestaurantPhone"].ToString();
                details.RestaurantFax = table.Rows[0]["RestaurantFax"].ToString();
                details.RestaurantEmail = table.Rows[0]["RestaurantEmail"].ToString();
                details.RestaurantWebSite = table.Rows[0]["RestaurantWebSite"].ToString();
                details.RestaurantSocialLink1 = table.Rows[0]["RestaurantSocialLink1"].ToString();
                details.RestaurantSocialLink2 = table.Rows[0]["RestaurantSocialLink2"].ToString();
                details.RestaurantSocialLink3 = table.Rows[0]["RestaurantSocialLink3"].ToString();
                details.RestaurantAboutUs = table.Rows[0]["RestaurantAboutUs"].ToString();
                details.RestaurantUserName = table.Rows[0]["RestaurantUserName"].ToString();
                details.RestaurantActive = Convert.ToBoolean(int.Parse(table.Rows[0]["RestaurantActive"].ToString()));
            }
            // return Restaurant details
            return details;
        }

        /// <summary>
        /// Retrieve the special schedule.
        /// </summary>
        public static DataTable GetRestaurantEvents(string RestaurantID) {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "SELECT * FROM RestaurantEvents WHERE RestaurantID = @RestaurantID ORDER BY SpecialEventStart DESC";
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@RestaurantID", CheckAndConvertInt(RestaurantID)));
            // execute the procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve a list of restaurants names. Set active to TRUE to retrieve only active restaurants.
        /// </summary>
        public static DataTable GetRestaurantNames(bool RestaurantActive) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            if (RestaurantActive == true) {
                comm.CommandText = "SELECT RestaurantID, RestaurantName FROM Restaurant WHERE RestaurantActive = @RestaurantActive ORDER BY (REPLACE(REPLACE(REPLACE(Restaurant.RestaurantName, 'The ', ''), 'An ', ''), 'A ', '')) ASC";
            } else {
                comm.CommandText = "SELECT RestaurantID, RestaurantName FROM Restaurant ORDER BY (REPLACE(REPLACE(REPLACE(Restaurant.RestaurantName, 'The ', ''), 'An ', ''), 'A ', '')) ASC";
            }
            comm.Parameters.Add(new SQLiteParameter("@RestaurantActive", CheckAndConvertBool(RestaurantActive)));
            // execute the procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve a restaurant's routine schedule.
        /// </summary>
        public static DataTable GetRestaurantRoutine(string RestaurantID) {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "SELECT * FROM RestaurantRoutine WHERE RestaurantID = @RestaurantID ORDER BY Weekday";
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@RestaurantID", CheckAndConvertInt(RestaurantID)));
            // execute the procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve a list of restaurants in a city. Set active to TRUE to retrieve only active restaurants.
        /// </summary>
        public static DataTable GetRestaurantsInCity(string CityID, bool RestaurantActive) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            if (RestaurantActive == true) {
                comm.CommandText = "SELECT * FROM Restaurant INNER JOIN RestaurantCity ON Restaurant.RestaurantID = RestaurantCity.RestaurantID WHERE RestaurantCity.CityID = @CityID AND Restaurant.RestaurantActive = @RestaurantActive ORDER BY (REPLACE(REPLACE(REPLACE(Restaurant.RestaurantName, 'The ', ''), 'An ', ''), 'A ', '')) ASC";
            } else {
                comm.CommandText = "SELECT * FROM Restaurant INNER JOIN RestaurantCity ON Restaurant.RestaurantID = RestaurantCity.RestaurantID WHERE RestaurantCity.CityID = @CityID ORDER BY (REPLACE(REPLACE(REPLACE(Restaurant.RestaurantName, 'The ', ''), 'An ', ''), 'A ', '')) ASC";
            }
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@CityID", CheckAndConvertInt(CityID)));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantActive", CheckAndConvertBool(RestaurantActive)));
            // execute the procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve a restaurant's special events.
        /// </summary>
        public static DataTable GetSpecialScheduleRange(string RestaurantID, string DateStart, string DateEnd) {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText =
                "SELECT * FROM RestaurantEvents WHERE RestaurantID = @RestaurantID AND " +
                "(date(SpecialEventEnd) IS NULL AND date(SpecialEventStart) BETWEEN date(@DateStart) AND date(@DateEnd)) OR " +
                "(date(SpecialEventEnd) IS NOT NULL AND ( " +
                "(date(SpecialEventStart) BETWEEN date(@DateStart) AND date(@DateEnd)) OR " +
                "(date(SpecialEventEnd) BETWEEN date(@DateStart) AND date(@DateEnd))" +
                ")) " +
                "ORDER BY SpecialEventStart DESC";
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@RestaurantID", CheckAndConvertInt(RestaurantID)));
            comm.Parameters.Add(new SQLiteParameter("@DateStart", CheckAndConvertDateTime(DateStart)));
            comm.Parameters.Add(new SQLiteParameter("@DateEnd", CheckAndConvertDateTime(DateEnd)));
            // execute the procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve a list of states in a country. Set active to TRUE to retrieve only active states.
        /// </summary>
        public static DataTable GetStatesInCountry(string CountryID, bool StateActive) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            if (StateActive == true) {
                comm.CommandText = "SELECT * FROM State WHERE CountryID = @CountryID AND StateActive = @StateActive ORDER BY StateName ASC";
            } else {
                comm.CommandText = "SELECT * FROM State WHERE CountryID = @CountryID ORDER BY StateName ASC";
            }
            comm.Parameters.Add(new SQLiteParameter("@CountryID", CheckAndConvertInt(CountryID)));
            comm.Parameters.Add(new SQLiteParameter("@StateActive", CheckAndConvertBool(StateActive)));
            // execute the procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve a list of today's special events for all restaurants.
        /// </summary>
        public static DataTable GetTodaysRestaurantEvents() {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "SELECT * FROM RestaurantEvents " +
                "WHERE SpecialEventStart = date('now', 'localtime') " +
                "ORDER BY SpecialEventID";
            // create the parameters
            string myToday = CheckAndConvertDateTime(DateTime.Today.ToShortDateString());
            comm.Parameters.Add(new SQLiteParameter("@Date", CheckAndConvertDateTime(DateTime.Today.ToShortDateString())));
            // execute the procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve today's schedule for the city's restaurants.
        /// </summary>
        public static DataTable GetTodaysScheduleForCity(string CityID, bool RestaurantActive) {
            string allRestaurantsCheck = (RestaurantActive == true) ? "" : "AND (Restaurant.RestaurantActive = 1) ";
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText =
                // -- populate the table variable with the complete list of products //
                "SELECT RestaurantRoutine.RestaurantID, Restaurant.RestaurantName, Restaurant.RestaurantActive, Restaurant.RestaurantPriority, Restaurant.RestaurantMotto, RestaurantRoutine.Weekday, RestaurantRoutine.HoursOfOperation, RestaurantRoutine.HappyHourTimes, RestaurantRoutine.HappyHourSpecials, RestaurantRoutine.FoodAndDrinkSpecials, RestaurantRoutine.RestaurantEvents " +
                "FROM RestaurantRoutine " +
                "INNER JOIN RestaurantCity ON RestaurantRoutine.RestaurantID = RestaurantCity.RestaurantID " +
                "INNER JOIN Restaurant ON RestaurantRoutine.RestaurantID = Restaurant.RestaurantID " +
                "WHERE (RestaurantCity.CityID = @CityID) " + allRestaurantsCheck + "AND (RestaurantRoutine.Weekday = @Weekday) " +
                "ORDER BY Restaurant.RestaurantPriority, (REPLACE(REPLACE(REPLACE(Restaurant.RestaurantName, 'The ', ''), 'An ', ''), 'A ', '')) ASC";
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@CityID", CheckAndConvertInt(CityID)));
            comm.Parameters.Add(new SQLiteParameter("@Weekday", (int)DateTime.Now.DayOfWeek));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantActive", CheckAndConvertBool(RestaurantActive)));
            // execute the procedure and save the results in a DataTable
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve the list of events in a city within the next 28 days
        /// </summary>
        public static DataTable GetUpcomingEventsInCity(string CityID) {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "SELECT * FROM Events INNER JOIN EventCity ON Events.EventID = EventCity.EventID " +
                "WHERE EventCity.CityID = @CityID " +
                "AND ((EventEnd IS NULL AND ((strftime('%Y-%m-%d', EventStart) BETWEEN date('now') AND date('now', '+28 days')))) " +
                "OR (EventEnd IS NOT NULL AND ((strftime('%Y-%m-%d', EventEnd) >= date('now')) AND ((strftime('%Y-%m-%d', EventStart) BETWEEN date('now') AND date('now', '+28 days')))))) " +
                "ORDER BY EventStart ASC";
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@CityID", CheckAndConvertInt(CityID)));
            // execute the procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Update an existing restaurant.
        /// </summary>
        public static bool UpdateEstablishment(string RestaurantID, string RestaurantName, bool RestaurantActive, string RestaurantPriority, string RestaurantTier, string RestaurantMotto, string RestaurantCuisine, string RestaurantStreetAddress, string RestaurantCity, string RestaurantState, string RestaurantZipCode, string RestaurantPhone, string RestaurantFax, string RestaurantEmail, string RestaurantWebSite, string RestaurantSocialLink1, string RestaurantSocialLink2, string RestaurantSocialLink3, string RestaurantAboutUs) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "UPDATE Restaurant SET RestaurantName = @RestaurantName, RestaurantActive = @RestaurantActive, RestaurantPriority = @RestaurantPriority, RestaurantTier = @RestaurantTier, RestaurantMotto = @RestaurantMotto, RestaurantCuisine = @RestaurantCuisine, RestaurantStreetAddress = @RestaurantStreetAddress, RestaurantCity = @RestaurantCity, RestaurantState = @RestaurantState, RestaurantZipCode = @RestaurantZipCode, RestaurantPhone = @RestaurantPhone, RestaurantFax = @RestaurantFax, RestaurantEmail = @RestaurantEmail, RestaurantWebSite = @RestaurantWebSite, RestaurantSocialLink1 = @RestaurantSocialLink1, RestaurantSocialLink2 = @RestaurantSocialLink2, RestaurantSocialLink3 = @RestaurantSocialLink3, RestaurantAboutUs = @RestaurantAboutUs WHERE RestaurantID = @RestaurantID";
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@RestaurantID", CheckAndConvertInt(RestaurantID)));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantName", RestaurantName));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantActive", CheckAndConvertBool(RestaurantActive)));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantPriority", CheckAndConvertInt(RestaurantPriority)));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantTier", CheckAndConvertInt(RestaurantTier)));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantMotto", String.IsNullOrEmpty(RestaurantMotto) ? null : RestaurantMotto));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantCuisine", String.IsNullOrEmpty(RestaurantCuisine) ? null : RestaurantCuisine));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantStreetAddress", RestaurantStreetAddress));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantCity", RestaurantCity));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantState", RestaurantState));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantZipCode", RestaurantZipCode));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantPhone", RestaurantPhone));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantFax", String.IsNullOrEmpty(RestaurantFax) ? null : RestaurantFax));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantEmail", String.IsNullOrEmpty(RestaurantEmail) ? null : RestaurantEmail));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantWebSite", String.IsNullOrEmpty(RestaurantWebSite) ? null : RestaurantWebSite));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantSocialLink1", String.IsNullOrEmpty(RestaurantSocialLink1) ? null : RestaurantSocialLink1));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantSocialLink2", String.IsNullOrEmpty(RestaurantSocialLink2) ? null : RestaurantSocialLink2));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantSocialLink3", String.IsNullOrEmpty(RestaurantSocialLink3) ? null : RestaurantSocialLink3));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantAboutUs", String.IsNullOrEmpty(RestaurantAboutUs) ? null : RestaurantAboutUs));
            // result will represent the number of changed rows
            int result = -1;
            try {
                // execute the procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            } catch {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result >= 1);
        }

        /// <summary>
        /// Update the routine schedule for a restaurant.
        /// </summary>
        public static bool UpdateRestaurantRoutine(string RestaurantRoutineID, string HoursOfOperation, string HappyHourTimes, string HappyHourSpecials, string FoodAndDrinkSpecials, string RestaurantEvents) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "UPDATE RestaurantRoutine " +
                "SET HoursOfOperation = @HoursOfOperation, HappyHourTimes = @HappyHourTimes, HappyHourSpecials = @HappyHourSpecials, FoodAndDrinkSpecials = @FoodAndDrinkSpecials, RestaurantEvents = @RestaurantEvents " +
                "WHERE RestaurantRoutineID = @RestaurantRoutineID";
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@RestaurantRoutineID", CheckAndConvertInt(RestaurantRoutineID)));
            comm.Parameters.Add(new SQLiteParameter("@HoursOfOperation", HoursOfOperation));
            comm.Parameters.Add(new SQLiteParameter("@HappyHourTimes", String.IsNullOrEmpty(HappyHourTimes) ? null : HappyHourTimes));
            comm.Parameters.Add(new SQLiteParameter("@HappyHourSpecials", String.IsNullOrEmpty(HappyHourSpecials) ? null : HappyHourSpecials));
            comm.Parameters.Add(new SQLiteParameter("@FoodAndDrinkSpecials", String.IsNullOrEmpty(FoodAndDrinkSpecials) ? null : FoodAndDrinkSpecials));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantEvents", String.IsNullOrEmpty(RestaurantEvents) ? null : RestaurantEvents));
            // result will represent the number of changed rows
            int result = -1;
            try {
                // execute the procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            } catch {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result >= 1);
        }

        /// <summary>
        /// Update the special schedule for a restaurant.
        /// </summary>
        public static bool UpdateRestaurantSpecialEvent(string specialEventID, string shortDescription, string specialEventStart, string specialEventEnd, string specialHours, string specialHappyHour, string specialHHSpecials, string specialHH_ARBFlag, string specialFoodDrinkSpecials, string specialFD_ARBFlag, string specialEvent, string specialEvent_ARBFlag) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "UPDATE RestaurantEvents " +
                "SET ShortDescription = @ShortDescription, SpecialEventStart = @SpecialEventStart, SpecialEventEnd = @SpecialEventEnd, SpecialHours = @SpecialHours, SpecialHappyHour = @SpecialHappyHour, SpecialHHSpecials = @SpecialHHSpecials, SpecialHH_ARBFlag = @SpecialHH_ARBFlag, SpecialFoodDrinkSpecials = @SpecialFoodDrinkSpecials, SpecialFD_ARBFlag = @SpecialFD_ARBFlag, SpecialEvent = @SpecialEvent, SpecialEvent_ARBFlag = @SpecialEvent_ARBFlag " +
                "WHERE SpecialEventID = @SpecialEventID";
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@SpecialEventID", specialEventID));
            comm.Parameters.Add(new SQLiteParameter("@ShortDescription", String.IsNullOrEmpty(shortDescription) ? null : shortDescription));
            comm.Parameters.Add(new SQLiteParameter("@SpecialEventStart", String.IsNullOrEmpty(specialEventStart) ? null : specialEventStart));
            comm.Parameters.Add(new SQLiteParameter("@SpecialEventEnd", String.IsNullOrEmpty(specialEventEnd) ? null : specialEventEnd));
            comm.Parameters.Add(new SQLiteParameter("@SpecialHours", String.IsNullOrEmpty(specialHours) ? null : specialHours));
            comm.Parameters.Add(new SQLiteParameter("@SpecialHappyHour", String.IsNullOrEmpty(specialHappyHour) ? null : specialHappyHour));
            comm.Parameters.Add(new SQLiteParameter("@SpecialHHSpecials", String.IsNullOrEmpty(specialHHSpecials) ? null : specialHHSpecials));
            comm.Parameters.Add(new SQLiteParameter("@SpecialHH_ARBFlag", String.IsNullOrEmpty(specialHH_ARBFlag) ? null : specialHH_ARBFlag));
            comm.Parameters.Add(new SQLiteParameter("@SpecialFoodDrinkSpecials", String.IsNullOrEmpty(specialFoodDrinkSpecials) ? null : specialFoodDrinkSpecials));
            comm.Parameters.Add(new SQLiteParameter("@SpecialFD_ARBFlag", String.IsNullOrEmpty(specialFD_ARBFlag) ? null : specialFD_ARBFlag));
            comm.Parameters.Add(new SQLiteParameter("@SpecialEvent", String.IsNullOrEmpty(specialEvent) ? null : specialEvent));
            comm.Parameters.Add(new SQLiteParameter("@SpecialEvent_ARBFlag", String.IsNullOrEmpty(specialEvent_ARBFlag) ? null : specialEvent_ARBFlag));
            // result will represent the number of changed rows
            int result = -1;
            try {
                // execute the procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            } catch {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result >= 1);
        }

        /// <summary>
        /// Test Add
        /// </summary>
        public static bool AddRestaurant(string RestaurantName, bool RestaurantActive, string RestaurantPriority, string RestaurantTier, string RestaurantMotto, string RestaurantCuisine, string RestaurantStreetAddress, string RestaurantCity, string RestaurantState, string RestaurantCountry, string RestaurantZipCode, string RestaurantPhone, string RestaurantFax, string RestaurantEmail, string RestaurantWebSite, string RestaurantSocialLink1, string RestaurantSocialLink2, string RestaurantSocialLink3, string RestaurantAboutUs, string RestaurantUserName, string CityID, out int RestaurantID) {
            /*
             * HOLDS
             * bool AddRestaurant(...),
             * bool AddCity2RestaurantAssoc(int cityID, int restaurantID)
             * bool AddRestaurantWeeklyRoutine(int restaurantID)
             */
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "INSERT INTO Restaurant (RestaurantName, RestaurantActive, RestaurantPriority, RestaurantTier, RestaurantMotto, RestaurantCuisine, RestaurantStreetAddress, RestaurantCity, RestaurantState, RestaurantCountry, RestaurantZipCode, RestaurantPhone, RestaurantFax, RestaurantEmail, RestaurantWebSite, RestaurantSocialLink1, RestaurantSocialLink2, RestaurantSocialLink3, RestaurantAboutUs, RestaurantUserName, CityID) VALUES (@RestaurantName, @RestaurantActive, @RestaurantPriority, @RestaurantTier, @RestaurantMotto, @RestaurantCuisine, @RestaurantStreetAddress, @RestaurantCity, @RestaurantState, @RestaurantCountry, @RestaurantZipCode, @RestaurantPhone, @RestaurantFax, @RestaurantEmail, @RestaurantWebSite, @RestaurantSocialLink1, @RestaurantSocialLink2, @RestaurantSocialLink3, @RestaurantAboutUs, @RestaurantUserName, @CityID)";
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@RestaurantName", RestaurantName));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantActive", CheckAndConvertBool(RestaurantActive)));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantPriority", CheckAndConvertInt(RestaurantPriority)));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantTier", CheckAndConvertInt(RestaurantTier)));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantMotto", String.IsNullOrEmpty(RestaurantMotto) ? null : RestaurantMotto));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantCuisine", String.IsNullOrEmpty(RestaurantCuisine) ? null : RestaurantCuisine));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantStreetAddress", RestaurantStreetAddress));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantCity", RestaurantCity));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantState", RestaurantState));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantCountry", RestaurantCountry));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantZipCode", RestaurantZipCode));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantPhone", RestaurantPhone));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantFax", String.IsNullOrEmpty(RestaurantFax) ? null : RestaurantFax));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantEmail", String.IsNullOrEmpty(RestaurantEmail) ? null : RestaurantEmail));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantWebSite", String.IsNullOrEmpty(RestaurantWebSite) ? null : RestaurantWebSite));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantSocialLink1", String.IsNullOrEmpty(RestaurantSocialLink1) ? null : RestaurantSocialLink1));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantSocialLink2", String.IsNullOrEmpty(RestaurantSocialLink2) ? null : RestaurantSocialLink2));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantSocialLink3", String.IsNullOrEmpty(RestaurantSocialLink3) ? null : RestaurantSocialLink3));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantAboutUs", String.IsNullOrEmpty(RestaurantAboutUs) ? null : RestaurantAboutUs));
            comm.Parameters.Add(new SQLiteParameter("@RestaurantUserName", RestaurantUserName));
            comm.Parameters.Add(new SQLiteParameter("@CityID", CityID));
            // result will represent the number of changed rows. Will be 1 in case of success
            int result = -1;
            try {
                // execute the procedure
                result = GenericDataAccess.ExecuteNonQuery(comm, out RestaurantID);
            } catch {
                // any errors are logged in GenericDataAccess, we ignore them here
                RestaurantID = -1;
            }
            // If successfull, update RestaurantCity junction table
            if (result >= 1) {
                comm.CommandText = "INSERT INTO RestaurantCity (RestaurantID, CityID) VALUES (@RestaurantID, @CityID)";
                comm.Parameters.Add(new SQLiteParameter("@RestaurantID", RestaurantID));
                comm.Parameters.Add(new SQLiteParameter("@CityID", CityID));
                // execute the procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            // If successfull, add placeholder to RestaurantRoutine table
            if (result >= 1) {
                comm.CommandText = "INSERT INTO 'RestaurantRoutine' (RestaurantRoutineID,RestaurantID,RestaurantName,Weekday,HoursOfOperation,HappyHourTimes,HappyHourSpecials,FoodAndDrinkSpecials,RestaurantEvents) VALUES " +
                    "(@RestaurantID, @RestaurantName, 0, 'Closed', '', '', '', '')," +
                    "(@RestaurantID, @RestaurantName, 1, 'Closed', '', '', '', ''), " +
                    "(@RestaurantID, @RestaurantName, 2, 'Closed', '', '', '', ''), " +
                    "(@RestaurantID, @RestaurantName, 3, 'Closed', '', '', '', ''), " +
                    "(@RestaurantID, @RestaurantName, 4, 'Closed', '', '', '', ''), " +
                    "(@RestaurantID, @RestaurantName, 5, 'Closed', '', '', '', ''), " +
                    "(@RestaurantID, @RestaurantName, 6, 'Closed', '', '', '', '');";
                comm.Parameters.Add(new SQLiteParameter("@RestaurantID", RestaurantID));
                comm.Parameters.Add(new SQLiteParameter("@RestaurantName", RestaurantName));
                // execute the procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            return (result >= 1);
        }


































































        /* NOT YET NOT YET NOT YET NOT YET NOT YET NOT YET NOT YET NOT YET NOT YET NOT YET NOT YET NOT YET NOT YET NOT YET NOT YET NOT YET */

            /// <summary>
            /// Retrieve the details of a country.
            /// </summary>
        public static CountryDetails GetCountryDetails(string CountryID) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "SELECT * FROM Country WHERE CountryID = @CountryID";
            comm.Parameters.Add(new SQLiteParameter("@CountryID", CheckAndConvertInt(CountryID)));
            // execute the procedure
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // wrap retrieved data in a CountryDetails object
            CountryDetails details = new CountryDetails();
            if (table.Rows.Count > 0) {
                details.CountryName = table.Rows[0]["CountryName"].ToString();
                details.CountryAbbr = table.Rows[0]["CountryAbbr"].ToString();
                details.CountryActive = Convert.ToBoolean(int.Parse(table.Rows[0]["CountryActive"].ToString()));
            }
            // return Country details
            return details;
        }

        /// <summary>
        /// Retrieve a list of states. Set active to TRUE to retrieve only active states.
        /// </summary>
        public static DataTable GetStates(bool StateActive) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            if (StateActive == true) {
                comm.CommandText = "SELECT * FROM State WHERE StateActive = @StateActive ORDER BY StateName ASC";
            } else {
                comm.CommandText = "SELECT * FROM State ORDER BY StateName ASC";
            }
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@StateActive", CheckAndConvertBool(StateActive)));
            // execute the procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve the details of a state.
        /// </summary>
        public static StateDetails GetStateDetails(string StateID) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "SELECT * FROM State WHERE StateID = @StateID";
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@StateID", CheckAndConvertInt(StateID)));
            // execute the procedure
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // wrap retrieved data in a StateDetails object
            StateDetails details = new StateDetails();
            if (table.Rows.Count > 0) {
                details.StateName = table.Rows[0]["StateName"].ToString();
                details.StateAbbr = table.Rows[0]["StateAbbr"].ToString();
                details.StateCountry = table.Rows[0]["StateCountry"].ToString();
                details.StateActive = Convert.ToBoolean(int.Parse(table.Rows[0]["StateActive"].ToString()));
            }
            // return State details
            return details;
        }

        /// <summary>
        /// Retrieve a list of cities. Set active to TRUE to retrieve only active cities.
        /// </summary>
        public static DataTable GetCities(bool CityActive) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            if (CityActive == true) {
                comm.CommandText = "SELECT * FROM City WHERE CityActive = @CityActive ORDER BY CityName";
            } else {
                comm.CommandText = "SELECT * FROM City ORDER BY CityName";
            }
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@CityActive", CheckAndConvertBool(CityActive)));
            // execute the procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve a list of restaurants. Set active to TRUE to retrieve only active restaurants.
        /// </summary>
        public static DataTable GetRestaurants(bool RestaurantActive) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            if (RestaurantActive == true) {
                comm.CommandText = "SELECT * FROM Restaurant WHERE RestaurantActive = @RestaurantActive ORDER BY (REPLACE(REPLACE(REPLACE(Restaurant.RestaurantName, 'The ', ''), 'An ', ''), 'A ', '')) ASC";
            } else {
                comm.CommandText = "SELECT * FROM Restaurant ORDER BY (REPLACE(REPLACE(REPLACE(Restaurant.RestaurantName, 'The ', ''), 'An ', ''), 'A ', '')) ASC";
            }
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@RestaurantActive", CheckAndConvertBool(RestaurantActive)));
            // execute the procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

        /// <summary>
        /// Test Add
        /// </summary>
        public static bool AddTestSubject(string lastName, string firstName, string birthDate, string age, bool active, out int nameID) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "INSERT INTO TestTable (LastName, FirstName, BirthDate, Age, Active) VALUES (@LastName, @FirstName, @BirthDate, @Age, @Active)";
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@LastName", lastName));
            comm.Parameters.Add(new SQLiteParameter("@FirstName", String.IsNullOrEmpty(firstName) ? null : firstName));
            comm.Parameters.Add(new SQLiteParameter("@BirthDate", CheckAndConvertDateTime(birthDate)));
            comm.Parameters.Add(new SQLiteParameter("@Age", CheckAndConvertInt(age)));
            comm.Parameters.Add(new SQLiteParameter("@Active", CheckAndConvertBool(active)));
            // result will represent the number of changed rows
            int result = -1;
            try {
                // execute the procedure
                result = GenericDataAccess.ExecuteNonQuery(comm, out nameID);
            } catch {
                // any errors are logged in GenericDataAccess, we ignore them here
                nameID = -1;
            }
            // result will be 1 in case of success 
            return (result >= 1);
        }

        /// <summary>
        /// Test Get
        /// </summary>
        public static DataTable GetTestSubjects(bool SubjectActive) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            if (bool.Parse(CheckAndConvertBool(SubjectActive).ToString()) == true) {
                comm.CommandText = "SELECT * FROM TestTable WHERE Active = @Active ORDER BY LastName ASC";
            } else {
                comm.CommandText = "SELECT * FROM TestTable ORDER BY LastName ASC";
            }
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@Active", CheckAndConvertBool(SubjectActive)));
            // execute the procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Test Update
        /// </summary>
        public static bool UpdateTestSubject(string testID, string lastName, string firstName, string birthDate, string age, bool active) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "UPDATE TestTable SET LastName = @LastName, FirstName = @FirstName, BirthDate = @BirthDate, Age = @Age, Active = @Active  WHERE TestID = @TestID";
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@TestID", CheckAndConvertInt(testID)));
            comm.Parameters.Add(new SQLiteParameter("@LastName", String.IsNullOrEmpty(lastName) ? null : lastName));
            comm.Parameters.Add(new SQLiteParameter("@FirstName", firstName));
            comm.Parameters.Add(new SQLiteParameter("@BirthDate", CheckAndConvertDateTime(birthDate)));
            comm.Parameters.Add(new SQLiteParameter("@Age", CheckAndConvertInt(age)));
            comm.Parameters.Add(new SQLiteParameter("@Active", CheckAndConvertBool(active)));
            // result will represent the number of changed rows
            int result = -1;
            try {
                // execute the procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            } catch {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result >= 1);
        }

        /// <summary>
        /// Test Delete
        /// </summary>
        public static bool DeleteTestSubject(string testID) {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // create the procedure
            comm.CommandText = "DELETE FROM WHERE TestID = @TestID";
            // create the parameters
            comm.Parameters.Add(new SQLiteParameter("@TestID", CheckAndConvertInt(testID.ToString())));
            // result will represent the number of changed rows
            int result = -1;
            try {
                // execute the procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            } catch {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result >= 1);
        }

        // SERVER-SIDE VALIDATION FUNCTIONS! //
        public static int CheckAndConvertInt(string value) {
            int newValue = default(int);
            int.TryParse(value, out newValue);
            if (String.IsNullOrEmpty(newValue.ToString()) == true)
                throw new Exception("CatalogAccess to Admin: WTF Over? That's not an integer!");
            else
                return newValue;
        }

        public static string CheckAndConvertDateTime(string value) {
            if (!String.IsNullOrEmpty(value)) {
                DateTime newValue = default(DateTime);
                DateTime.TryParse(value, out newValue);
                if (String.IsNullOrEmpty(newValue.ToString()) == true)
                    throw new Exception("CatalogAccess to Admin: WTF Over? That's not a valid Date!");
                else {
                    return Utilities.ConvertDateTimeToSQLite(newValue);
                }
            } else {
                return null;
            }
        }

        public static Int32 CheckAndConvertBool(bool value) {
            Int16 newValue = default(Int16);
            newValue = Convert.ToInt16(value);
            if (String.IsNullOrEmpty(newValue.ToString()) == true)
                throw new Exception("CatalogAccess to Admin: WTF Over? That's not True or False!");
            else
                return newValue;
        }
    }
}