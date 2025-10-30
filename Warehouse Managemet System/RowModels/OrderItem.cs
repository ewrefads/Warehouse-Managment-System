using System.Data;

namespace Warehouse_Managemet_System.RowModels
{
    public class OrderItem : IRowModel
    {
        public string? Id { get; set; }
        public string? OrderId { get; set; }
        public string? ProductId { get; set; }
        public int? Amount { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }

        public bool CreateFromDataRow(DataRow row)
        {
            try
            {
                Id = "";
                OrderId = "";
                ProductId = "";
                Amount = -1;
                Order = null;
                Product = null;
                foreach (DataColumn c in row.Table.Columns)
                {
                    switch (c.ColumnName)
                    {
                        case "Id":
                            Id = row["Id"].ToString();
                            break;
                        case "OrderId":
                            OrderId = row["OrderId"].ToString();
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
            return "OrderItem! Id: " + Id + ", Order Id: " + OrderId + ", Product Id: " + ProductId + ", Amount: " + Amount;
        }
        
        public List<string> GetAllValues()
        {
            return new List<string>()
            {
                Id, OrderId, ProductId, Amount.ToString()
            };
        }
        
        public List<string> GetColumnNames()
        {
            return new List<string>()
            {
                "Id", "OrderId", "ProductId", "Amount"
            };
        }
    }
}