using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managment_Test.Mocks.RowModels
{
    /// <summary>
    /// An implementation of IRowModel used to test the QueryHandler
    /// </summary>
    public class QueryTestRowModel:IRowModel
    {
        /// <summary>
        /// An empty constructer for the QueryTestRowModel
        /// </summary>
        public QueryTestRowModel()
        {
        }

        /// <summary>
        /// Primary constructer for the QueryTestRowModel
        /// </summary>
        /// <param name="id">the id to be used</param>
        /// <param name="name">the name to be used</param>
        /// <param name="filterValue1">the filtervalue1 to be used</param>
        /// <param name="filterValue2">the filtervalue2 to be used</param>
        /// <param name="filterValue3">the filtervalue3 to be used</param>
        public QueryTestRowModel(string id, string name, int filterValue1, int filterValue2, int filterValue3)
        {
            Id = id;
            Name = name;
            FilterValue1 = filterValue1;
            FilterValue2 = filterValue2;
            FilterValue3 = filterValue3;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int FilterValue1 { get; set; }
        public int FilterValue2 { get; set; }
        public int FilterValue3 { get; set; }

        /// <summary>
        /// Compares two QueryTestRowModels with each other to see if they have the same values
        /// </summary>
        /// <param name="other">The row model to compare to</param>
        /// <returns>Whether they share all values</returns>
        public bool CompareTo(QueryTestRowModel? other)
        {
            if (other == null) return false;
            if (other.Id != Id) return false;
            if (other.Name != Name) return false;
            if (other.FilterValue1 != FilterValue1) return false;
            if (other.FilterValue2 != FilterValue2) return false;
            if (other.FilterValue3 != FilterValue3) return false;
            return true;
        }

        /// <summary>
        /// Takes a row from a datatable and inserts its values into the row model
        /// </summary>
        /// <param name="row">the row to be used</param>
        /// <returns>Whether the operation was succcesful</returns>
        public bool CreateFromDataRow(DataRow row)
        {
            try
            {
                Id = "";
                Name = "";
                FilterValue1 = -1;
                FilterValue2 = -1;
                FilterValue3 = -1;
                foreach (DataColumn c in row.Table.Columns)
                {
                    switch (c.ColumnName)
                    {
                        case "Id":
                            Id = row["Id"].ToString();
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
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a list containing the string representation of all field variables in the instance
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllValues()
        {
            return new List<string>()
            {
                Id, Name, FilterValue1.ToString(), FilterValue2.ToString(), FilterValue3.ToString()
            };
        }
    }
}
