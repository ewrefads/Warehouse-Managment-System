using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Management_System.QueryHandlers;
using Warehouse_Managemet_System.Commands;
using Warehouse_Managemet_System.RowModels;
using Warehouse_Managemet_System.SQL_Executer;

namespace Warehouse_Management_System.Commands
{
    public class GetItem<RowModel> : Command where RowModel : class, IRowModel, new()
    {

        public GetItem(IQueryHandler queryHandler)
        {
            AddQueryHandler(queryHandler);
        }

        public (bool, RowModel) RetrieveItem(string id)
        {
            try
            {
                List<string> desiredCollumns = new List<string>();
                List<RowModel> res = queryHandlers[0].SelectFromTable<RowModel>(new Dictionary<string, List<string>>() { { "Id", new List<string>() { " = " + id } } }, desiredCollumns);
                if (res.Count == 0)
                {
                    return (false, new RowModel() { Id = "No item with an id of " + id + " could be found" });
                }
                return (true, res[0]);
            }
            catch (Exception ex)
            {
                return (false, new RowModel() {Id = ex.Message });
            }
        }

        public (bool, List<RowModel>) RetrieveItems(Dictionary<string, List<string>>? filters, List<string>? ids)
        {
            try
            {
                List<RowModel> res = new List<RowModel>();
                if (filters != null)
                {
                    res = queryHandlers[0].SelectFromTable<RowModel>(filters, new List<string>());

                }
                else if(ids != null)
                {
                    foreach(string id in ids)
                    {
                        (bool, RowModel) retrieveRes = RetrieveItem(id);
                        if(retrieveRes.Item1)
                        {
                            res.Add(retrieveRes.Item2);
                        }
                        else
                        {
                            res.Add(new RowModel() { Id = "No item with an id of " + id + " exist" });
                        }
                    }
                }
                else
                {
                    res = queryHandlers[0].SelectFromTable<RowModel>(new Dictionary<string, List<string>>(), new List<string>());
                }
                if(res.Count == 0)
                {
                    res.Add(new RowModel { Id = "No items matching the criteria could be found" });
                }
                return (true, res);
            }
            catch (Exception ex)
            {
                return (false, new List<RowModel>() { new RowModel() { Id = ex.Message } });
            }
            
        }
    }
}
