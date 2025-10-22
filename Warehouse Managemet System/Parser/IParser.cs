using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managemet_System.Parsers
{
    public interface IParser
    {
        public List<MockRowModel> Parse(string filePath);
    }
}
