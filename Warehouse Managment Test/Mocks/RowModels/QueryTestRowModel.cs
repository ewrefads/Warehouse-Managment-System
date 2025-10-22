using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managment_Test.Mocks.RowModels
{
    public class QueryTestRowModel:IRowModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FilterValue1;
        public int FilterValue2;
        public int FilterValue3;
    }
}
