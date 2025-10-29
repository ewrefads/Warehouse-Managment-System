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

        public string GetTable<RowModel>() where RowModel : IRowModel;

        public DbSet<RowModel> GetDbSet<RowModel>() where RowModel : class, IRowModel;

        int SaveChanges();

        EntityEntry Entry(object entity);

        public string GetTable(); // redundant: needs to be removed

        public MySqlConnection GetConnection(); // redundant: needs to be removed

        public void CreateTable(ModelBuilder modelBuilder); // redundant: needs to be removed
        
    }
}
