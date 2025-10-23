using System.Data;

namespace Warehouse_Managemet_System.Table_Models
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

        public List<string> GetAllValues()
        {
            return new List<string>()
            {
                Id, OrderId, ProductId, Amount.ToString()
            };
        }
    }
}