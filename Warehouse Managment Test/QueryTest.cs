using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.Commands;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.Table_Models;
using Warehouse_Managment_Test.Mocks;
using Warehouse_Managment_Test.Mocks.QueryHandlers;
using Warehouse_Managment_Test.Mocks.RowModels;

namespace Warehouse_Managment_Test
{
    public class QueryTest
    {
        private IContext context;
        private QueryHandler<QueryTestRowModel> handler;
        public QueryTest() 
        {
            context = new QueryTestContext();
            context.CreateTable("");
            handler = new QueryHandler<QueryTestRowModel>(context);
        }

    }
}
