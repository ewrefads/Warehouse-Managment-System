namespace Warehouse_Managemet_System.Table_Models
{
    class OrderItem : IRowModel
    {
        public required string OrderId { get; set; }
        public required string ProductId { get; set; }
        public required int Amount { get; set; }
    }
}