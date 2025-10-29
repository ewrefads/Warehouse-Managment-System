using System.Data;
using System.Transactions;

namespace Warehouse_Managemet_System.RowModels
{
    public class Order : IRowModel
    {
        public string Id { get; set; }
        public string Customer { get; set; }
        public DateTime CreationTime { get; set; }
        public OrderStatus Status { get; set; }
        public string? ActiveTransactionId { get; set; } // øhm 
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<RowModels.Transaction> Transactions { get; set; }

        public bool CreateFromDataRow(DataRow row)
        {
            throw new NotImplementedException();
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
        Processed,
        Cancelled,
        None
    }
}