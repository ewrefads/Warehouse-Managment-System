using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.Seeders
{
    public interface ISeeder<RowModel> where RowModel : class, IRowModel
    {
        public void PopulateTable(string filePath);
    }
}
