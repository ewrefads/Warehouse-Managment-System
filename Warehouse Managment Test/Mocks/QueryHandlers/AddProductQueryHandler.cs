using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Management_System.QueryHandlers;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Management_Test.Mocks.QueryHandlers
{
    public class AddProductQueryHandler : IQueryHandler
    {
        public List<IRowModel> inventoryItems = new List<IRowModel>();
        public (bool, string) DeleteFromTable<RowModel>(Dictionary<string, List<string>> filters) where RowModel : IRowModel
        {
            throw new NotImplementedException();
        }

        public (bool, string) InsertIntoTable<RowModel>(List<RowModel> itemsToBeInserted) where RowModel : IRowModel, new()
        {
            foreach (var item in itemsToBeInserted)
            {
                inventoryItems.Add(item);
            }
            return (true, "sucess");
        }

        public List<RowModel> SelectFromTable<RowModel>(Dictionary<string, List<string>> filters, List<string> desiredCollumns) where RowModel : IRowModel, new()
        {
            List<RowModel> returnList = new List<RowModel>();
            foreach (var item in inventoryItems)
            {
                returnList.Add((RowModel)item);
            }
            return returnList;
        }

        public (bool, string) UpdateTable<RowModel>(Dictionary<string, List<string>> filters, Dictionary<string, string> updateValues) where RowModel : IRowModel
        {
            throw new NotImplementedException();
        }
    }
}
