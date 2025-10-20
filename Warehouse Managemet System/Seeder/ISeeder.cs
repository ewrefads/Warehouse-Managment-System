using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Managemet_System.Seeder
{
    public interface ISeeder
    {
        public bool PopulateTable(DataTable data);
    }
}
