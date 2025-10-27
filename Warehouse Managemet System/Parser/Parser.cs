using System;
using System.Data;
using System.Globalization;
using Warehouse_Managemet_System.RowModels;
using CsvHelper;
using System.Collections.Generic;
using System.IO;

namespace Warehouse_Managemet_System.Parsers
{
    public class Parser : Parsers.IParser
    {
        public List<RowModel> Parse<RowModel>(string filePath) where RowModel : IRowModel
        {
            try
            {
                using var reader = new StreamReader(filePath);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                return csv.GetRecords<RowModel>().ToList();
            }
            catch
            {
                return new List<RowModel>();
            }
        }
    }
}
