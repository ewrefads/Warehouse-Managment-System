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
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<MockRowModel> Rows { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MockRowModel>()
            .HasKey(i => i.Id);
        }

    }
}