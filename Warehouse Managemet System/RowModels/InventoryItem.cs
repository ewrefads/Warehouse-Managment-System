using Bogus.DataSets;
using System.Data;
using System.Net.Http.Headers;

namespace Warehouse_Managemet_System.RowModels
{
    public class InventoryItem : IRowModel
    {
        public string Id { get; set; }
        public string WarehouseId { get; set; }
        public string ProductId { get; set; }
        public int Amount { get; set; }
        public Product Product { get; set; }
        public Warehouse Warehouse { get; set; }

        public bool CreateFromDataRow(DataRow row)
        {
            try
            {
                Id = "";
                WarehouseId = "";
                ProductId = "";
                Amount = -1;
                Product = null;
                Warehouse = null;
                foreach (DataColumn c in row.Table.Columns)
                {
                    switch (c.ColumnName)
                    {
                        case "Id":
                            Id = row["Id"].ToString();
                            break;
                        case "WarehouseId":
                            WarehouseId = row["WarehouseId"].ToString();
                            break;
                        case "ProductId":
                            ProductId = row["ProductId"].ToString();
                            break;
                        case "Amount":
                            Amount = Convert.ToInt32(row["Amount"]);
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
            return "InventoryItem! Id: " + Id + ", Warehouse Id: " + WarehouseId + ", Product Id: " + ProductId + ", Amount: " + Amount;
        }
        
        public List<string> GetAllValues()
        {
            return new List<string>()
            {
                Id, WarehouseId, ProductId, Amount.ToString()
            };
        }
        
        public List<string> GetColumnNames()
        {
            return new List<string>()
            {
                "Id", "WarehouseId", "ProductId", "Amount"
            };
        }
    }
}