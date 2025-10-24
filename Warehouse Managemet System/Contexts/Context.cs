using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.Contexts
{
    public class Context<RowModel> : DbContext, IContext where RowModel : class, IRowModel
    {
        public Context(DbContextOptions<Context<RowModel>> options) : base(options) { }

        public DbSet<RowModel> Rows { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RowModel>()
            .HasKey(i => i.Id);
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