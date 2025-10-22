using System;
using System.Data;

namespace Warehouse_Managemet_System.Table_Models
{
    public class MockRowModel : IRowModel
    {
        public required string Id { get; set; }
        public int Amount { get; set; }

        public bool CreateFromDataRow(DataRow row)
        {
            throw new NotImplementedException();
        }
    }
}