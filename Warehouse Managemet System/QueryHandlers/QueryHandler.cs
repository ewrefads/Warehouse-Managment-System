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
