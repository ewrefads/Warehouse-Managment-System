using System.Data;

namespace Warehouse_Managemet_System.RowModels
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
    }
}