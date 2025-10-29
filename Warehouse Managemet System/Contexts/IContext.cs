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
    
    /// <summary>
    /// Defines a contract for interacting with the application's database context.
    /// Provides methods for accessing tables, saving changes, and tracking entity states.
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// Retrieves the table name for a given RowModel type.
        /// </summary>
        /// <typeparam name="RowModel"> The type of the row model. </typeparam>
        /// <returns> The name of the table associated with the RowModel type. </return>
        public string GetTable<RowModel>() where RowModel : IRowModel;

        /// <summary>
        /// Retrieves the DbSet corresponding to the specified RowModel type.
        /// </summary>
        /// <typeparam name="RowModel"> The type of the row model. </typeparam>
        /// <returns> The DbSet for the specified RowModel type. </returns>
        /// <exception cref="InvalidOperationException"> Thrown if the RowModel type is not supported. </exception>
        public DbSet<RowModel> GetDbSet<RowModel>() where RowModel : class, IRowModel;


        /// <summary>
        /// Inherited from the DbContext class.
        /// Saves all changes made in the context to the database.
        /// </summary>
        /// <returns> The number of state entries written to the database. </returns>
        int SaveChanges();


        /// <summary>
        /// Inherited from the DbContext class.
        /// Provides access to change tracking information and operations for a given entity.
        /// </summary>
        /// <param name="entity"> The entity to track. </param>
        /// <returns> An EntityEntry object for accessing and modifying tracking information. </returns>
        EntityEntry Entry(object entity);

        public string GetTable(); // redundant: needs to be removed

        public MySqlConnection GetConnection(); // redundant: needs to be removed

        public void CreateTable(ModelBuilder modelBuilder); // redundant: needs to be removed
        
    }
}
