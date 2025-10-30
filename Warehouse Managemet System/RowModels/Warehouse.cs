using System.Data;
using System.Transactions;

namespace Warehouse_Managemet_System.RowModels
{
    public class Warehouse : IRowModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public ICollection<RowModels.Transaction> IngoingTransactions { get; set; }
        public ICollection<RowModels.Transaction> OutgoingTransactions { get; set; }
        public ICollection<InventoryItem> InventoryItems { get; set; }

        public bool CreateFromDataRow(DataRow row)
        {
            try
            {
                Id = "";
                Name = "";
                IngoingTransactions = null;
                OutgoingTransactions = null;
                InventoryItems = null;
                foreach (DataColumn c in row.Table.Columns)
                {
                    switch (c.ColumnName)
                    {
                        case "Id":
                            Id = row["Id"].ToString();
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

        public string ToString()
        {
            return "Warehouse! Id: " + Id + ", Name: " + Name;
        }
        
        public List<string> GetAllValues()
        {
            return new List<string>()
            {
                Id, Name
            };
        }
        
        public List<string> GetColumnNames()
        {
            return new List<string>()
            {
                "Id", "Name"
            };
        }
    }
}