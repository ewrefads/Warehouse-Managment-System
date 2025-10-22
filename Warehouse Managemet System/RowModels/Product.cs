namespace Warehouse_Managemet_System.Table_Models
{
    class Product : IRowModel
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required double Price { get; set; }
    }
}