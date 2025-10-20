using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Managemet_System.Parser
{
    public interface IParser
    {
        public DataTable Parse(string filePath);
    }
}
