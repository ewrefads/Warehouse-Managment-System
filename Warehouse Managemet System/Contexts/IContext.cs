using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managemet_System.Contexts
{
    public interface IContext
    {
        public void CreateTable(ModelBuilder modelBuilder);

        public string GetTable();

        public MySqlConnection GetConnection();
    }
}

// metoder i context
// det er muligt at kalde hjælpemetoder i context
// id type
// project naming 
