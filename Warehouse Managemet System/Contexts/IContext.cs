using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.Contexts
{
    public interface IContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        public string GetTable<RowModel>() where RowModel : IRowModel;

        public DbSet<RowModel> GetDbSet<RowModel>() where RowModel : class, IRowModel;

        int SaveChanges();

        EntityEntry Entry(object entity);

        public string GetTable();

        public MySqlConnection GetConnection();

        public void CreateTable(ModelBuilder modelBuilder);
        
    }
}
