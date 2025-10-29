using System.Data;

namespace Warehouse_Managemet_System.RowModels
{
    public class Product : IRowModel
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required double Price { get; set; }
        public ICollection<RowModels.Transaction> Transactions { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<InventoryItem> InventoryItems { get; set; }

        public bool CreateFromDataRow(DataRow row)
        {
            throw new NotImplementedException();
        }

        public string ToString()
        {
            return "Product! Id: " + Id + ", Name: " + Name + ", Price: " + Price;
        }
        
        public List<string> GetAllValues()
        {
            return new List<string>()
            {
                Id, Name, Price.ToString()
            };
        }
    }
}