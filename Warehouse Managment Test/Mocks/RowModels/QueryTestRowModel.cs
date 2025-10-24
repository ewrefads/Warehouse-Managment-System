using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managment_Test.Mocks.RowModels
{
    public class QueryTestRowModel:IRowModel
    {
        public QueryTestRowModel()
        {
        }

        public QueryTestRowModel(int id, string name, int filterValue1, int filterValue2, int filterValue3)
        {
            Id = id;
            Name = name;
            FilterValue1 = filterValue1;
            FilterValue2 = filterValue2;
            FilterValue3 = filterValue3;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int FilterValue1 { get; set; }
        public int FilterValue2 { get; set; }
        public int FilterValue3 { get; set; }

        public bool CreateFromDataRow(DataRow row)
        {
            try
            {
                Id = Convert.ToInt32(row["id"]);
                Name = row["name"].ToString();
                FilterValue1 = Convert.ToInt32(row["FilterValue1"]);
                FilterValue2 = Convert.ToInt32(row["FilterValue2"]);
                FilterValue3 = Convert.ToInt32(row["FilterValue3"]);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
