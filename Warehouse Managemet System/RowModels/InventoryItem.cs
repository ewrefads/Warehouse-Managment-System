using System.Data;

namespace Warehouse_Managemet_System.Table_Models
{
    public class InventoryItem : IRowModel
    {
        public required string Id { get; set; }
        public required string WarehouseId { get; set; }
        public required string ProductId { get; set; }
        public required int Amount { get; set; }

        public bool CreateFromDataRow(DataRow row)
        {
            throw new NotImplementedException();
        }

        public string ToString()
        {
            return "";
        }
        
        public List<string> GetAllValues()
        {
            return new List<string>()
            {
                Id, WarehouseId, ProductId, Amount.ToString()
            };
        }
    }
}