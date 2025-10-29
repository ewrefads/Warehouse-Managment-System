using System;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.Parsers;
using Warehouse_Managemet_System.RowModels;
using CsvHelper;

namespace Warehouse_Managemet_System.Seeders
{
    /// <summary>
    /// Seeder class responsible for populating database tables with data parsed from CSV files.
    /// Implements the ISeeder interface.
    /// </summary>
    public class Seeder : ISeeder
    {
        /// <value>
        /// The database context used to interact with the data store.
        /// Provides access to DbSets and handles saving changes.
        /// </value>
        private readonly IContext _context;
        
        /// <value>
        /// The parser used to read and convert CSV data into row model objects.
        /// </value>
        private Parsers.IParser _parser;

        /// <summary>
        /// Constructor for the Seeder class. Initializes the database context and parser dependencies.
        /// </summary>
        /// <param name="context"> The database context. </param>
        /// <param name="parser"> The parser for CSV files. </param>
        public Seeder(IContext context, Parsers.IParser parser)
        {
            _context = context;
            _parser = parser;
        }

        /// <summary>
        /// Populates a database table with data from a CSV file. Adds new entries or updates existing ones based on their ID.
        /// </summary>
        /// <typeparam name="RowModel"> The type of row model file is based on. </typeparam>
        /// <param name="filePath"> The path to the CSV file containing the data. </param>
        public void PopulateTable<RowModel>(string filePath) where RowModel : class, IRowModel
        {
            List<RowModel> rows = _parser.Parse<RowModel>(filePath);
            var table = _context.GetDbSet<RowModel>();

            foreach (var row in rows)
            {
                var existing = table.Find(row.Id);
                if (existing == null)
                {
                    table.Add(row);
                }
                else
                {
                    _context.Entry(existing).CurrentValues.SetValues(row);
                }
            }

            _context.SaveChanges();

        }
    }
}