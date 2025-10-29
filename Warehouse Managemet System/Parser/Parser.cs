using System;
using System.Data;
using System.Globalization;
using Warehouse_Managemet_System.RowModels;
using CsvHelper;
using System.Collections.Generic;
using System.IO;

namespace Warehouse_Managemet_System.Parsers
{
    
    /// <summary>
    /// Parser class responsible for reading CSV files and converting them into a list of row models.
    /// Implements the IParser interface.
    /// </summary>
    public class Parser : Parsers.IParser
    {
        /// <summary>
        /// Parses a CSV file located at the given file path into a list of RowModel objects.
        /// </summary>
        /// <typeparam name="RowModel"> The type of row model to parse from csv (Must implement IRowModel) </typeparam>
        /// <param name="filePath"> The path to the CSV file to be parsed. </param>
        /// <returns> A list of parsed RowModel objects. Returns an empty list if parsing fails.</returns>
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
