using System.Threading.Tasks;

namespace JollyWrapper
{
    public static partial class Database
    {
        /// <summary>
        /// Executes a MySQL stored procedure that could return multiple rows with multiple columns.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns>QueryData Object</returns>
        /// <example>
        public static async Task<QueryData> ExecuteProcedure(string storedProcedureName, QueryParms parameters = null)
        {
            QueryData rows = new QueryData();
            MySqlConnection connection = await OpenConnection();

            try
            {
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProcedureName;

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.AddWithValue(parameter.Key, (object)parameter.Value ?? DBNull.Value);
                        }
                    }

                    await cmd.PrepareAsync();

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
    }
}
