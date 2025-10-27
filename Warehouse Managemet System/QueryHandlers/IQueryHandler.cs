using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Management_System.QueryHandlers
{
    public interface IQueryHandler
    {
        public (bool, string) InsertIntoTable<RowModel>(List<RowModel> itemsToBeInserted) where RowModel : IRowModel, new();
        public (bool, string) UpdateTable<RowModel>(Dictionary<string, List<string>> filters, Dictionary<string, string> updateValues) where RowModel : IRowModel;
        public (bool, string) DeleteFromTable<RowModel>(Dictionary<string, List<string>> filters) where RowModel : IRowModel;
        public List<RowModel> SelectFromTable<RowModel>(Dictionary<string, List<string>> filters, List<string> desiredCollumns) where RowModel : IRowModel, new();
    }
}
