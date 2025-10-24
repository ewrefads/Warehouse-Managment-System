using System.Data;

namespace Warehouse_Managemet_System.RowModels
{
    class OrderItem : IRowModel
    {
        public required string Id { get; set; }
        public required string OrderId { get; set; }
        public required string ProductId { get; set; }
        public required int Amount { get; set; }

        public bool CreateFromDataRow(DataRow row)
        {
            throw new NotImplementedException();
        }
    }
}