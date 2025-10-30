using System.Data;

namespace Warehouse_Managemet_System.RowModels
{
    public class Product : IRowModel
    {
        public string? Id { get; set; }
        public string? ProductName { get; set; }
        public double? Price { get; set; }
        public ICollection<RowModels.Transaction> Transactions { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<InventoryItem> InventoryItems { get; set; }

        public bool CreateFromDataRow(DataRow row)
        {
            try
            {
                Id = "";
                ProductName = "";
                Price = -1.0;
                Transactions = null;
                OrderItems = null;
                InventoryItems = null;
                foreach (DataColumn c in row.Table.Columns)
                {
                    switch (c.ColumnName)
                    {
                        case "Id":
                            Id = row["Id"].ToString();
                            break;
                        case "Name":
                            ProductName = row["ProductName"].ToString();
                            break;
                        case "Price":
                            Price = Convert.ToDouble(row["Price"]);
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
            return "Product! Id: " + Id + ", ProductName: " + ProductName + ", Price: " + Price;
        }
        
        public List<string> GetAllValues()
        {
            return new List<string>()
            {
                Id, ProductName, Price.ToString()
            };
        }
    }
}