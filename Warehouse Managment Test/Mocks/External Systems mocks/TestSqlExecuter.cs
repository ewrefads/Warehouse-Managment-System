using Moq;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.SQL_Executer;
using Warehouse_Management_Test.Mocks.RowModels;

namespace Warehouse_Management_Test.Mocks.External_Systems_mocks
{
    /// <summary>
    /// An implementation of ISQLExecuter to unit test the QueryHandler
    /// </summary>
    public class TestSqlExecuter : ISQLExecuter
    {
        /// <summary>
        /// The Rowmodels contained in the simulated database
        /// </summary>
        public List<QueryTestRowModel> results;

        /// <summary>
        /// The constructer of the TestSQLExecuter class
        /// </summary>
        /// <param name="results">The initial value of the results list</param>
        public TestSqlExecuter(List<QueryTestRowModel> results)
        {
            this.results = results;
        }
        /// <summary>
        /// Takes the command and verifies its made as a known command and to the correct table. It then simulates the command based on the paramaters given
        /// </summary>
        /// <param name="command">The command to be simulated</param>
        /// <param name="connection">not used</param>
        /// <param name="paramaters">The paramaters of the command</param>
        /// <returns></returns>
        public string ExecuteNonReturningQuery(string command, MySqlConnection connection, Dictionary<string, string> paramaters)
        {
            try
            {
                //Removes all objects from the list meeting the conditions given in the paramaters
                if (command.Contains("DELETE") && command.Contains("testTable"))
                {
                    Dictionary<string, List<(string, int)>> filterValues = GetConditions(0, paramaters);
                    int affectedRows = 0;
                    List<QueryTestRowModel> rowsToBeDeleted = new List<QueryTestRowModel>();
                    foreach (QueryTestRowModel queryTestRowModel in results)
                    {
                        if (ValidateRow(filterValues, queryTestRowModel))
                        {
                            affectedRows++;
                            rowsToBeDeleted.Add(queryTestRowModel);
                        }
                    }
                    foreach (QueryTestRowModel rowModel in rowsToBeDeleted)
                    {
                        results.Remove(rowModel);
                    }
                    return $"command executed succesfully. {affectedRows} rows affected";
                }
                //updates the given values in all objects meeting the conditions given
                else if (command.Contains("UPDATE") && command.Contains("testTable"))
                {
                    Dictionary<string, object> updateValues = GetUpdateValues(paramaters);
                    Dictionary<string, List<(string, int)>> filterValues = GetConditions(0, paramaters);
                    int affectedRows = 0;
                    foreach (QueryTestRowModel rowModel in results)
                    {
                        if (ValidateRow(filterValues, rowModel))
                        {
                            foreach (string collumn in updateValues.Keys)
                            {
                                UpdateCollumn(collumn, updateValues[collumn], rowModel);
                            }
                            affectedRows++;
                        }
                    }
                    return $"command executed succesfully. {affectedRows} rows affected";
                }
                //Creates and adds QueryTestRowModels based on the data given in the paramaters
                else if(command.Contains("INSERT") && command.Contains("testTable"))
                {
                    int remainingVariables = 4;
                    QueryTestRowModel rowModel = new QueryTestRowModel();
                    int affectedRows = 0;
                    for(int i = 0; i < paramaters.Count; i++)
                    {
                        string paramater = paramaters[paramaters.Keys.ToList()[i]];
                        switch(remainingVariables)
                        {
                            case 4:
                                if(paramater.Length == 0)
                                {
                                    throw new Exception("Id must have a value");
                                }
                                rowModel = new QueryTestRowModel();
                                rowModel.Id = paramater;
                                remainingVariables--;
                                break;
                            case 3:
                                rowModel.Name = paramater;
                                remainingVariables--;
                                break;
                            case 2:
                                if(Int32.TryParse(paramater, out int param))
                                {
                                    rowModel.FilterValue1 = param;
                                    remainingVariables--;
                                }
                                else
                                {
                                    throw new Exception("paramater was not int");
                                }
                                break;
                            case 1:
                                if (Int32.TryParse(paramater, out param))
                                {
                                    rowModel.FilterValue2 = param;
                                    remainingVariables--;
                                }
                                else
                                {
                                    throw new Exception("paramater was not int");
                                }
                                break;
                            case 0:
                                if (Int32.TryParse(paramater, out param))
                                {
                                    rowModel.FilterValue3 = param;
                                    results.Add(rowModel);
                                    remainingVariables = 4;
                                    affectedRows++;
                                }
                                else
                                {
                                    throw new Exception("paramater was not int");
                                }
                                break;
                        }
                    }
                    return $"command executed succesfully. {affectedRows} rows affected";
                }
                else
                {
                    return "unknown query type";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Simulates the query on the results list
        /// </summary>
        /// <param name="command">The command to be executed</param>
        /// <param name="connection">Not used</param>
        /// <param name="paramaters">The paramaters to use</param>
        /// <returns></returns>
        public (bool, DataTable?) ExecuteQuery(string command, MySqlConnection connection, Dictionary<string, string> paramaters)
        {
            //Retrieves all collumns desired from all objects meeting the conditions and creates a datatable with this information
            if (command.Contains("SELECT") && command.Contains("testTable"))
            {
                DataTable result = new DataTable();
                bool returnValue = true;
                int ignoredKeyAmount = 0;
                if (paramaters.ContainsKey("@selectedCollumns"))
                {
                    ignoredKeyAmount++;
                    string[] desiredCollumns = paramaters["@selectedCollumns"].Split(", ");
                    for (int i = 0; i < desiredCollumns.Length; i++)
                    {
                        if (desiredCollumns[i] == "Name" || desiredCollumns[i] == "Id")
                        {
                            result.Columns.Add(desiredCollumns[i], typeof(string));
                        }
                        else if (desiredCollumns[i] == "FilterValue4")
                        {
                            returnValue = false;
                        }
                        else
                        {
                            result.Columns.Add(desiredCollumns[i], typeof(int));
                        }
                    }
                }
                else
                {
                    result.Columns.Add("Id", typeof(string));
                    result.Columns.Add("Name", typeof(string));
                    result.Columns.Add("FilterValue1", typeof(int));
                    result.Columns.Add("FilterValue2", typeof(int));
                    result.Columns.Add("FilterValue3", typeof(int));
                }
                Dictionary<string, List<(string, int)>> filterValues = GetConditions(ignoredKeyAmount, paramaters);
                foreach (QueryTestRowModel rowModel in results)
                {
                    bool validRow = true;
                    if (filterValues.Count > 0)
                    {
                        validRow = ValidateRow(filterValues, rowModel);
                    }
                    if (validRow)
                    {
                        object[] values = new object[result.Columns.Count];
                        for (int i = 0; i < values.Length; i++)
                        {
                            values[i] = GetRowValue(result.Columns[i].ColumnName, rowModel);
                        }
                        result.Rows.Add(values);
                    }
                }
                if (returnValue)
                {
                    return (true, result);
                }
                else
                {
                    return (false, null);
                }
            }
            else
            {
                return (false, null);
            }
        }

        /// <summary>
        /// Returns the value of a given variable in a QueryTestRowModel
        /// </summary>
        /// <param name="collumnName">the name of the collumn</param>
        /// <param name="rowModel">The rowmodel to get the value from</param>
        /// <returns>
        /// The value of the desired property in the rowModel. 
        /// As it is a pure object which is returned a casting to the actual type should be done on the result
        /// </returns>
        /// <exception cref="Exception">An exception is thrown if an unknown collumn name is given</exception>
        private object GetRowValue(string collumnName, QueryTestRowModel rowModel)
        {
            switch (collumnName)
            {
                case "Id":
                    return rowModel.Id;
                case "Name":
                    return rowModel.Name;
                case "FilterValue1":
                    return rowModel.FilterValue1;
                case "FilterValue2":
                    return rowModel.FilterValue2;
                case "FilterValue3":
                    return rowModel.FilterValue3;
                default:
                    throw new Exception("unknown collumn");
            }
        }

        /// <summary>
        /// Executes the action given in a operator string
        /// </summary>
        /// <param name="op">the logical operator to be executed</param>
        /// <param name="x">the first value</param>
        /// <param name="y">the second operator</param>
        /// <returns>The boolean result of the operation</returns>
        /// <exception cref="Exception">An exception is thrown if the op does not match a known operator</exception>
        private bool StringOperator(string op, int x, int y)
        {
            switch (op)
            {
                case "=": return x == y;
                case "<>": return x != y;
                case ">": return x > y;
                case ">=": return x >= y;
                case "<": return x < y;
                case "<=": return x <= y;
                default: throw new Exception($"Unknown operator: {op}");
            }
        }

        /// <summary>
        /// Retrieves conditions from the paramaters
        /// </summary>
        /// <param name="ignoredKeyAmount">the amount of paramaters to ignore</param>
        /// <param name="paramaters">The paramaters to use</param>
        /// <returns>A dictionary with the collumns to use conditions on as key and a list of tuples consisting of the operator and the comparison value as value</returns>
        private Dictionary<string, List<(string, int)>> GetConditions(int ignoredKeyAmount, Dictionary<string, string> paramaters)
        {
            Dictionary<string, List<(string, int)>> filterValues = new Dictionary<string, List<(string, int)>>();
            if (paramaters.Count > ignoredKeyAmount)
            {
                for (int i = 0; i < paramaters.Count; i++)
                {
                    string paramater = paramaters.Keys.ToList()[i];
                    if (paramater == "@selectedCollumns")
                    {
                        continue;
                    }
                    else if (paramater.Contains("con"))
                    {
                        string[] parts = paramaters[paramater].Split(' ');
                        string oCollumn = paramater.Split("con")[0];
                        int value = int.Parse(parts[1]);
                        string key = oCollumn.Remove(0, 1);
                        if (filterValues.ContainsKey(key))
                        {
                            filterValues[key].Add((parts[0], value));
                        }
                        else
                        {
                            filterValues.Add(key, new List<(string, int)>() { (parts[0], value) });
                        }
                    }
                }
            }
            return filterValues;
        }

        /// <summary>
        /// Creates a dictionary with collumns as key and the desired value as value
        /// </summary>
        /// <param name="paramaters">The paramaters to get the values from</param>
        /// <returns>The desired dictionary of collumns and their desired values</returns>
        /// <exception cref="Exception"></exception>
        private Dictionary<string, object> GetUpdateValues(Dictionary<string, string> paramaters)
        {
            Dictionary<string, object> UpdateValues = new Dictionary<string, object>();
            foreach (string paramater in paramaters.Keys)
            {
                if (paramater.Contains("val"))
                {
                    string fieldName = paramater.Split("val")[0];
                    fieldName = fieldName.Remove(0, 1);
                    switch(fieldName)
                    {
                        case "Name":
                        case "Id":
                            UpdateValues.Add(fieldName, paramaters[paramater].Split("'")[1]);
                            break;                        
                        case "FilterValue1":
                        case "FilterValue2":
                        case "FilterValue3":
                            if (int.TryParse(paramaters[paramater], out int val))
                            {
                                UpdateValues.Add(fieldName, val);
                            }
                            else
                            {
                                throw new Exception("invalid type");
                            }
                            break;
                        default:
                            throw new Exception("Unknown collumn");
                    }
                }
            }
            return UpdateValues;
        }

        /// <summary>
        /// Updates a given collumn with a given value
        /// </summary>
        /// <param name="collumn">The collumn to be updated</param>
        /// <param name="value">The new value</param>
        /// <param name="rowModel">The rowmodel containing the value</param>
        private void UpdateCollumn(string collumn, object value, QueryTestRowModel rowModel)
        {
            switch(collumn)
            {
                case "Id":
                    rowModel.Id = value.ToString();
                    break;
                case "Name":
                    rowModel.Name = value.ToString();
                    break;
                case "FilterValue1":
                    rowModel.FilterValue1 = (int)value;
                    break;
                case "FilterValue2":
                    rowModel.FilterValue2 = (int)value;
                    break;
                case "FilterValue3":
                    rowModel.FilterValue3 = (int)value;
                    break;
            }
        }

        /// <summary>
        /// Verifies that a given rolemodel meets the given conditions
        /// </summary>
        /// <param name="filterValues">The conditions to be used</param>
        /// <param name="rowModel">The rowmodel to verify</param>
        /// <returns>Whether it meets all the conditions</returns>
        private bool ValidateRow(Dictionary<string, List<(string, int)>> filterValues, QueryTestRowModel rowModel)
        {
            bool validRow = true;
            if (filterValues.Count > 0)
            {
                foreach (string paramater in filterValues.Keys)
                {
                    int value = (int)GetRowValue(paramater, rowModel);
                    foreach ((string, int) comparator in filterValues[paramater])
                    {
                        if (!StringOperator(comparator.Item1, value, comparator.Item2))
                        {
                            validRow = false;
                            break;
                        }
                    }
                }
            }
            return validRow;
        }
    }

}
