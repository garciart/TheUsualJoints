using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace tuj_app.App_Start
{
    /// <summary>
    /// Wraps country details data
    /// </summary>
    public struct CountryDetails
    {
        public string CountryName;
        public string CountryAbbreviation;
        public bool Active;
    }

    /// <summary>
    /// Wraps state details data
    /// </summary>
    public struct StateDetails
    {
        public int CountryID;
        public string StateName;
        public string StateAbbreviation;
        public bool Active;
    }

    /// <summary>
    /// Wraps city details data
    /// </summary>
    public struct CityDetails
    {
        public int StateID;
        public string CityName;
        public string ZipCode;
        public int TimeZoneID;
        public bool Active;
    }

    /// <summary>
    /// Wraps advertiser details data
    /// </summary>
    public struct AdvertiserDetails
    {
        public int BlockID;
        public string ImageUrl;
        public string NavigateUrl;
        public string AlternateText;
        public string Keyword;
        public int Impressions;
        public bool Active;
    }

    /// <summary>
    /// Wraps establishment details data
    /// </summary>
    public struct EstablishmentDetails
    {
        public string EstablishmentName;
        public int Priority;
        public int Tier;
        public string Logo;
        public string Motto;
        public string Cuisine;
        public string StreetAddress;
        public string CityName;
        public string StateAbbreviation;
        public string ZipCode;
        public float Latitude;
        public float Longitude;
        public string TelephoneNo;
        public string FaxNo;
        public string Email;
        public string WebSite;
        public string Facebook;
        public string AboutUs;
        public string Photo01;
        public string Photo02;
        public string Photo03;
        public string Photo04;
        public string Photo05;
        public bool Active;
    }

    /// <summary>
    /// Wraps event details data
    /// </summary>
    public struct EventDetails
    {
        public string EventName;
        public DateTime Date;
        public DateTime EndDate;
        public string Description;
        public string ImageUrl;
        public string NavigateUrl;
    }

    /// <summary>
    /// Wraps link details data
    /// </summary>
    public struct LinkDetails
    {
        public string LinkName;
        public string LinkUrl;
        public string LinkTitle;
        public string LinkCategoryName;
        public string LinkCategoryRank;
    }

    /// <summary>
    /// Wraps advertiser and establishment user name
    /// </summary>
    public struct EntryUserName
    {
        public string userName;
    }

    /// <summary>
    /// Product catalog business tier component
    /// </summary>
    public class CatalogAccess
    {
        public CatalogAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Retrieve a list of countries from the database. Set active to TRUE to retrieve only active countries
        /// </summary>
        public static DataTable GetCountries(bool active)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            if (CountryActive == true)
            {
                comm.CommandText = "SELECT * FROM Country WHERE CountryActive = @CountryActive ORDER BY CountryName ASC";
            }
            else
            {
                comm.CommandText = "SELECT * FROM Country ORDER BY CountryName ASC";
            }
            // create a new parameter
            comm.Parameters.Add(new SQLiteParameter("@CountryActive", Convert.ToInt16(CountryActive)));
            // execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve the details of a country from the database
        /// </summary>
        public static CountryDetails GetCountryDetails(string CountryID)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "SELECT * FROM Country WHERE CountryID = @CountryID";
            comm.Parameters.Add(new SQLiteParameter("@CountryID", CountryID));
            // execute the stored procedure
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // wrap retrieved data into a CountryDetails object
            CountryDetails details = new CountryDetails();
            if (table.Rows.Count > 0)
            {
                details.CountryName = table.Rows[0]["CountryName"].ToString();
                details.CountryAbbr = table.Rows[0]["CountryAbbr"].ToString();
                details.CountryActive = Convert.ToBoolean(int.Parse(table.Rows[0]["CountryActive"].ToString()));
            }
            // return Country details
            return details;
        }

        /// <summary>
        /// Add a new country
        /// </summary>
        public static bool AddCountry(string countryName, string countryAbbreviation, string active, out string countryId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "AddCountry";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CountryName";
            param.Value = countryName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CountryAbbreviation";
            param.Value = countryAbbreviation;
            param.DbType = DbType.String;
            param.Size = 2;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Active";
            param.Value = active;
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CountryID";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            countryId = comm.Parameters["@CountryID"].Value.ToString();
            // result will be 1 in case of success 
            return (result != -1);
        }

        /// <summary>
        /// Update country details
        /// </summary>
        public static bool UpdateCountry(string countryId, string countryName, string countryAbbreviation, string active)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "UpdateCountry";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CountryId";
            param.Value = countryId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CountryName";
            param.Value = countryName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CountryAbbreviation";
            param.Value = countryAbbreviation;
            param.DbType = DbType.String;
            param.Size = 2;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Active";
            param.Value = active;
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        /// <summary>
        /// Delete country
        /// </summary>
        public static bool DeleteCountry(string id)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "DeleteCountry";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CountryId";
            param.Value = id;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // execute the stored procedure; an error will be thrown by the
            // database in case the country has related states, in which case
            // it is not deleted
            int result = -1;
            try
            {
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success
            return (result != -1);
        }

        /// <summary>
        /// Retrieve a list of states from the database. Set active to TRUE to retrieve only active states
        /// </summary>
        public static DataTable GetStatesInCountry(string countryId, bool active)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetStatesInCountry";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CountryID";
            param.Value = countryId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Active";
            param.Value = active;
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Get state details
        /// </summary>
        public static StateDetails GetStateDetails(string stateId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetStateDetails";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@StateID";
            param.Value = stateId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // Wrap retrieved data into a StateDetails object
            StateDetails details = new StateDetails();
            if (table.Rows.Count > 0)
            {
                details.CountryID = Int32.Parse(table.Rows[0]["CountryID"].ToString());
                details.StateName = table.Rows[0]["StateName"].ToString();
                details.StateAbbreviation = table.Rows[0]["StateAbbreviation"].ToString();
                details.Active = bool.Parse(table.Rows[0]["Active"].ToString());
            }
            // Return state details
            return details;
        }

        /// <summary>
        /// Create a new state
        /// </summary>
        public static bool AddState(string countryId, string stateName, string stateAbbreviation, string active, out string stateId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "AddState";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CountryID";
            param.Value = countryId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@StateName";
            param.Value = stateName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@StateAbbreviation";
            param.Value = stateAbbreviation;
            param.DbType = DbType.String;
            param.Size = 2;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Active";
            param.Value = active;
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@StateID";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            stateId = comm.Parameters["@StateID"].Value.ToString();
            // result will be 1 in case of success 
            return (result != -1);
        }

        /// <summary>
        /// Update state details
        /// </summary>
        public static bool UpdateState(string stateId, string stateName, string stateAbbreviation, string active)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "UpdateState";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@StateId";
            param.Value = stateId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@StateName";
            param.Value = stateName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@StateAbbreviation";
            param.Value = stateAbbreviation;
            param.DbType = DbType.String;
            param.Size = 2;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Active";
            param.Value = active;
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        /// <summary>
        /// Delete State
        /// </summary>
        public static bool DeleteState(string stateId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "DeleteState";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@StateId";
            param.Value = stateId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // execute the stored procedure; an error will be thrown by the
            // database in case the State has related cities, in which case
            // it is not deleted
            int result = -1;
            try
            {
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success
            return (result != -1);
        }

        /// <summary>
        /// Retrieve a list of cities from the database. Set active to TRUE to retrieve only active cities
        /// </summary>
        public static DataTable GetCitiesInState(string stateId, bool active)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetCitiesInState";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@StateID";
            param.Value = stateId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Active";
            param.Value = active;
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Get city details
        /// </summary>
        public static CityDetails GetCityDetails(string cityId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetCityDetails";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // Wrap retrieved data into a CityDetails object
            CityDetails details = new CityDetails();
            if (table.Rows.Count > 0)
            {
                details.StateID = Int32.Parse(table.Rows[0]["StateID"].ToString());
                details.CityName = table.Rows[0]["CityName"].ToString();
                details.ZipCode = table.Rows[0]["ZipCode"].ToString();
                details.TimeZoneID = Int32.Parse(table.Rows[0]["TimeZoneID"].ToString());
                details.Active = bool.Parse(table.Rows[0]["Active"].ToString());
            }
            // Return city details
            return details;
        }

        /// <summary>
        /// Create a new city
        /// </summary>
        public static bool AddCity(string stateId, string cityName, string zipCode, string timeZoneId, string active, out string cityId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "AddCity";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@StateID";
            param.Value = stateId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CityName";
            param.Value = cityName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@ZipCode";
            param.Value = zipCode;
            param.DbType = DbType.String;
            param.Size = 6;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@TimeZoneID";
            param.Value = timeZoneId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Active";
            param.Value = active;
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            cityId = comm.Parameters["@CityID"].Value.ToString();
            // result will be 1 in case of success 
            return (result != -1);
        }

        /// <summary>
        /// Update city details
        /// </summary>
        public static bool UpdateCity(string cityId, string cityName, string zipCode, string timeZoneId, string active)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "UpdateCity";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CityName";
            param.Value = cityName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@ZipCode";
            param.Value = zipCode;
            param.DbType = DbType.String;
            param.Size = 6;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@TimeZoneID";
            param.Value = timeZoneId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Active";
            param.Value = active;
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // Delete City
        public static bool DeleteCity(string cityId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "DeleteCity";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CityId";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // execute the stored procedure; an error will be thrown by the
            // database in case the City has related establishments, in which case
            // it is not deleted
            int result = -1;
            try
            {
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success
            return (result != -1);
        }


        // A D V E R T I S E R   B E G I N - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        /// <summary>
        /// Retrieve the list of advertisers from the database.
        /// </summary>
        public static DataTable GetAdvertisers()
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetAdvertisers";
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve the list of advertisers in a city
        /// </summary>
        public static DataTable GetAdvertisersInCity(string cityId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetAdvertisersInCity";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        // Get advertiser details
        public static AdvertiserDetails GetAdvertiserDetails(string advertiserId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetAdvertiserDetails";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@AdvertiserID";
            param.Value = advertiserId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // Wrap retrieved data into a AdvertiserDetails object
            AdvertiserDetails details = new AdvertiserDetails();
            if (table.Rows.Count > 0)
            {
                details.BlockID = Int32.Parse(table.Rows[0]["BlockID"].ToString());
                details.ImageUrl = table.Rows[0]["ImageUrl"].ToString();
                details.NavigateUrl = table.Rows[0]["NavigateUrl"].ToString();
                details.AlternateText = table.Rows[0]["AlternateText"].ToString();
                details.Keyword = table.Rows[0]["Keyword"].ToString();
                details.Impressions = Int32.Parse(table.Rows[0]["Impressions"].ToString());
            }
            // Return advertiser details
            return details;
        }

        // Add a new advertiser
        public static bool AddAdvertiser(string cityId, string blockId, string imageUrl, string navigateUrl, string alternateText, string keyword, string impressions, out string advertiserId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "AddAdvertiser";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@BlockID";
            param.Value = blockId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@ImageUrl";
            param.Value = imageUrl;
            param.DbType = DbType.String;
            param.Size = 255;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@NavigateUrl";
            param.Value = navigateUrl;
            param.DbType = DbType.String;
            param.Size = 255;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@AlternateText";
            param.Value = alternateText;
            param.DbType = DbType.String;
            param.Size = 100;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Keyword";
            param.Value = keyword;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Impressions";
            param.Value = impressions;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@AdvertiserID";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            advertiserId = comm.Parameters["@AdvertiserID"].Value.ToString();
            // result will be 1 in case of success 
            return (result >= 1);
        }

        // Update an existing advertiser
        public static bool UpdateAdvertiser(string advertiserId, string blockId, string imageUrl, string navigateUrl, string alternateText, string keyword, string impressions)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "UpdateAdvertiser";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@AdvertiserID";
            param.Value = advertiserId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@BlockID";
            param.Value = blockId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@ImageUrl";
            param.Value = imageUrl;
            param.DbType = DbType.String;
            param.Size = 255;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@NavigateUrl";
            param.Value = navigateUrl;
            param.DbType = DbType.String;
            param.Size = 255;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@AlternateText";
            param.Value = alternateText;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Keyword";
            param.Value = keyword;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Impressions";
            param.Value = impressions;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result >= 1);
        }

        // Get cities that contain a specified advertiser
        public static DataTable GetCitiesWithAdvertiser(string advertiserId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetCitiesWithAdvertiser";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@AdvertiserID";
            param.Value = advertiserId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        // Get cities that do not contain a specified advertiser
        public static DataTable GetCitiesWithoutAdvertiser(string advertiserId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetCitiesWithoutAdvertiser";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@AdvertiserID";
            param.Value = advertiserId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        // Assign an advertiser to a new city
        public static bool AssignAdvertiserToCity(string advertiserId, string cityId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "AssignAdvertiserToCity";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@AdvertiserID";
            param.Value = advertiserId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // Move advertiser to a new City
        public static bool MoveAdvertiserToCity(string advertiserId, string oldCityId, string newCityId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "MoveAdvertiserToCity";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@AdvertiserID";
            param.Value = advertiserId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@OldCityID";
            param.Value = oldCityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@NewCityID";
            param.Value = newCityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // Removes a advertiser from a city
        public static bool RemoveAdvertiserFromCity(string advertiserId, string cityId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "RemoveAdvertiserFromCity";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@AdvertiserID";
            param.Value = advertiserId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // Deletes an advertiser from the advertiser catalog
        public static bool DeleteAdvertiser(string advertiserId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "DeleteAdvertiser";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@AdvertiserID";
            param.Value = advertiserId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // Update an existing advertiser's username
        public static bool UpdateAdvertiserUserName(string advertiserId, string userName)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "UpdateAdvertiserUserName";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@AdvertiserID";
            param.Value = advertiserId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@UserName";
            param.Value = userName;
            param.DbType = DbType.String;
            param.Size = 40;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result >= 1);
        }

        // Get advertiser username
        public static EntryUserName GetAdvertiserUserName(string advertiserId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetAdvertiserUserName";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@AdvertiserID";
            param.Value = advertiserId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // Wrap retrieved data into a EntryUserName object
            EntryUserName advertiserUserName = new EntryUserName();
            if (table.Rows.Count > 0)
            {
                advertiserUserName.userName = table.Rows[0]["UserName"].ToString();
            }
            // Return advertiser user name
            return advertiserUserName;
        }


        // A D V E R T I S E R   E N D - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        // E S T A B L I S H M E N T   B E G I N - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        /// <summary>
        /// Retrieve the list of establishments from the database.
        /// </summary>
        public static DataTable GetEstablishments()
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetEstablishments";
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve the list of establishments in a city
        /// </summary>
        public static DataTable GetEstablishmentsInCity(string cityId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetEstablishmentsInCity";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        // Get establishment details
        public static EstablishmentDetails GetEstablishmentDetails(string establishmentId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "GetEstablishmentDetails";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentID";
            param.Value = establishmentId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // execute the stored procedure
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // wrap retrieved data into a EstablishmentDetails object
            EstablishmentDetails details = new EstablishmentDetails();
            if (table.Rows.Count > 0)
            {
                // get the first table row
                DataRow dr = table.Rows[0];
                // get establishment details
                details.EstablishmentName = dr["EstablishmentName"].ToString();
                details.Priority = int.Parse(dr["Priority"].ToString());
                details.Tier = int.Parse(dr["Tier"].ToString());
                details.Logo = dr["Logo"].ToString();
                details.Motto = dr["Motto"].ToString();
                details.Cuisine = dr["Cuisine"].ToString();
                details.StreetAddress = dr["StreetAddress"].ToString();
                details.CityName = dr["CityName"].ToString();
                details.StateAbbreviation = dr["StateAbbreviation"].ToString();
                details.ZipCode = dr["ZipCode"].ToString();
                details.Latitude = float.Parse(dr["Latitude"].ToString());
                details.Longitude = float.Parse(dr["Longitude"].ToString());
                details.TelephoneNo = dr["TelephoneNo"].ToString();
                details.FaxNo = dr["FaxNo"].ToString();
                details.Email = dr["Email"].ToString();
                details.WebSite = dr["WebSite"].ToString();
                details.Facebook = dr["Facebook"].ToString();
                details.AboutUs = dr["AboutUs"].ToString();
                details.Photo01 = dr["Photo01"].ToString();
                details.Photo02 = dr["Photo02"].ToString();
                details.Photo03 = dr["Photo03"].ToString();
                details.Photo04 = dr["Photo04"].ToString();
                details.Photo05 = dr["Photo05"].ToString();
            }
            // return establishment details
            return details;
        }

        // Add a new establishment
        public static bool AddEstablishment(string cityId, string establishmentName, string priority, string tier, string logo, string motto, string cuisine, string streetAddress, string cityName, string stateAbbreviation, string zipCode, string latitude, string longitude, string telephoneNo, string faxNo, string email, string webSite, string facebook, string aboutUs, string photo01, string photo02, string photo03, string photo04, string photo05, out string establishmentId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "AddEstablishment";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentName";
            param.Value = establishmentName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Priority";
            param.Value = priority;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Tier";
            param.Value = tier;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Logo";
            param.Value = logo;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Motto";
            param.Value = motto;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Cuisine";
            param.Value = cuisine;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@StreetAddress";
            param.Value = streetAddress;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CityName";
            param.Value = cityName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@StateAbbreviation";
            param.Value = stateAbbreviation;
            param.DbType = DbType.String;
            param.Size = 2;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@ZipCode";
            param.Value = zipCode;
            param.DbType = DbType.String;
            param.Size = 6;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Latitude";
            param.Value = latitude;
            param.DbType = DbType.Double;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Longitude";
            param.Value = longitude;
            param.DbType = DbType.Double;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@TelephoneNo";
            param.Value = telephoneNo;
            param.DbType = DbType.String;
            param.Size = 14;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@FaxNo";
            param.Value = faxNo;
            param.DbType = DbType.String;
            param.Size = 14;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Email";
            param.Value = email;
            param.DbType = DbType.String;
            param.Size = 100;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@WebSite";
            param.Value = webSite;
            param.DbType = DbType.String;
            param.Size = 100;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Facebook";
            param.Value = facebook;
            param.DbType = DbType.String;
            param.Size = 100;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@AboutUs";
            param.Value = aboutUs;
            param.DbType = DbType.String;
            param.Size = 1000;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Photo01";
            param.Value = photo01;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Photo02";
            param.Value = photo02;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Photo03";
            param.Value = photo03;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Photo04";
            param.Value = photo04;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Photo05";
            param.Value = photo05;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentID";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            establishmentId = comm.Parameters["@EstablishmentID"].Value.ToString();
            // result will be 1 in case of success 
            return (result >= 1);
        }

        // Update an existing establishment
        public static bool UpdateEstablishment(string establishmentId, string establishmentName, string priority, string tier, string logo, string motto, string cuisine, string streetAddress, string cityName, string stateAbbreviation, string zipCode, string latitude, string longitude, string telephoneNo, string faxNo, string email, string webSite, string facebook, string aboutUs, string photo01, string photo02, string photo03, string photo04, string photo05)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "UpdateEstablishment";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentID";
            param.Value = establishmentId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentName";
            param.Value = establishmentName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Priority";
            param.Value = priority;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Tier";
            param.Value = tier;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Logo";
            param.Value = logo;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Motto";
            param.Value = motto;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Cuisine";
            param.Value = cuisine;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@StreetAddress";
            param.Value = streetAddress;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CityName";
            param.Value = cityName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@StateAbbreviation";
            param.Value = stateAbbreviation;
            param.DbType = DbType.String;
            param.Size = 2;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@ZipCode";
            param.Value = zipCode;
            param.DbType = DbType.String;
            param.Size = 6;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Latitude";
            param.Value = latitude;
            param.DbType = DbType.Double;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Longitude";
            param.Value = longitude;
            param.DbType = DbType.Double;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@TelephoneNo";
            param.Value = telephoneNo;
            param.DbType = DbType.String;
            param.Size = 14;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@FaxNo";
            param.Value = faxNo;
            param.DbType = DbType.String;
            param.Size = 14;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Email";
            param.Value = email;
            param.DbType = DbType.String;
            param.Size = 100;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@WebSite";
            param.Value = webSite;
            param.DbType = DbType.String;
            param.Size = 100;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Facebook";
            param.Value = facebook;
            param.DbType = DbType.String;
            param.Size = 100;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@AboutUs";
            param.Value = aboutUs;
            param.DbType = DbType.String;
            param.Size = 1000;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Photo01";
            param.Value = photo01;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Photo02";
            param.Value = photo02;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Photo03";
            param.Value = photo03;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Photo04";
            param.Value = photo04;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Photo05";
            param.Value = photo05;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result >= 1);
        }

        // Get cities that contain a specified establishment
        public static DataTable GetCitiesWithEstablishment(string establishmentId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetCitiesWithEstablishment";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentID";
            param.Value = establishmentId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        // Get cities that do not contain a specified establishment
        public static DataTable GetCitiesWithoutEstablishment(string establishmentId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetCitiesWithoutEstablishment";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentID";
            param.Value = establishmentId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        // Assign an establishment to a new city
        public static bool AssignEstablishmentToCity(string establishmentId, string cityId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "AssignEstablishmentToCity";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentID";
            param.Value = establishmentId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // Move establishment to a new City
        public static bool MoveEstablishmentToCity(string establishmentId, string oldCityId, string newCityId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "MoveEstablishmentToCity";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentID";
            param.Value = establishmentId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@OldCityID";
            param.Value = oldCityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@NewCityID";
            param.Value = newCityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // Removes a establishment from a city
        public static bool RemoveEstablishmentFromCity(string establishmentId, string cityId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "RemoveEstablishmentFromCity";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentID";
            param.Value = establishmentId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // Deletes an establishment from the establishment catalog
        public static bool DeleteEstablishment(string establishmentId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "DeleteEstablishment";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentID";
            param.Value = establishmentId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // Update an existing establishment's username
        public static bool UpdateEstablishmentUserName(string establishmentId, string userName)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "UpdateEstablishmentUserName";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentID";
            param.Value = establishmentId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@UserName";
            param.Value = userName;
            param.DbType = DbType.String;
            param.Size = 40;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result >= 1);
        }

        // Get establishment username
        public static EntryUserName GetEstablishmentUserName(string establishmentId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetEstablishmentUserName";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentID";
            param.Value = establishmentId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // Wrap retrieved data into a EntryUserName object
            EntryUserName establishmentUserName = new EntryUserName();
            if (table.Rows.Count > 0)
            {
                establishmentUserName.userName = table.Rows[0]["UserName"].ToString();
            }
            // Return establishment user name
            return establishmentUserName;
        }


        // E S T A B L I S H M E N T   E N D - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        // E V E N T   B E G I N - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        /// <summary>
        /// Retrieve the list of events from the database.
        /// </summary>
        public static DataTable GetEvents()
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetEvents";
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve the list of events in a city. Set all to FALSE to retrieve only events in the next 30 days
        /// </summary>
        public static DataTable GetEventsInCity(string cityId, bool all)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetEventsInCity";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@All";
            param.Value = all;
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            return table;
        }

        // Get event details
        public static EventDetails GetEventDetails(string eventId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetEventDetails";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EventID";
            param.Value = eventId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // Wrap retrieved data into a EventDetails object
            EventDetails details = new EventDetails();
            if (table.Rows.Count > 0)
            {
                details.EventName = table.Rows[0]["EventName"].ToString();
                details.Date = DateTime.Parse(table.Rows[0]["Date"].ToString());
                details.EndDate = DateTime.Parse(table.Rows[0]["EndDate"].ToString());
                details.Description = table.Rows[0]["Description"].ToString();
                details.ImageUrl = table.Rows[0]["ImageUrl"].ToString();
                details.NavigateUrl = table.Rows[0]["NavigateUrl"].ToString();
            }
            // Return event details
            return details;
        }

        // Add a new event
        public static bool AddEvent(string cityId, string eventName, string date, string endDate, string description, string imageUrl, string navigateUrl, out string eventId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "AddEvent";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@EventName";
            param.Value = eventName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Date";
            param.Value = date;
            param.DbType = DbType.DateTime;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@EndDate";
            param.Value = endDate;
            param.DbType = DbType.DateTime;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Description";
            param.Value = description;
            param.DbType = DbType.String;
            param.Size = 300;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@ImageUrl";
            param.Value = imageUrl;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@NavigateUrl";
            param.Value = navigateUrl;
            param.DbType = DbType.String;
            param.Size = 100;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@EventID";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            eventId = comm.Parameters["@EventID"].Value.ToString();
            // result will be 1 in case of success 
            return (result >= 1);
        }

        // Update an existing event
        public static bool UpdateEvent(string eventId, string eventName, string date, string endDate, string description, string imageUrl, string navigateUrl)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "UpdateEvent";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EventID";
            param.Value = eventId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@EventName";
            param.Value = eventName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Date";
            param.Value = date;
            param.DbType = DbType.DateTime;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@EndDate";
            param.Value = endDate;
            param.DbType = DbType.DateTime;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Description";
            param.Value = description;
            param.DbType = DbType.String;
            param.Size = 300;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@ImageUrl";
            param.Value = imageUrl;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@NavigateUrl";
            param.Value = navigateUrl;
            param.DbType = DbType.String;
            param.Size = 100;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result >= 1);
        }

        // Get cities that contain a specified event
        public static DataTable GetCitiesWithEvent(string eventId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetCitiesWithEvent";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EventID";
            param.Value = eventId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        // Get cities that do not contain a specified event
        public static DataTable GetCitiesWithoutEvent(string eventId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetCitiesWithoutEvent";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EventID";
            param.Value = eventId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        // Assign an event to a new city
        public static bool AssignEventToCity(string eventId, string cityId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "AssignEventToCity";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EventID";
            param.Value = eventId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // Move event to a new City
        public static bool MoveEventToCity(string eventId, string oldCityId, string newCityId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "MoveEventToCity";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EventID";
            param.Value = eventId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@OldCityID";
            param.Value = oldCityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@NewCityID";
            param.Value = newCityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // Removes a event from a city
        public static bool RemoveEventFromCity(string eventId, string cityId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "RemoveEventFromCity";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EventID";
            param.Value = eventId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // Deletes an event from the event catalog
        public static bool DeleteEvent(string eventId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "DeleteEvent";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EventID";
            param.Value = eventId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }


        // E V E N T   E N D - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        // L I N K - B E G I N - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        /// <summary>
        /// Retrieve the list of links from the database.
        /// </summary>
        public static DataTable GetLinks()
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetLinks";
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve the list of links in a city
        /// </summary>
        public static DataTable GetLinksInCity(string cityId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetLinksInCity";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        // Get link details
        public static LinkDetails GetLinkDetails(string linkId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetLinkDetails";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@LinkID";
            param.Value = linkId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // Wrap retrieved data into a LinkDetails object
            LinkDetails details = new LinkDetails();
            if (table.Rows.Count > 0)
            {
                details.LinkName = table.Rows[0]["LinkName"].ToString();
                details.LinkUrl = table.Rows[0]["LinkUrl"].ToString();
                details.LinkTitle = table.Rows[0]["LinkTitle"].ToString();
                details.LinkCategoryName = table.Rows[0]["LinkCategoryName"].ToString();
                details.LinkCategoryRank = table.Rows[0]["LinkCategoryRank"].ToString();
            }
            // Return link details
            return details;
        }

        // Add a new link
        public static bool AddLink(string cityId, string linkName, string linkUrl, string linkTitle, string linkCategoryName, string linkCategoryRank, out string linkId)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "AddLink";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@LinkName";
            param.Value = linkName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@LinkUrl";
            param.Value = linkUrl;
            param.DbType = DbType.String;
            param.Size = 100;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@LinkTitle";
            param.Value = linkTitle;
            param.DbType = DbType.String;
            param.Size = 100;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@LinkCategoryName";
            param.Value = linkCategoryName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@LinkCategoryRank";
            param.Value = linkCategoryRank;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@LinkID";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            linkId = comm.Parameters["@LinkID"].Value.ToString();
            // result will be 1 in case of success 
            return (result >= 1);
        }

        // Update an existing link
        public static bool UpdateLink(string linkId, string linkName, string linkUrl, string linkTitle, string linkCategoryName, string linkCategoryRank)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "UpdateLink";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@LinkID";
            param.Value = linkId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@LinkName";
            param.Value = linkName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@LinkUrl";
            param.Value = linkUrl;
            param.DbType = DbType.String;
            param.Size = 100;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@LinkTitle";
            param.Value = linkTitle;
            param.DbType = DbType.String;
            param.Size = 100;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@LinkCategoryName";
            param.Value = linkCategoryName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@LinkCategoryRank";
            param.Value = linkCategoryRank;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result >= 1);
        }

        // Get cities that contain a specified link
        public static DataTable GetCitiesWithLink(string linkId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetCitiesWithLink";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@LinkID";
            param.Value = linkId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        // Get cities that do not contain a specified link
        public static DataTable GetCitiesWithoutLink(string linkId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetCitiesWithoutLink";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@LinkID";
            param.Value = linkId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        // Assign an link to a new city
        public static bool AssignLinkToCity(string linkId, string cityId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "AssignLinkToCity";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@LinkID";
            param.Value = linkId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // Move link to a new City
        public static bool MoveLinkToCity(string linkId, string oldCityId, string newCityId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "MoveLinkToCity";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@LinkID";
            param.Value = linkId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@OldCityID";
            param.Value = oldCityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@NewCityID";
            param.Value = newCityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // Removes a link from a city
        public static bool RemoveLinkFromCity(string linkId, string cityId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "RemoveLinkFromCity";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@LinkID";
            param.Value = linkId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        // Deletes an link from the link catalog
        public static bool DeleteLink(string linkId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "DeleteLink";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@LinkID";
            param.Value = linkId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }


        // L I N K - E N D - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        /// <summary>
        /// Retrieve the routine schedule from the database.
        /// </summary>
        public static DataTable GetRoutineEstablishmentSchedule(int establishmentId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetRoutineEstablishmentSchedule";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentID";
            param.Value = establishmentId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        // Add the routine schedule for the establishment
        public static bool AddRoutineEstablishmentSchedule(string establishmentId, string establishmentName, string weekday, string hoursOfOperation, string happyHourTimes, string happyHourSpecials, string foodAndDrinkSpecials, string specialEvents, out string establishmentRoutineEventID)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "AddRoutineEstablishmentSchedule";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentID";
            param.Value = establishmentId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentName";
            param.Value = establishmentName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Weekday";
            param.Value = weekday;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@HoursOfOperation";
            param.Value = hoursOfOperation;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@HappyHourTimes";
            param.Value = happyHourTimes;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@HappyHourSpecials";
            param.Value = happyHourSpecials;
            param.DbType = DbType.String;
            param.Size = 300;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@FoodAndDrinkSpecials";
            param.Value = foodAndDrinkSpecials;
            param.DbType = DbType.String;
            param.Size = 300;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@SpecialEvents";
            param.Value = specialEvents;
            param.DbType = DbType.String;
            param.Size = 300;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentRoutineEventID";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            establishmentRoutineEventID = comm.Parameters["@EstablishmentRoutineEventID"].Value.ToString();
            // result will be 1 in case of success 
            return (result >= 1);
        }

        // Update the routine schedule for the establishment
        public static bool UpdateRoutineEstablishmentSchedule(string establishmentRoutineEventID, string weekday, string hoursOfOperation, string happyHourTimes, string happyHourSpecials, string foodAndDrinkSpecials, string specialEvents)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "UpdateRoutineEstablishmentSchedule";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentRoutineEventID";
            param.Value = establishmentRoutineEventID;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Weekday";
            param.Value = weekday;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@HoursOfOperation";
            param.Value = hoursOfOperation;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@HappyHourTimes";
            param.Value = happyHourTimes;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@HappyHourSpecials";
            param.Value = happyHourSpecials;
            param.DbType = DbType.String;
            param.Size = 300;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@FoodAndDrinkSpecials";
            param.Value = foodAndDrinkSpecials;
            param.DbType = DbType.String;
            param.Size = 300;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@SpecialEvents";
            param.Value = specialEvents;
            param.DbType = DbType.String;
            param.Size = 300;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result >= 1);
        }

        // Delete an entry in the special schedule for the establishment
        public static bool DeleteRoutineEstablishmentSchedule(string establishmentRoutineEventID)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "DeleteRoutineEstablishmentSchedule";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentRoutineEventID";
            param.Value = establishmentRoutineEventID;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        /// <summary>
        /// Retrieve the special schedule from the database.
        /// </summary>
        public static DataTable GetSpecialEstablishmentSchedule(int establishmentId)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetSpecialEstablishmentSchedule";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentID";
            param.Value = establishmentId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        /// <summary>
        /// Retrieve the special schedule from the database.
        /// </summary>
        public static DataTable Get30DaySpecialEstablishmentSchedule(int establishmentId, DateTime date)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "Get30DaySpecialEstablishmentSchedule";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentID";
            param.Value = establishmentId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Date";
            param.Value = date;
            param.DbType = DbType.DateTime;
            comm.Parameters.Add(param);
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

        // Add an entry to the special schedule for the establishment
        public static bool AddSpecialEstablishmentSchedule(string establishmentId, string establishmentName, string date, string description, string todaysHoursOfOperation, string todaysHappyHourTimes, string todaysHappyHourSpecials, string thsAppendOrReplaceFlag, string todaysFoodAndDrinkSpecials, string tfsAppendOrReplaceFlag, string todaysSpecialEvents, string tseAppendOrReplaceFlag, out string establishmentSpecialEventID)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "AddSpecialEstablishmentSchedule";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentID";
            param.Value = establishmentId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentName";
            param.Value = establishmentName;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Date";
            param.Value = date;
            param.DbType = DbType.DateTime;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Description";
            param.Value = description;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@TodaysHoursOfOperation";
            param.Value = todaysHoursOfOperation;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@TodaysHappyHourTimes";
            param.Value = todaysHappyHourTimes;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@TodaysHappyHourSpecials";
            param.Value = todaysHappyHourSpecials;
            param.DbType = DbType.String;
            param.Size = 300;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@THSAppendOrReplaceFlag";
            param.Value = thsAppendOrReplaceFlag;
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@TodaysFoodAndDrinkSpecials";
            param.Value = todaysFoodAndDrinkSpecials;
            param.DbType = DbType.String;
            param.Size = 300;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@TFSAppendOrReplaceFlag";
            param.Value = tfsAppendOrReplaceFlag;
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@TodaysSpecialEvents";
            param.Value = todaysSpecialEvents;
            param.DbType = DbType.String;
            param.Size = 300;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@TSEAppendOrReplaceFlag";
            param.Value = tseAppendOrReplaceFlag;
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentSpecialEventID";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            establishmentSpecialEventID = comm.Parameters["@EstablishmentSpecialEventID"].Value.ToString();
            // result will be 1 in case of success 
            return (result >= 1);
        }

        // Update the special schedule for the establishment
        public static bool UpdateSpecialEstablishmentSchedule(string establishmentSpecialEventID, string description, string todaysHoursOfOperation, string todaysHappyHourTimes, string todaysHappyHourSpecials, string thsAppendOrReplaceFlag, string todaysFoodAndDrinkSpecials, string tfsAppendOrReplaceFlag, string todaysSpecialEvents, string tseAppendOrReplaceFlag)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "UpdateSpecialEstablishmentSchedule";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentSpecialEventID";
            param.Value = establishmentSpecialEventID;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@Description";
            param.Value = description;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@TodaysHoursOfOperation";
            param.Value = todaysHoursOfOperation;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@TodaysHappyHourTimes";
            param.Value = todaysHappyHourTimes;
            param.DbType = DbType.String;
            param.Size = 50;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@TodaysHappyHourSpecials";
            param.Value = todaysHappyHourSpecials;
            param.DbType = DbType.String;
            param.Size = 300;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@THSAppendOrReplaceFlag";
            param.Value = thsAppendOrReplaceFlag;
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@TodaysFoodAndDrinkSpecials";
            param.Value = todaysFoodAndDrinkSpecials;
            param.DbType = DbType.String;
            param.Size = 300;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@TFSAppendOrReplaceFlag";
            param.Value = tfsAppendOrReplaceFlag;
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@TodaysSpecialEvents";
            param.Value = todaysSpecialEvents;
            param.DbType = DbType.String;
            param.Size = 300;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@TSEAppendOrReplaceFlag";
            param.Value = tseAppendOrReplaceFlag;
            param.DbType = DbType.Boolean;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result >= 1);
        }

        // Delete an entry in the special schedule for the establishment
        public static bool DeleteSpecialEstablishmentSchedule(string establishmentSpecialEventID)
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "DeleteSpecialEstablishmentSchedule";
            // Create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentSpecialEventID";
            param.Value = establishmentSpecialEventID;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // result will represent the number of changed rows
            int result = -1;
            try
            {
                // execute the stored procedure
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch
            {
                // any errors are logged in GenericDataAccess, we ignore them here
            }
            // result will be 1 in case of success 
            return (result != -1);
        }

        /// <summary>
        /// Retrieve today's schedule from the database
        /// </summary>
        public static DataTable GetTodaysScheduleForCity(int cityId, string pageNumber, out int howManyPages)
        {
            // get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // set the stored procedure name
            comm.CommandText = "GetTodaysScheduleForCity";
            // create a new parameter
            DbParameter param = comm.CreateParameter();
            param.ParameterName = "@CityID";
            param.Value = cityId;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@PageNumber";
            param.Value = pageNumber;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@EstablishmentsPerPage";
            param.Value = theusualjointsConfiguration.EstablishmentsPerPage;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // create a new parameter
            param = comm.CreateParameter();
            param.ParameterName = "@HowManyEstablishments";
            param.Direction = ParameterDirection.Output;
            param.DbType = DbType.Int32;
            comm.Parameters.Add(param);
            // execute the stored procedure and save the results in a DataTable
            DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
            // calculate how many pages of establishments and set the out parameter
            int howManyEstablishments = Int32.Parse(comm.Parameters["@HowManyEstablishments"].Value.ToString());
            howManyPages = (int)Math.Ceiling((double)howManyEstablishments / (double)theusualjointsConfiguration.EstablishmentsPerPage);
            // return the page of establishments
            return table;
        }

        /// <summary>
        /// Retrieve a list of today's special events from the database.
        /// </summary>
        public static DataTable GetTodaysSpecialEvents()
        {
            // Get a configured DbCommand object
            DbCommand comm = GenericDataAccess.CreateCommand();
            // Set the stored procedure name
            comm.CommandText = "GetTodaysSpecialEvents";
            // Execute the stored procedure and return the results
            return GenericDataAccess.ExecuteSelectCommand(comm);
        }

    }
}