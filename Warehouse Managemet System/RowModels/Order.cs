using System.Data;

namespace Warehouse_Managemet_System.Table_Models
{
    public class Order : IRowModel
    {
        public required string Id { get; set; }
        public required string Customer { get; set; }
        public required DateTime CreationTime { get; set; }
        public required OrderStatus Status { get; set; }
        public required string ActiveTransactionId { get; set; }

        public bool CreateFromDataRow(DataRow row)
        {
            throw new NotImplementedException();
        }
    }

    public enum OrderStatus
    {
        Reserved,
        Shipping,
        Processed
    }
}