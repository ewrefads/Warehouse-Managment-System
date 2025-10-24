using System.Data;

namespace Warehouse_Managemet_System.RowModels
{
    class Warehouse : IRowModel
    {
        public required string Id { get; set; }
        public required string Name { get; set; }

        public bool CreateFromDataRow(DataRow row)
        {
            throw new NotImplementedException();
        }
    }
}