using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace JollyWrapper
{
    public static partial class Database
    {
        public static string ConnectionString { get; set; } = null;

        /// <summary>
        /// Initialises a database connection string using the provided parameters. Custom connection string parameters can also be provided.
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="database"></param>
        /// <param name="args"></param>
        /// <returns>Returns a Boolean if the connection string was successfully created and populated to the connection string variable.</returns>
        public static bool Init(string hostname = "localhost", string username = "root", string password = "", string database = "", params string[] args)
        {
            string connectionString = $"server={hostname};uid={username};pwd={password};database={database}";

            //Join the custom args to the connection string.
            if (args != null)
                connectionString += ";" + string.Join(";", args);
            ConnectionString = connectionString;

            return true;
        }

        /// <summary>
        /// Readable name of connected database.
        /// </summary>
        /// <returns>Database name from connection string</returns>
        public static string ReadableName()
        {
            if (!String.IsNullOrEmpty(ConnectionString))
            {
                //Get database name from connection string
                string[] connectionStringParts = ConnectionString.Split(';');
                foreach (string part in connectionStringParts)
                {
                    if (part.StartsWith("database="))
                    {
                        return part.Replace("database=", "");
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Checks the database connection to see if it is available.
        /// </summary>
        /// <returns>Boolean if the database connection is available.</returns>
        public static async Task<bool> CheckConnection()
        {
            if (ConnectionString == null)
                return false;

            bool isSuccessful = false;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    isSuccessful = connection.Ping();
                    await connection.CloseAsync();
                }

                return isSuccessful;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
