using System.Threading.Tasks;

namespace JollyWrapper
{
    public static partial class Database
    {
        public static int ExecuteNonQuerySync(string query, QueryParms parameters = null)
        {
            Task<int> task = Task.Run<int>(async () => await ExecuteNonQuery(query, parameters));
            return task.Result;
        }

        public static int ExecuteNonQuerySync(string query, params object[] parameters)
        {
            Task<int> task = Task.Run<int>(async () => await ExecuteNonQuery(query, parameters));
            return task.Result;
        }

        public static string ExecuteScalerQuerySync(string query, QueryParms parameters = null)
        {
            Task<string> task = Task.Run<string>(async () => await ExecuteScalerQuery(query, parameters));
            return task.Result;
        }

        public static string ExecuteScalerQuerySync(string query, params object[] parameters)
        {
            Task<string> task = Task.Run<string>(async () => await ExecuteScalerQuery(query, parameters));
            return task.Result;
        }

        public static QueryData ExecuteQuerySync(string query, QueryParms parameters = null)
        {
            Task<QueryData> task = Task.Run<QueryData>(async () => await ExecuteQuery(query, parameters));
            return task.Result;
        }

        public static QueryData ExecuteQuerySync(string query, params object[] parameters)
        {
            Task<QueryData> task = Task.Run<QueryData>(async () => await ExecuteQuery(query, parameters));
            return task.Result;
        }

        public static QueryData ExecuteProcedureSync(string query, QueryParms parameters = null)
        {
            Task<QueryData> task = Task.Run<QueryData>(async () => await ExecuteProcedure(query, parameters));
            return task.Result;
        }
    }
}
