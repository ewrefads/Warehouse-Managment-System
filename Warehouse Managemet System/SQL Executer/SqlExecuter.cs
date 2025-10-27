using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.SQL_Executer
{
    /// <summary>
    /// The standard implementation of ISQLExecuter
    /// </summary>
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

        public (bool, DataTable) ExecuteQuery(string command, MySqlConnection connection, Dictionary<string, string> paramaters)
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

        /// <summary>
        /// Creates a MySqlCommand and adds the paramaters to it
        /// </summary>
        /// <param name="command">The command itself as a string</param>
        /// <param name="connection">The connection to be used</param>
        /// <param name="paramaters">The paramaters to be used</param>
        /// <returns>The MySqlCommand object ready to be executed</returns>
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
