using System.Data;
using System.Transactions;

namespace Warehouse_Managemet_System.RowModels
{
    public class Warehouse : IRowModel
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public ICollection<RowModels.Transaction> IngoingTransactions { get; set; }
        public ICollection<RowModels.Transaction> OutgoingTransactions { get; set; }
        public ICollection<InventoryItem> InventoryItems { get; set; }

        public bool CreateFromDataRow(DataRow row)
        {
            throw new NotImplementedException();
        }

        public string ToString()
        {
            return "Warehouse! Id: " + Id + ", Name: " + Name;
        }
        
        public List<string> GetAllValues()
        {
            return new List<string>()
            {
                Id, Name
            };
        }
    }
}