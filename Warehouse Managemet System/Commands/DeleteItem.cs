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

            queryHandler = new QueryHandler<RowModel>("testTable", new SqlExecuter());
            AddQueryHandler(queryHandler);
        }

        public DeleteItem(IQueryHandler queryHandler)
        {
            AddQueryHandler(queryHandler);
        }

        public (bool, string) DeleteSpecificItem(string id)
        {

            return DeleteItems(new Dictionary<string, List<string>>() { {"Id", new List<string> {" = " + id} } });
        }

        public (bool, string) DeleteItems(Dictionary<string, List<string>> conditions)
        {
            try
            {
                return queryHandlers[0].DeleteFromTable<RowModel>(conditions);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string) RemoveSomeItemsFromInventory(string id, int amountToRemove)
        {
            if(typeof(RowModel) != typeof(InventoryItem))
            {
                return (false, "this command only works on InventoryItems");
            }
            List<InventoryItem> items = queryHandlers[0].SelectFromTable<InventoryItem>(new Dictionary<string, List<string>>() { { "Id", new List<string>() { " = " + id } } }, new List<string>() { "amount" });
            if(items.Count == 0)
            {
                return (false, "no items have an id of " + id);
            }
            if(amountToRemove > items[0].Amount)
            {
                return (false, "the requested amount is larger than the amount in the warehouse");
            }
            items[0].Amount -= amountToRemove;
            return queryHandlers[0].UpdateTable<InventoryItem>(new Dictionary<string, List<string>>() { { "Id", new List<string>() { " = " + id } } }, new Dictionary<string, string>() { { "Amount", items[0].Amount.ToString() } });
        }
    }
}
