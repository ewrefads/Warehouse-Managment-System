using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Managemet_System.RowModels
{
    public interface IRowModel
    {
        public string Id { get; set; }
        public bool CreateFromDataRow(DataRow row);
        public string ToString();
        public List<string> GetAllValues();
    }
}
