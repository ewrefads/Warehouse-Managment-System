using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.Seeders
{
    /// <summary>
    /// Defines a contract for populating database tables with data parsed from CSV files.
    /// Implementing classes must provide logic to adding new entries and updating existing ones based on their ID
    /// </summary>

    public interface ISeeder
    {
        /// <summary>
        /// Populates a database table with data from a CSV file. Adds new entries or updates existing ones based on their ID.
        /// </summary>
        /// <typeparam name="RowModel"> The type of row model file is based on. </typeparam>
        /// <param name="filePath"> The path to the CSV file containing the data. </param>
        public void PopulateTable<RowModel>(string filePath) where RowModel : class, IRowModel;
    }
}
