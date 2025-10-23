using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.SQL_Executer;
using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managemet_System.Commands
{
    public class QueryHandler 
    {
        private IContext context;
        private ISQLExecuter sQLExecuter;

        public QueryHandler(IContext context, ISQLExecuter sQLExecuter)
        {
            this.context = context;
            this.sQLExecuter = sQLExecuter;
        }

        public bool InsertIntoTable<RowModel>(List<RowModel> itemsToBeInserted) where RowModel : IRowModel, new ()
        {
            return false;
        }

        public bool UpdateTable<RowModel>(List<RowModel> itemsToBeUpdated) where RowModel : IRowModel, new()
        {
            return false;
        }

        public bool DeleteFromTable<RowModel>(List<RowModel> itemsToBeDeleted) where RowModel : IRowModel, new()
        {
            return false;
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
                    if(desiredCollumns.Count == 0)
                    {
                        command += $"*";
                    }
                    else
                    {
                        string collumns = "";
                        for (int i = 0; i < desiredCollumns.Count; i++)
                        {
                            if(collumns.Length == 0)
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
                        command += " WHERE ";
                        string conditionsString = "";
                        foreach (string collumn in filters.Keys)
                        {
                            paramaters.Add($"@{collumn}", collumn);
                            for (int i = 0; i < filters[collumn].Count; i++)
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
                        command += conditionsString;
                    }
                    command += ";";
                    (bool, DataTable?) res = sQLExecuter.ExecuteQuery(command, conn, paramaters);
                    if(res.Item1)
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
    }
}
