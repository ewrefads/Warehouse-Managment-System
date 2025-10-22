namespace Warehouse_Managemet_System.Table_Models
{
    class OrderItem : IRowModel
    {
        public required string Id { get; set; }
        public required string ProductId { get; set; }
        public required TransactionType type { set; get; }
        public required int Amount { get; set; }
        public required TransactionStatus Status { set; get; }
        public string? FromWarehouseId { set; get; }
        public string? ToWareHouseId { set; get; }
    }

    enum TransactionType
    {
        Sale,
        Return,
        Transfer
    }

    enum TransactionStatus
    {
        Waiting,
        Active,
        Done
    }
}