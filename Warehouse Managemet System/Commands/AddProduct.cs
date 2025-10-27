using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Management_System.QueryHandlers;
using Warehouse_Managemet_System.Commands;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.RowModels;
using Warehouse_Managemet_System.SQL_Executer;

namespace Warehouse_Management_System.Commands
{
    public class AddProduct : Command
    {
        public QueryHandler<InventoryItem> queryHandler;

        public AddProduct()
        {

            queryHandler = new QueryHandler<InventoryItem>(new Context<InventoryItem>(new DbContextOptions<Context<InventoryItem>>()), new SqlExecuter());
            AddQueryHandler(queryHandler);    
        }

        public AddProduct(IQueryHandler queryHandler)
        {
            AddQueryHandler(queryHandler);
        }

        public (bool, string) AddNewProduct(InventoryItem inventoryItem)
        {
            return AddNewProducts(new List<InventoryItem>() { inventoryItem });
        }

        public (bool, string) AddNewProducts(List<InventoryItem> inventoryItems)
        {
            
            try
            {
                if(inventoryItems.Count == 0)
                {
                    throw new Exception("List is empty");
                }
                List<InventoryItem> ids = queryHandlers[0].SelectFromTable<InventoryItem>(new Dictionary<string, List<string>>(), new List<string>() { "Id" });
                string largestId = "-1";
                if(ids.Count > 0)
                {
                    largestId = ids.OrderByDescending(x => x.Id).ToList()[0].Id;
                    
                }
                int currentId = Int32.Parse(largestId) + 1;
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    InventoryItem item = inventoryItems[i];
                    item.Id = currentId.ToString();
                    currentId++;
                }
                return queryHandlers[0].InsertIntoTable<InventoryItem>(inventoryItems);
            }
            catch(Exception e)
            {
                return (false, e.Message);
            }
        }
    }
}
