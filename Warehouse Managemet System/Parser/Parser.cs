using System;
using System.Data;
using System.Globalization;
using Warehouse_Managemet_System.RowModels;
using CsvHelper;
using System.Collections.Generic;
using System.IO;

namespace Warehouse_Managemet_System.Parsers
{
    public class Parser<RowModel> : Parsers.IParser<RowModel> where RowModel : IRowModel
    {
        public List<RowModel> Parse(string filePath) 
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
