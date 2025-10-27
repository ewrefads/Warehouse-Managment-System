using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.Contexts;

namespace Warehouse_Management_Test.Mocks.QueryHandlers
{
    /// <summary>
    /// An implementation of IContext used to unit test the QueryHandler
    /// </summary>
    public class QueryTestContext : IContext
    {
        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="modelBuilder">Not used</param>
        public void CreateTable(ModelBuilder modelBuilder)
        {
            
        }

        /// <summary>
        /// Retrieves an empty MySqlConnection
        /// </summary>
        /// <returns>an empty MySqlConnection</returns>
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection();
        }

        /// <summary>
        /// Retrieves the table name
        /// </summary>
        /// <returns>The name of the table</returns>
        public string GetTable()
        {
            return "testTable";
        }
    }
}
