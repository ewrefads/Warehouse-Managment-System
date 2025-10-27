using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Management_Test.Mocks.QueryHandlers
{
    public class QueryTestContext : DbContext, IContext
    {

                public DbSet<Product> Products { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

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

        public string GetTable<RowModel>() where RowModel : IRowModel
        {
            return "";
        }

        public DbSet<RowModel> GetDbSet<RowModel>() where RowModel : class, IRowModel
        {
            return Products as DbSet<RowModel>;
        }

        int SaveChanges()
        {
            return 0;
        }
    }
}
