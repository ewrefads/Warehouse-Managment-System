using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.Contexts;

namespace Warehouse_Managment_Test.Mocks.QueryHandlers
{
    public class QueryTestContext : IContext
    {
        public bool CreateTable(string pathToTable)
        {
            throw new NotImplementedException();
        }

        public string GetTable()
        {
            throw new NotImplementedException();
        }
    }
}
