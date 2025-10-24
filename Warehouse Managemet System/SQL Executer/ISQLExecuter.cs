using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Managemet_System.SQL_Executer
{
    /// <summary>
    /// Interface for classes executing MySqlQueries
    /// </summary>
    public interface ISQLExecuter
    {
        /// <summary>
        /// Executes a query which has no return value except the amount of rows it affected
        /// </summary>
        /// <param name="command">The sql command to be used</param>
        /// <param name="connection">The open MySqlConnection to be used</param>
        /// <param name="paramaters">Any paramaters to be used</param>
        /// <returns>
        /// If succesful and if used together with the QueryHandler a string in the format "command executed succesfully. {queryResult} rows affected" 
        /// where queryResult is the amount of rows affected.
        /// If it fails it should returns the error message
        /// </returns>
        public string ExecuteNonReturningQuery(string command, MySqlConnection connection, Dictionary<string, string> paramaters);

        /// <summary>
        /// Exceutes a query which returns data
        /// </summary>
        /// <param name="command">The sql command to be used</param>
        /// <param name="connection">The open MySqlConnection to be used</param>
        /// <param name="paramaters">Any paramaters to be used</param>
        /// <returns>whether the operation was succesfu</returns>
        public (bool, DataTable table) ExecuteQuery(string command, MySqlConnection connection, Dictionary<string, string> paramaters);
    }
}
