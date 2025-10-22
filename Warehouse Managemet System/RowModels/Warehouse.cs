namespace Warehouse_Managemet_System.Table_Models
{
    class Warehouse : IRowModel
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
    }
}