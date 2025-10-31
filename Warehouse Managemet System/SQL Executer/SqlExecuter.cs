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
            string res = "";
            MySqlCommand cmd = CreateCommand(command, connection, paramaters);
            Console.WriteLine(cmd.CommandText);
            int queryResult = cmd.ExecuteNonQuery();
            
            res = $"command executed succesfully. {queryResult} rows affected";
            return res;
        }

        public (bool, DataTable) ExecuteQuery(string command, MySqlConnection connection, Dictionary<string, string> paramaters)
        {
            try
            {
                MySqlCommand cmd = CreateCommand(command, connection, paramaters);
                MySqlDataReader res = cmd.ExecuteReader();
                if(res.FieldCount > 0)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(res);
                    return (true, dataTable);
                }
                else
                {
                    return (false, new DataTable());
                }
            }
            catch (Exception e)
            {
                throw e;
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
                object paramaterValue = paramaters[paramater];
                if (paramater[0] != '@')
                {
                    newParamater.Insert(0, "@");
                }
                if (!newParamater.ToLower().Contains("@id") && int.TryParse(paramaters[paramater], out int res))
                {
                    paramaterValue = res;
                }
                if (paramaters.Values.Contains("Price") && double.TryParse(paramaters[paramater], out double resdouble))
                {
                    paramaterValue = resdouble;
                }
                cmd.Parameters.AddWithValue(newParamater, paramaters[paramater]);
            }
            return cmd;
        }
    }
}
