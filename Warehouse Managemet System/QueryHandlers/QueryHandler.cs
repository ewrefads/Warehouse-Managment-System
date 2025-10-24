using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.SQL_Executer;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.Commands
{
    public class QueryHandler 
    {
        private IContext context;
        private ISQLExecuter sQLExecuter;

        /// <summary>
        /// Constructor for the queryhandler
        /// </summary>
        /// <param name="context">the iContext implementation to be used by the query handler</param>
        /// <param name="sQLExecuter">the ISQLExecuter implementation to be used by the query handler</param>
        public QueryHandler(IContext context, ISQLExecuter sQLExecuter)
        {
            this.context = context;
            this.sQLExecuter = sQLExecuter;
        }

        /// <summary>
        /// Inserts a list of rowmodels into the QueryHandlers table
        /// </summary>
        /// <typeparam name="RowModel">The IRowModel implementation for the table</typeparam>
        /// <param name="itemsToBeInserted">The items to be inserted. Must be ready to be placed in the table when given to this method</param>
        /// <returns>Whether the operation was succesful and the succes message</returns>
        /// <exception cref="Exception">An exception is thrown if the sql query fails to execute</exception>
        public (bool,string) InsertIntoTable<RowModel>(List<RowModel> itemsToBeInserted) where RowModel : IRowModel, new()
        {
            try
            {
                using (MySqlConnection conn = context.GetConnection())
                {
                    Dictionary<string, string> paramaters = new Dictionary<string, string>();
                    string command = $"INSERT INTO {context.GetTable()} VALUES";
                    string valueString = "";
                    for (int i = 0; i < itemsToBeInserted.Count; i++)
                    {
                        RowModel item = itemsToBeInserted[i];
                        if (valueString.Length > 0)
                        {
                            valueString += ",";
                        }
                        valueString += GetValueString(item.GetAllValues(), paramaters);
                    }
                    command += valueString;
                    command += ";";
                    string res = sQLExecuter.ExecuteNonReturningQuery(command, conn, paramaters);
                    if (res.Contains("command executed succesfully"))
                    {
                        return (true, res);
                    }
                    else
                    {
                        throw new Exception("Sql query failed");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Formats the given values into the correct format for an INSERT INTO sql Query and updates the paramaters dictionary
        /// </summary>
        /// <param name="values">the values to be formated</param>
        /// <param name="paramaters">the paramaters dictionary to be updated</param>
        /// <returns>A string with the values in the desired format</returns>
        private string GetValueString(List<string> values, Dictionary<string, string> paramaters)
        {
            string valueString = "(";
            for (int i = 0; i < values.Count; i++)
            {
                if (valueString.Length > 1)
                {
                    valueString += ", ";                    
                }
                string paramaterName = $"@value{paramaters.Count}";
                paramaters.Add(paramaterName, values[i]);

                valueString += paramaterName;
            }
            valueString += ")";
            return valueString;
        }

        /// <summary>
        /// Updates table rows with new values limited by the given filters 
        /// </summary>
        /// <typeparam name="RowModel">The tables IRowModel implementation</typeparam>
        /// <param name="filters">The filter conditions to use. The key is the collumn and the value list is all logical conditions to be applied to it</param>
        /// <param name="updateValues">The collumns to be updated and the value to update them with. The collumn is the key</param>
        /// <returns>Whether the operation was succesful and the succes message</returns>
        /// <exception cref="Exception">An exception is thrown if the sql query fails to execute</exception>
        public (bool, string) UpdateTable<RowModel>(Dictionary<string, List<string>> filters, Dictionary<string, string> updateValues) where RowModel : IRowModel
        {
            try
            {
                using (MySqlConnection conn = context.GetConnection())
                {
                    Dictionary<string, string> paramaters = new Dictionary<string, string>();
                    string command = $"UPDATE {context.GetTable()} SET ";
                    List<(string, string)> valuePairs = GetValuePairs(updateValues, paramaters);
                    string updateString = "";
                    foreach ((string, string) valuePair in valuePairs)
                    {
                        command += $"{valuePair.Item1} = {valuePair.Item2}";
                    }
                    command += updateString;
                    if (filters.Count > 0)
                    {
                        updateString += $" WHERE {GetConditionString(filters, paramaters)}";
                    }
                    
                    command += ";";
                    string res = sQLExecuter.ExecuteNonReturningQuery(command, conn, paramaters);
                    if (res.Contains("command executed succesfully"))
                    {
                        return (true, res);
                    }
                    else
                    {
                        throw new Exception("Sql query failed");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Creates a list containing tuples with the paramater name of collumns and the paramater name of their desired value. 
        /// it also updates the paramaters dictionary with their actual values.
        /// </summary>
        /// <param name="updateValues">A dictionary with the collumn name as key and its desired value as the value</param>
        /// <param name="paramaters">A dictionary with paramater names as key and their value as value</param>
        /// <returns>A List of tupples consisting of a collumn paramater name and their value paramater name</returns>
        private List<(string, string)> GetValuePairs(Dictionary<string, string> updateValues, Dictionary<string, string> paramaters)
        {
            List<(string, string)> valuePairs = new List<(string, string)>();
            foreach (string collumn in updateValues.Keys)
            {
                if(!paramaters.ContainsKey($"@{collumn}"))
                {
                    paramaters.Add($"@{collumn}", collumn);
                }
                paramaters.Add($"@{collumn}val", updateValues[collumn]);
                if(!float.TryParse(updateValues[collumn], out float res))
                {
                    paramaters[$"@{collumn}val"] = $"'{updateValues[collumn]}'";
                }
                valuePairs.Add(($"@{collumn}", $"@{collumn}val"));
            }
            return valuePairs;
        }

        /// <summary>
        /// Deletes an item from the table
        /// </summary>
        /// <typeparam name="RowModel">The tables IRowModel implementation</typeparam>
        /// <param name="filters">The conditions to be used. The key is collumns and the value is a list of conditions to be applied to it</param>
        /// <returns>Whether the operation was succesful and the succes message</returns>
        /// <exception cref="Exception">An exception is thrown if the sql query fails to execute</exception>
        public (bool, string) DeleteFromTable<RowModel>(Dictionary<string, List<string>> filters) where RowModel : IRowModel
        {
            try
            {
                using (MySqlConnection conn = context.GetConnection())
                {
                    Dictionary<string, string> paramaters = new Dictionary<string, string>();
                    string command = $"DELETE FROM {context.GetTable()}";
                    if (filters.Count > 0)
                    {
                        command += $" WHERE {GetConditionString(filters, paramaters)}";
                    }
                    command += ";";
                    string res = sQLExecuter.ExecuteNonReturningQuery(command, conn, paramaters);
                    if (res.Contains("command executed succesfully"))
                    {
                        return (true, res);
                    }
                    else
                    {
                        throw new Exception("Sql query failed");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// A method to select items from a specific table in a MySQL database
        /// </summary>
        /// <typeparam name="RowModel">The table row model representation class used for the output</typeparam>
        /// <param name="filters">A dictionary of collumn names and conditions for them to be included in the query</param>
        /// <param name="desiredCollumns">Which collumns should be shown</param>
        /// <returns>A list of all items in the table which met the previous conditions</returns>
        /// <exception cref="Exception">An exception is thrown if there was any errors with the given input or other issues with the connection to the server</exception>
        public List<RowModel> SelectFromTable<RowModel>(Dictionary<string, List<string>> filters, List<string> desiredCollumns) where RowModel : IRowModel, new()
        {
            try
            {
                using (MySqlConnection conn = context.GetConnection())
                {
                    Dictionary<string, string> paramaters = new Dictionary<string, string>();
                    string command = "SELECT ";
                    if (desiredCollumns.Count == 0)
                    {
                        command += $"*";
                    }
                    else
                    {
                        string collumns = "";
                        for (int i = 0; i < desiredCollumns.Count; i++)
                        {
                            if (collumns.Length == 0)
                            {
                                collumns = desiredCollumns[i];
                            }
                            else
                            {
                                collumns += $", {desiredCollumns[i]}";
                            }
                        }
                        command += "@selectedCollumns";
                        paramaters.Add("@selectedCollumns", collumns);

                    }
                    command += $" FROM {context.GetTable()}";
                    if (filters.Count > 0)
                    {
                        command += $" WHERE {GetConditionString(filters, paramaters)}";
                    }
                    command += ";";
                    (bool, DataTable?) res = sQLExecuter.ExecuteQuery(command, conn, paramaters);
                    if (res.Item1)
                    {
                        DataTable dataTable = res.Item2;
                        List<RowModel> returnedRowModels = new List<RowModel>();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            RowModel rowModel = new RowModel();
                            rowModel.CreateFromDataRow(row);
                            returnedRowModels.Add(rowModel);
                        }
                        return returnedRowModels;
                    }
                    else
                    {
                        throw new Exception("Sql query failed");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        /// <summary>
        /// Formats the conditions and updates the paramaters dictionary
        /// </summary>
        /// <param name="filters">The filters to format</param>
        /// <param name="paramaters">The paramaters dictionary to update</param>
        /// <returns>The correctly formatted condtions string</returns>
        private string GetConditionString(Dictionary<string, List<string>> filters, Dictionary<string, string> paramaters)
        {
            string conditionsString = "";
            foreach (string collumn in filters.Keys)
            {
                if(!paramaters.ContainsKey($"@{collumn}"))
                {
                    paramaters.Add($"@{collumn}", collumn);
                }
                
                for(int i = 0; i < filters[collumn].Count; i++)
                            {
                    string condition = filters[collumn][i];
                    paramaters.Add($"@{collumn}con{i}", condition);
                    if (conditionsString.Length > 0)
                    {
                        conditionsString += "AND ";
                    }
                    conditionsString += $"@{collumn} @{collumn}con{i} ";
                }
            }
            return conditionsString;            
        }
    }
}
