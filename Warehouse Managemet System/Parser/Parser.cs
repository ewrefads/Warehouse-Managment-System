using System;
using System.Data;
using System.Globalization;
using Warehouse_Managemet_System.Table_Models;
using CsvHelper;

namespace Warehouse_Managemet_System.Parsers
{
    public class Parser : IParser
    {
        public List<MockRowModel> Parse(string filePath)
        {
            try
            {
                using var reader = new StreamReader(filePath);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                return csv.GetRecords<MockRowModel>().ToList();
            }
            catch
            {
                return new List<MockRowModel>();
            }
        }
    }
}
