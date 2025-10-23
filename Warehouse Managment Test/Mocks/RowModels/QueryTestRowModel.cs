using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.Table_Models;

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
            Id = -1;
            Name = "";
            FilterValue1 = -1;
            FilterValue2 = -1;
            FilterValue3 = -1;
            foreach (DataColumn c in row.Table.Columns)
            {
                switch (c.ColumnName)
                {
                    case "Id":
                        Id = Convert.ToInt32(row["Id"]);
                        break;
                    case "FilterValue1":
                        FilterValue1 = Convert.ToInt32(row["FilterValue1"]);
                        break;
                    case "FilterValue2":
                        FilterValue2 = Convert.ToInt32(row["FilterValue3"]);
                        break;
                    case "FilterValue3":
                        FilterValue3 = Convert.ToInt32(row["FilterValue3"]);
                        break;
                    case "Name":
                        Name = row["Name"].ToString();
                        break;
                }
            }
            return true;
        }
    }
}
