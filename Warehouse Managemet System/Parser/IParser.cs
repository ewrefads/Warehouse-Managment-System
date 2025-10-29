using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.Parsers
{
    /// <summary>
    /// Defines a contract for parsing CSV files into a list of row models.
    /// Implementing classes must provide logic to read and convert CSV data.
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Parses the CSV file at the specified file path into a list of RowModel objects.
        /// </summary>
        /// <typeparam name="RowModel"> The type of row model to parse, (Must implement IRowModel) </typeparam>
        /// <param name="filePath"> The path to the CSV file to be parsed. </param>
        /// <returns> A list of parsed RowModel objects. </returns>
        public List<RowModel> Parse<RowModel>(string filePath) where RowModel : IRowModel;
    }
}
