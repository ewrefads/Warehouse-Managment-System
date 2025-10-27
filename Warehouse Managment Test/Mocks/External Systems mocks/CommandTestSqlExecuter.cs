using MySql.Data.MySqlClient;
using Mysqlx.Prepare;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.SQL_Executer;

namespace Warehouse_Management_Test.Mocks.External_Systems_mocks
{
    public class CommandTestSqlExecuter : ISQLExecuter
    {
        public string ExecuteNonReturningQuery(string command, MySqlConnection connection, Dictionary<string, string> paramaters)
        {
            return "command executed succesfully";
        }

        public (bool, DataTable) ExecuteQuery(string command, MySqlConnection connection, Dictionary<string, string> paramaters)
        {
            return (true, new DataTable());
        }
    }
}
