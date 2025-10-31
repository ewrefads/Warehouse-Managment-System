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
    public class AddItem<RowModel> : Command where RowModel : class, IRowModel, new()
    {
        public QueryHandler<RowModel> queryHandler;


        public AddItem(IQueryHandler queryHandler)
        {
            AddQueryHandler(queryHandler);
        }

        public (bool, string) AddNewItem(RowModel item)
        {
            return AddNewItems(new List<RowModel>() { item });
        }

        public (bool, string) AddNewItems(List<RowModel> items)
        {
            
            try
            {
                if(items.Count == 0)
                {
                    throw new Exception("List is empty");
                }
                List<RowModel> ids = queryHandlers[0].SelectFromTable<RowModel>(new Dictionary<string, List<string>>(), new List<string>() { "Id" });
                string largestId = "-1";
                if(ids.Count > 0)
                {
                    largestId = ids.OrderByDescending(x => x.Id).ToList()[0].Id;
                    
                }
                int currentId = Int32.Parse(largestId) + 1;
                for (int i = 0; i < items.Count; i++)
                {
                    RowModel item = items[i];
                    item.Id = currentId.ToString();
                    currentId++;
                }
                return queryHandlers[0].InsertIntoTable<RowModel>(items);
            }
            catch(Exception e)
            {
                return (false, e.Message);
            }
        }
    }
}
