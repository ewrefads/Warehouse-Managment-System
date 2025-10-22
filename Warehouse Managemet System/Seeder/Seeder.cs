using System;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.Parsers;
using Warehouse_Managemet_System.Table_Models;
using CsvHelper;

namespace Warehouse_Managemet_System.Seeders
{
    public class Seeder
    {
        private readonly IContext _context;
        private readonly Parser _parser;

        public Seeder(IContext context, Parser parser)
        {
            _context = context;
            _parser = parser;
        }

        public void PopulateTable(string filePath)
        {
            string file = filePath;
        }
    }
}