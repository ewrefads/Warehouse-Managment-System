using System;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.Parsers;
using Warehouse_Managemet_System.RowModels;
using CsvHelper;

namespace Warehouse_Managemet_System.Seeders
{
    public class Seeder : ISeeder
    {
        private readonly IContext _context;
        private Parsers.IParser _parser;

        public Seeder(IContext context, Parsers.IParser parser)
        {
            _context = context;
            _parser = parser;
        }

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