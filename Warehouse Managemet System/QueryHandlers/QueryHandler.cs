using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managemet_System.Commands
{
    public class QueryHandler<T> where T : IRowModel
    {
        private IContext context;

        public QueryHandler(IContext context)
        {
            this.context = context;
        }

        public bool InsertIntoTable(List<T> itemsToBeInserted)
        {
            return false;
        }

        public bool UpdateTable(List<T> itemsToBeUpdated)
        {
            return false;
        }

        public bool DeleteFromTable(List<T> itemsToBeDeleted)
        { 
            return false;
        }

        public List<T> SelectFromTable(Dictionary<string, List<string>> filters)
        {
            return new List<T>();
        }
    }
}
