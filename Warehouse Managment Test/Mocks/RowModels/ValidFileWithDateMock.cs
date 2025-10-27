using System;
using System.Data;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Management_Test.Mocks.RowModels
{
    public class ValidFileWithDateMock : IRowModel
    {
        public required string Id { get; set; }
        public DateTime Date { get; set; }

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
                Id, Date.ToString()
            };
        }
    }
}