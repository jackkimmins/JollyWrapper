using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;

namespace JollyWrapper
{
    public static partial class Database
    {
        /// <summary>
        /// Initialises a new database connection asynchronously.
        /// </summary>
        /// <returns>Returns a task representing the MySqlConnection object.</returns>
        private static async Task<MySqlConnection> OpenConnection()
        {
            if (ConnectionString == null)
                throw new Exception("Database Connection String is Null");

            MySqlConnection connection = null;

            try
            {
                connection = new MySqlConnection(ConnectionString);
                await connection.OpenAsync();
            }
            catch (Exception ex)
            {
                OutputError(ex.Message);
            }

            return connection;
        }

        /// <summary>
        /// Terminates the provided MySqlConnection asynchronously.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        private static async Task CloseConnection(MySqlConnection connection)
        {
            try
            {
                await connection.CloseAsync();
            }
            catch (Exception ex)
            {
                OutputError(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new MySqlCommand connection object with the provided query and parameters.
        /// //Example of query: 'SELECT * FROM table WHERE column = @value'
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns a task representing a MySqlCommand.</returns>
        private static Task<MySqlCommand> CreateCommand(MySqlConnection connection, string query, Dictionary<string, string> parameters = null)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue(parameter.Key, (object)parameter.Value ?? DBNull.Value);
                }

                cmd.PrepareAsync();
            }

            return Task.FromResult(cmd);
        }

        /// <summary>
        /// Converts a correctly formatted query and corresponding parameters into a dictionary of corresponding key and value pairs.
        /// Example: query = SELECT * FROM `users` WHERE `id` = @val AND `name` = @val | parameters = 12, "Jack"
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns the dictionary of mapped query parameters to their values.</returns>
        private static (string, QueryParms) ParmsToQuery(string query, params object[] parameters)
        {
            ushort paramCount = 0;
            query = System.Text.RegularExpressions.Regex.Replace(query, System.Text.RegularExpressions.Regex.Escape("@val"), (m) => $"@val{paramCount++}");

            if (parameters.Length != paramCount)
                throw new ArgumentException("Number of parameters does not match number of '@val' placeholders in query.");

            var parms = new QueryParms();

            for (int i = 0; i < paramCount; i++)
                parms.Add($"@val{i}", parameters[i].ToString());

            return (query, parms);
        }

    }
}