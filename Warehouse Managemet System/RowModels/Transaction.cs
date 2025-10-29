using System.Data;

namespace Warehouse_Managemet_System.RowModels
{
    public class Transaction : IRowModel
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public TransactionType Type { set; get; }
        public int Amount { get; set; }
        public TransactionStatus Status { set; get; }
        public string? OrderId { get; set; }
        public string? FromWarehouseId { set; get; }
        public string? ToWarehouseId { set; get; }
        public Product Product { get; set; }
        public Order? Order { get; set; }
        public Warehouse? FromWarehouse { get; set; }
        public Warehouse? ToWarehouse { get; set; }

        public bool CreateFromDataRow(DataRow row)
        {
            throw new NotImplementedException();
        }

        public string ToString()
        {
            return "Transaction! Id: " + Id + ", Product Id: " + ProductId + ", Type: " + Type + ", Amount: " + Amount + ", Status: " + Status + ", Order Id: " + OrderId +  ", From-Warehouse Id: " + FromWarehouseId + ", To-Warehouse Id: " + ToWarehouseId;
        }
        
        public List<string> GetAllValues()
        {
            return new List<string>()
            {
                Id, ProductId, Type.ToString(), Amount.ToString(), Status.ToString(), OrderId, FromWarehouseId, ToWarehouseId
            };
        }
    }

    public enum TransactionType
    {
        Sale,
        Return,
        Transfer
    }

    public enum TransactionStatus
    {
        Waiting,
        Active,
        Done,
        Aborted
    }
}