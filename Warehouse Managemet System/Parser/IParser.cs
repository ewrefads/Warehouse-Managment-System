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
    public interface IParser 
    {
        public List<RowModel> Parse<RowModel>(string filePath) where RowModel : IRowModel;
    }
}
