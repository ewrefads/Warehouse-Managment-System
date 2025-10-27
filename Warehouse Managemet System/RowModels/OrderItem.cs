using System.Data;

namespace Warehouse_Managemet_System.RowModels
{
    public class OrderItem : IRowModel
    {
        public required string Id { get; set; }
        public required string OrderId { get; set; }
        public required string ProductId { get; set; }
        public required int Amount { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }

        public bool CreateFromDataRow(DataRow row)
        {
            throw new NotImplementedException();
        }

        public string ToString()
        {
            return "OrderItem! Id: " + Id + ", Order Id: " + OrderId + ", Product Id: " + ProductId + ", Amount: " + Amount;
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