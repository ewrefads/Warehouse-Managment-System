using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Warehouse_Managemet_System.Contexts
{
    public interface IContext
    {
        protected void CreateTable(ModelBuilder modelBuilder);

    }
}
