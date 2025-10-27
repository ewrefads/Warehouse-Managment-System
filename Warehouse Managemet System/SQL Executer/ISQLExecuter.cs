using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Managemet_System.SQL_Executer
{
    public interface ISQLExecuter
    {
        public string ExecuteNonReturningQuery(string command, MySqlConnection connection, Dictionary<string, string> paramaters);

        /// <summary>
        /// Exceutes a query which returns data
        /// </summary>
        /// <param name="command">The sql command to be used</param>
        /// <param name="connection">The open MySqlConnection to be used</param>
        /// <param name="paramaters">Any paramaters to be used</param>
        /// <returns>whether the operation was succesfu</returns>
        public (bool, DataTable) ExecuteQuery(string command, MySqlConnection connection, Dictionary<string, string> paramaters);
    }
}
