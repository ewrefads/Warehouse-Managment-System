using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managemet_System.SQL_Executer
{
    public class SqlExecuter : ISQLExecuter
    {
        public string ExecuteNonReturningQuery(string command, MySqlConnection connection, Dictionary<string, string> paramaters)
        {
            try
            {
                string res = "";
                MySqlCommand cmd = CreateCommand(command, connection, paramaters);
                int queryResult = cmd.ExecuteNonQuery();
                res = $"command executed succesfully. {queryResult} rows affected";
                return res;
            }
            catch(Exception e) 
            { 
                return e.Message;
            }
        }

        public (bool, DataTable table) ExecuteQuery(string command, MySqlConnection connection, Dictionary<string, string> paramaters)
        {
            try
            {
                MySqlCommand cmd = CreateCommand(command, connection, paramaters);
                MySqlDataReader res = cmd.ExecuteReader();
                DataTable dataTable = res.GetSchemaTable();
                return (true, dataTable);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return (false, null);
            }
        }

        private MySqlCommand CreateCommand(string command, MySqlConnection connection, Dictionary<string, string> paramaters)
        {
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = command;
            foreach (string paramater in paramaters.Keys)
            {
                string newParamater = paramater;
                if (paramater[0] != '@')
                {
                    newParamater.Insert(0, "@");
                }
                cmd.Parameters.AddWithValue(newParamater, paramaters[paramater]);
            }
            return cmd;
        }
    }
}
