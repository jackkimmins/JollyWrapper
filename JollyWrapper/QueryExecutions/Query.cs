using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace JollyWrapper
{
    public static partial class Database
    {
        /// <summary>
        /// Executes a MySQL query that could return multiple rows with multiple columns.
        /// + With Dictionary Parameters
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns a list of dictionaries that corresponds to each row with a given a associative name which is mapped to the column name.</returns>
        public static async Task<QueryData> ExecuteQuery(string query, QueryParms parameters = null)
        {
            QueryData rows = new QueryData();
            MySqlConnection connection = await OpenConnection();

            try
            {
                using (MySqlCommand cmd = await CreateCommand(connection, query, parameters))
                {
                    using (System.Data.Common.DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Dictionary<string, string> row = new Dictionary<string, string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row.Add(reader.GetName(i), reader.GetString(i));
                            }
                            rows.Add(row);
                        }

                        reader.Close();
                    }
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

            return rows;
        }



        /// <summary>
        /// Executes a MySQL query that could return multiple rows with multiple columns.
        /// - Without Dictionary Parameters
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns a list of dictionaries that corresponds to each row with a given a associative name which is mapped to the column name.</returns>
        public static async Task<QueryData> ExecuteQuery(string query, params object[] parameters)
        {
            var data = ParmsToQuery(query, parameters);
            return await ExecuteQuery(data.Item1, data.Item2);
        }
    }
}
