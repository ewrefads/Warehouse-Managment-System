using System;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.Parsers;
using Warehouse_Managemet_System.RowModels;
using CsvHelper;

namespace Warehouse_Managemet_System.Seeders
{
    public class Seeder<RowModel> : ISeeder where RowModel : class, IRowModel
    {
        private readonly Context<RowModel> _context;
        private readonly Parsers.IParser<RowModel> _parser;

        public Seeder(Context<RowModel> context, Parsers.IParser<RowModel> parser)
        {
            _context = context;
            _parser = parser;
        }

        public void PopulateTable(string filePath) 
        {
            List<RowModel> rows = _parser.Parse(filePath);

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