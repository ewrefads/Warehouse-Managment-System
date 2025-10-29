using System.Data;
using System.Transactions;

namespace Warehouse_Managemet_System.RowModels
{
    public class Order : IRowModel
    {
        public string? Id { get; set; }
        public string? Customer { get; set; }
        public DateTime? CreationTime { get; set; }
        public OrderStatus? Status { get; set; }
        public string? ActiveTransactionId { get; set; } // øhm 
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<RowModels.Transaction> Transactions { get; set; }

        public bool CreateFromDataRow(DataRow row)
        {
            try
            {
                Id = "";
                Customer = "";
                CreationTime = null;
                Status = OrderStatus.Reserved;
                ActiveTransactionId = null;
                OrderItems = null;
                Transactions = null;
                foreach (DataColumn c in row.Table.Columns)
                {
                    switch (c.ColumnName)
                    {
                        case "Id":
                            Id = row["Id"].ToString();
                            break;
                        case "Customer":
                            Customer = row["Customer"].ToString();
                            break;
                        case "CreationTime":
                            CreationTime = Convert.ToDateTime(row["CreationTime"]);
                            break;
                        case "Status":
                            OrderStatus orderStatus;
                            if (Enum.TryParse(row["Status"].ToString(), out orderStatus))
                            {Status = orderStatus;}
                            break;
                        case "ActiveTransactionId":
                            ActiveTransactionId = row["ActiveTransactionId"].ToString();
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
            return "Order! Id: " + Id + ", Customer: " + Customer + ", Creation time: " + CreationTime + ", Status: " + Status + ", Active Transaction Id: " + ActiveTransactionId;
        }
        
        public List<string> GetAllValues()
        {
            return new List<string>()
            {
                Id, Customer, CreationTime.ToString(), Status.ToString(), ActiveTransactionId
            };
        }
    }

    public enum OrderStatus
    {
        Reserved,
        Shipping,
        Processed
    }
}