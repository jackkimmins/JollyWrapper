using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace JollyWrapper
{
    public static partial class Database
    {
        /// <summary>
        /// Executes a MySQL query that doesn't return any values, such as an INSERT etc.
        /// + With Dictionary Parameters
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns the number of affected rows.</returns>
        public static async Task<int> ExecuteNonQuery(string query, QueryParms parameters = null)
        {
            int rowsAffected = 0;

            try
            {
                MySqlConnection connection = await OpenConnection();

                using (MySqlCommand cmd = await CreateCommand(connection, query, parameters))
                {
                    rowsAffected = await cmd.ExecuteNonQueryAsync();
                }

                await CloseConnection(connection);
            }
            catch (Exception ex)
            {
                OutputError(ex.Message);
            }

            return rowsAffected;
        }



        /// <summary>
        /// Executes a MySQL query that doesn't return any values, such as an INSERT etc.
        /// - Without Dictionary Parameters
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns the number of affected rows.</returns>
        public static async Task<int> ExecuteNonQuery(string query, params object[] parameters)
        {
            var data = ParmsToQuery(query, parameters);
            return await ExecuteNonQuery(data.Item1, data.Item2);
        }
    }
}
