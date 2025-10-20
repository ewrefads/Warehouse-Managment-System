using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Managemet_System.Commands
{
    public abstract class Command
    {
        List<QueryHandler> queryHandlers;
        public Command(List<QueryHandler> queryHandlers)
        {
            this.queryHandlers = queryHandlers;
        }
    }
}
