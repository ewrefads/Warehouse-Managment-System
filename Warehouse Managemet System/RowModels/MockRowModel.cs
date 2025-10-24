using System;
using System.Data;

namespace Warehouse_Managemet_System.RowModels
{
    public class MockRowModel : IRowModel
    {
        public required string Id { get; set; }
        public int Amount { get; set; }

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
                Id, Amount.ToString()
            };
        }
    }
}