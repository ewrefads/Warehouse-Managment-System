using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managemet_System.Commands
{
    public abstract class QueryHandler
    {
        protected IContext context;

        public bool InsertIntoTable(List<IRowModel> itemsToBeInserted)
        {
            return false;
        }

        public bool UpdateTable(List<IRowModel> itemsToBeUpdated)
        {
            return false;
        }

        public bool DeleteFromTable(List<IRowModel> itemsToBeDeleted)
        { 
            return false;
        }

        public List<IRowModel> SelectFromTable(Dictionary<string, List<string>> filters)
        {
            return new List<IRowModel>();
        }
    }
}
