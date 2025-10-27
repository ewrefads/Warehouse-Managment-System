using System.Data;

namespace Warehouse_Managemet_System.RowModels
{
    public class Transaction : IRowModel
    {
        public required string Id { get; set; }
        public required string ProductId { get; set; }
        public required TransactionType Type { set; get; }
        public required int Amount { get; set; }
        public required TransactionStatus Status { set; get; }
        public string? OrderId { get; set; }
        public string? FromWarehouseId { set; get; }
        public string? ToWareHouseId { set; get; }
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
            return "Transaction! Id: " + Id + " Product Id: " + ProductId + " Type: " + Type + " Amount: " + Amount + " Status: " + Status + " From-Warehouse Id: " + FromWarehouseId + " To-Warehouse Id: " + ToWareHouseId;
        }
        
        public List<string> GetAllValues()
        {
            return new List<string>()
            {
                Id, ProductId, Type.ToString(), Amount.ToString(), Status.ToString(), FromWarehouseId, ToWareHouseId
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
        Done
    }
}