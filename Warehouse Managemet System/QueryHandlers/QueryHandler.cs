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

        public List<RowModel> SelectFromTable<RowModel>(Dictionary<string, List<string>> filters) where RowModel : IRowModel, new()
        {
            try
            {
                using (MySqlConnection conn = context.GetConnection())
                {
                    string command = $"SELECT * FROM {context.GetTable()}";
                    (bool, DataTable?) res = sQLExecuter.ExecuteQuery(command, conn, new Dictionary<string, string>());
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
