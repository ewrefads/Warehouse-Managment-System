using System.Data;

namespace Warehouse_Managemet_System.Table_Models
{
    public class Warehouse : IRowModel
    {
        public required string Id { get; set; }
        public required string Name { get; set; }

        public bool CreateFromDataRow(DataRow row)
        {
            throw new NotImplementedException();
        }
    }
}