using System;
using System.Data;
using System.Data.Common;

namespace TheUsualJoints.App_Start
{
    /// <summary>
    /// Class contains generic data access functionality to be accessed from
    /// the business tier
    /// </summary>
    public class GenericDataAccess
    {
        // static constructor
        static GenericDataAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        // executes a command and returns the results as a DataTable object
        public static DataTable ExecuteSelectCommand(DbCommand command)
        {
            // The DataTable to be returned
            DataTable table;
            // Execute the command, making sure the connection gets closed in the
            // end
            try
            {
                // Open the data connection
                command.Connection.Open();
                // Execute the command and save the results in a DataTable
                DbDataReader reader = command.ExecuteReader();
                table = new DataTable();
                table.Load(reader);
                // Close the reader
                reader.Close();
            }
            catch (Exception ex)
            {
                Utilities.LogError(ex);
                throw;
            }
            finally
            {
                // Close the connection
                command.Connection.Close();
            }
            return table;
        }

        // creates and prepares a new DbCommand object on a new connection
        public static DbCommand CreateCommand()
        {
            // Obtain the database provider name
            string dataProviderName = TheUsualJointsConfiguration.DbProviderName;
            // Obtain the database connection string
            string connectionString = TheUsualJointsConfiguration.DbConnectionString;
            // Create a new data provider factory
            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProviderName);
            // Obtain a database-specific connection object
            DbConnection conn = factory.CreateConnection();
            // Set the connection string
            conn.ConnectionString = connectionString;
            // Create a database-specific command object
            DbCommand comm = conn.CreateCommand();
            // Set the command type to stored procedure
            comm.CommandType = CommandType.Text;
            // Return the initialized command object
            return comm;
        }

        // execute an update, delete, or insert command 
        // and return the number of affected rows
        public static int ExecuteNonQuery(DbCommand command)
        {
            // The number of affected rows 
            int affectedRows = -1;
            // Execute the command making sure the connection gets closed in the end
            try
            {
                // Open the connection of the command
                command.Connection.Open();
                // Execute the command and get the number of affected rows
                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Log eventual errors and rethrow them
                Utilities.LogError(ex);
                throw;
            }
            finally
            {
                // Close the connection
                command.Connection.Close();
            }
            // return the number of affected rows
            return affectedRows;
        }

        // execute an update, delete, or insert command 
        // and the number of affected rows and the last row ID
        public static int ExecuteNonQuery(DbCommand command, out int rowID)
        {
            // The last id 
            int affectedRows = -1;
            // Execute the command making sure the connection gets closed in the end
            try
            {
                // Open the connection of the command
                command.Connection.Open();
                // Execute the command and get the number of affected rows
                affectedRows = command.ExecuteNonQuery();
                // Execute the command and the ID of the last row inserted
                command.CommandText = @"SELECT last_insert_rowid()";
                command.ExecuteNonQuery();
                rowID = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                // Log eventual errors and rethrow them
                Utilities.LogError(ex);
                throw;
            }
            finally
            {
                // Close the connection
                command.Connection.Close();
            }
            // return the number of affected rows
            return affectedRows;
        }

        // execute a select command and return a single result as a string
        public static string ExecuteScalar(DbCommand command)
        {
            // The value to be returned 
            string value = "";
            // Execute the command making sure the connection gets closed in the end
            try
            {
                // Open the connection of the command
                command.Connection.Open();
                // Execute the command and get the number of affected rows
                value = command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                // Log eventual errors and rethrow them
                Utilities.LogError(ex);
                throw;
            }
            finally
            {
                // Close the connection
                command.Connection.Close();
            }
            // return the result
            return value;
        }
    }
}