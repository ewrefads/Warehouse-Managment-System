using System;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.Parsers;
using Warehouse_Managemet_System.Table_Models;
using CsvHelper;

namespace Warehouse_Managemet_System.Seeders
{
    public class Seeder
    {
        private readonly Context _context;
        private readonly Parsers.IParser _parser;

        public Seeder(Context context, Parsers.IParser parser)
        {
            _context = context;
            _parser = parser;
        }

        public void PopulateTable(string filePath)
        {
            var rows = _parser.Parse(filePath);

            foreach (var row in rows)
            {
                var existing = _context.Rows.Find(row.Id);
                if (existing == null)
                {
                    _context.Rows.Add(row);
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