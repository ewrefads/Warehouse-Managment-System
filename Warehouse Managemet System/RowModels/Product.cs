using System.Data;

namespace Warehouse_Managemet_System.Table_Models
{
    class Product : IRowModel
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required double Price { get; set; }

        public bool CreateFromDataRow(DataRow row)
        {
            throw new NotImplementedException();
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