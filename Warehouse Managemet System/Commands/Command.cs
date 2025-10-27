using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Management_System.QueryHandlers;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.Commands
{
    public abstract class Command
    {
        protected List<IQueryHandler> queryHandlers = new List<IQueryHandler>();

        protected void AddQueryHandler(IQueryHandler queryHandler)
        { 
            queryHandlers.Add(queryHandler); 
        }
    }
}
