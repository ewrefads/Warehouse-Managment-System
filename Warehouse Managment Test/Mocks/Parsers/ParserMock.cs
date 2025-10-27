using System;
using System.Xml.Serialization;
using Warehouse_Managemet_System.Parsers;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managment_Test.Mocks.Parsers
{
    public class ParserMock : Warehouse_Managemet_System.Parsers.IParser 
    {
        public List<RowModel> Parse<RowModel>(string filePath) where RowModel : IRowModel
        {
            return new List<RowModel>();
        }
    }
}