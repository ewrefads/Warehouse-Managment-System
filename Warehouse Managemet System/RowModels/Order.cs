namespace Warehouse_Managemet_System.Table_Models
{
    class Order : IRowModel
    {
        public required string Id { get; set; }
        public required string Customer { get; set; }
        public required DateTime CreationTime { get; set; }
        public required OrderStatus Status { get; set; }
        public required string ActiveTransactionId { get; set; }
    }

    enum OrderStatus
    {
        Reserved,
        Shipping,
        Processed
    }
}