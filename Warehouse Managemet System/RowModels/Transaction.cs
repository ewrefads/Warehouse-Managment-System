using System.Data;

namespace Warehouse_Managemet_System.RowModels
{
    public class Transaction : IRowModel
    {
        public string? Id { get; set; }
        public string? ProductId { get; set; }
        public TransactionType? Type { set; get; }
        public int? Amount { get; set; }
        public TransactionStatus? Status { set; get; }
        public string? OrderId { get; set; }
        public string? FromWarehouseId { set; get; }
        public string? ToWarehouseId { set; get; }
        public Product Product { get; set; }
        public Order? Order { get; set; }
        public Warehouse? FromWarehouse { get; set; }
        public Warehouse? ToWarehouse { get; set; }

        public bool CreateFromDataRow(DataRow row)
        {
            try
            {
                Id = "";
                ProductId = "";
                Type = TransactionType.Transfer;
                Amount = -1;
                Status = TransactionStatus.Waiting;
                OrderId = "";
                FromWarehouseId = "";
                ToWarehouseId = "";
                Product = null;
                Order = null;
                FromWarehouse = null;
                ToWarehouse = null;
                foreach (DataColumn c in row.Table.Columns)
                {
                    switch (c.ColumnName)
                    {
                        case "Id":
                            Id = row["Id"].ToString();
                            break;
                        case "ProductId":
                            ProductId = row["ProductId"].ToString();
                            break;
                        case "Type":
                            TransactionType transactionType;
                            if (Enum.TryParse(row["Type"].ToString(), out transactionType))
                            {Type = transactionType;}
                            break;
                        case "Amount":
                            Amount = Convert.ToInt32(row["Amount"]);
                            break;
                        case "Status":
                            TransactionStatus transactionStatus;
                            if (Enum.TryParse(row["Status"].ToString(), out transactionStatus))
                            {Status = transactionStatus;}
                            break;
                        case "OrderId":
                            OrderId = row["OrderId"].ToString();
                            break;
                        case "FromWarehouseId":
                            FromWarehouseId = row["FromWarehouseId"].ToString();
                            break;
                        case "ToWarehouseId":
                            ToWarehouseId = row["ToWarehouseId"].ToString();
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
            return "Transaction! Id: " + Id + " Product Id: " + ProductId + " Type: " + Type + " Amount: " + Amount + " Status: " + Status + " From-Warehouse Id: " + FromWarehouseId + " To-Warehouse Id: " + ToWarehouseId;
        }
        
        public List<string> GetAllValues()
        {
            return new List<string>()
            {
                Id, ProductId, Type.ToString(), Amount.ToString(), Status.ToString(), FromWarehouseId, ToWarehouseId
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