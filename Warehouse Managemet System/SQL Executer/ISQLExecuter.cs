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

        public (bool, DataTable table) ExecuteQuery(string command, MySqlConnection connection, Dictionary<string, string> paramaters);
    }
}
