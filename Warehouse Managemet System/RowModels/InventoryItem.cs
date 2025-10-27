using Bogus.DataSets;
using System.Data;
using System.Net.Http.Headers;

namespace Warehouse_Managemet_System.RowModels
{
    public class InventoryItem : IRowModel
    {
        public required string Id { get; set; }
        public required string WarehouseId { get; set; }
        public required string ProductId { get; set; }
        public required int Amount { get; set; }
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
            return "InventoryItem! Id: " + Id + " Warehouse Id: " + WarehouseId + " Product Id: " + " Amount: " + Amount;
        }
        
        public List<string> GetAllValues()
        {
            return new List<string>()
            {
                Id, WarehouseId, ProductId, Amount.ToString()
            };
        }
    }
}