using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.Contexts;

namespace Warehouse_Managment_Test.Mocks.QueryHandlers
{
    public class QueryTestContext : IContext
    {
        public bool CreateTable(string pathToTable)
        {
            return true;
        }

        public void CreateTable(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection();
        }

        public string GetTable()
        {
            return "testTable";
        }
    }
}
