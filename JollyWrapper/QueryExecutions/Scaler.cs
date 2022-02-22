using System;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace JollyWrapper
{
    public static partial class Database
    {
        /// <summary>
        /// Executes a MySQL query that only returns a single value.
        /// + With Dictionary Parameters
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns single value as string.</returns>
        public static async Task<string> ExecuteScalerQuery(string query, QueryParms parameters = null)
        {
            string scaler = null;
            MySqlConnection connection = await OpenConnection();

            try
            {
                using (MySqlCommand cmd = await CreateCommand(connection, query, parameters))
                {
                    scaler = Convert.ToString(await cmd.ExecuteScalarAsync());
                }
            }
            catch (Exception ex)
            {
                OutputError(ex.Message);
            }
            finally
            {
                await CloseConnection(connection);
            }

            return scaler;
        }



        /// <summary>
        /// Executes a MySQL query that only returns a single value.
        /// - Without Dictionary Parameters
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns single value as string.</returns>
        public static async Task<string> ExecuteScalerQuery(string query, params object[] parameters)
        {
            var data = ParmsToQuery(query, parameters);
            return await ExecuteScalerQuery(data.Item1, data.Item2);
        }
    }
}
