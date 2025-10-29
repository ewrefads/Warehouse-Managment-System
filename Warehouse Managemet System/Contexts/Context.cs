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
    /// <summary>
    /// Represents the application's database context, providing access to entity sets and configuration.
    /// Inherits from Entity Framework's DbContext and implements the IContext interface.
    /// </summary>
    public class Context : DbContext, IContext
    {
        
        /// <value> Represents the Products table in the database. </value>
        public DbSet<Product> Products { get; set; }

        /// <value> Represents the Inventory Items table in the database. </value>
        public DbSet<InventoryItem> InventoryItems { get; set; }

        /// <value> Represents the Orders table in the database. </value>
        public DbSet<Order> Orders { get; set; }

        /// <value> Represents the Order Items table in the database. </value>
        public DbSet<OrderItem> OrderItems { get; set; }

        /// <value> Represents the Transactions table in the database. </value>
        public DbSet<Transaction> Transactions { get; set; }

        /// <value> Represents the Warehouses table in the database. </value>
        public DbSet<Warehouse> Warehouses { get; set; }

        /// <summary>
        /// Initializes a new instance of the Context class with the specified options.
        /// </summary>
        /// <param name="options">The options to configure the DbContext.</param>
        public Context(DbContextOptions<Context> options) : base(options) { }


        /// <summary>
        /// Configures the entity relationships and keys using Fluent API.
        /// </summary>
        /// <param name="modelBuilder"> The builder used to configure entity mappings. </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<InventoryItem>().HasKey(i => i.Id);
            modelBuilder.Entity<Order>().HasKey(o => o.Id);
            modelBuilder.Entity<OrderItem>().HasKey(oi => oi.Id);
            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
            modelBuilder.Entity<Warehouse>().HasKey(w => w.Id);

            modelBuilder.Entity<Product>().HasMany(i => i.InventoryItems).WithOne(p => p.Product).HasForeignKey(p => p.ProductId);
            modelBuilder.Entity<Product>().HasMany(oi => oi.OrderItems).WithOne(p => p.Product).HasForeignKey(p => p.ProductId);
            modelBuilder.Entity<Product>().HasMany(t => t.Transactions).WithOne(p => p.Product).HasForeignKey(p => p.ProductId);
            modelBuilder.Entity<Order>().HasMany(oi => oi.OrderItems).WithOne(o => o.Order).HasForeignKey(o => o.OrderId);
            modelBuilder.Entity<Order>().HasMany(t => t.Transactions).WithOne(o => o.Order).HasForeignKey(o => o.OrderId);
            modelBuilder.Entity<Warehouse>().HasMany(i => i.InventoryItems).WithOne(w => w.Warehouse).HasForeignKey(w => w.WarehouseId);
            modelBuilder.Entity<Warehouse>().HasMany(t => t.IngoingTransactions).WithOne(w => w.ToWarehouse).HasForeignKey(w => w.ToWareHouseId);
            modelBuilder.Entity<Warehouse>().HasMany(t => t.OutgoingTransactions).WithOne(w => w.FromWarehouse).HasForeignKey(w => w.FromWarehouseId);
        }
        
        /// <summary>
        /// Retrieves the table name for a given RowModel type.
        /// </summary>
        /// <typeparam name="RowModel"> The type of the row model. </typeparam>
        /// <returns> The name of the table associated with the RowModel type. </return>
        public string GetTable<RowModel>() where RowModel : IRowModel
        {
            var table = Model.FindEntityType(typeof(RowModel));
            return table?.GetTableName();
        }

        /// <summary>
        /// Retrieves the DbSet corresponding to the specified RowModel type.
        /// </summary>
        /// <typeparam name="RowModel"> The type of the row model. </typeparam>
        /// <returns> The DbSet for the specified RowModel type. </returns>
        /// <exception cref="InvalidOperationException"> Thrown if the RowModel type is not supported. </exception>
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

        // to be removed
        public string GetTable()
        {
            return "";
        }

        // to be removed
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection();
        }

        // to be removed
        public void CreateTable(ModelBuilder modelBuilder)
        {

        }

    }
}