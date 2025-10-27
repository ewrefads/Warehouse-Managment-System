using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warehouse_Managemet_System.RowModels;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace Warehouse_Managemet_System.Contexts
{
    public class Context : DbContext, IContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<InventoryItem>().HasKey(i => i.Id);
            modelBuilder.Entity<Order>().HasKey(o => o.Id);
            modelBuilder.Entity<OrderItem>().HasKey(oi => oi.Id);
            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
            modelBuilder.Entity<Warehouse>().HasKey(w => w.Id);

            modelBuilder.Entity<Warehouse>().HasMany(i => i.InventoryItems).WithOne(w => w.Warehouse).HasForeignKey(w => w.WarehouseId);
        }
        public string GetTable<RowModel>() where RowModel : IRowModel
        {
            var table = Model.FindEntityType(typeof(RowModel));
            return table?.GetTableName();
        }


        public DbSet<RowModel> GetDbSet<RowModel>() where RowModel : class, IRowModel
        {
            if (typeof(RowModel) == typeof(Product)) return Products as DbSet<RowModel>;
            if (typeof(RowModel) == typeof(InventoryItem)) return InventoryItems as DbSet<RowModel>;
            if (typeof(RowModel) == typeof(Order)) return Orders as DbSet<RowModel>;
            if (typeof(RowModel) == typeof(OrderItem)) return OrderItems as DbSet<RowModel>;
            if (typeof(RowModel) == typeof(Transaction)) return Transactions as DbSet<RowModel>;
            if (typeof(RowModel) == typeof(Warehouse)) return Warehouses as DbSet<RowModel>;
            throw new InvalidOperationException($"Unsupported type: {typeof(RowModel).Name}");
        }

        public string GetTable()
        {
            return "";
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection();
        }

                public void CreateTable(ModelBuilder modelBuilder)
        {
            
        }

    }
}