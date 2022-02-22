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
        public static async Task<QueryData> ExecuteProcedure(string query, QueryParms parameters = null)
        {
            return await ExecuteQuery(query, parameters);
        }
    }
}
