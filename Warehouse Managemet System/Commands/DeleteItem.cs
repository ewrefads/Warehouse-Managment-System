using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Management_System.QueryHandlers;
using Warehouse_Managemet_System.Commands;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.RowModels;
using Warehouse_Managemet_System.SQL_Executer;

namespace Warehouse_Management_System.Commands
{
    public class DeleteItem<RowModel>:Command where RowModel : class, IRowModel, new()
    {
        public QueryHandler<RowModel> queryHandler;

        public DeleteItem()
        {

            queryHandler = new QueryHandler<RowModel>(new Context<RowModel>(new DbContextOptions<Context<RowModel>>()), new SqlExecuter());
            AddQueryHandler(queryHandler);
        }

        public DeleteItem(IQueryHandler queryHandler)
        {
            AddQueryHandler(queryHandler);
        }

        public (bool, string) DeleteSpecificProduct(string id)
        {

            return DeleteProducts(new Dictionary<string, List<string>>() { {"Id", new List<string> {" = " + id} } });
        }

        public (bool, string) DeleteProducts(Dictionary<string, List<string>> conditions)
        {
            try
            {
                return queryHandler.DeleteFromTable<RowModel>(conditions);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
