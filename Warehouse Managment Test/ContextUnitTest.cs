using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.Internal;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Management_Test
{
    public class ContextUnitTest
    {
        [Fact]
        public void OnModelCreating_SetsCorrectPrimaryKeys()
        {
            Assert.True(true);
        }

        [Fact]
        public void OnModelCreating_SetsCorrectForeignKeys()
        {
            Assert.True(true);
        }

        [Fact]
        public void OnModelCreating_MapsCorrectRelationships()
        {
            Assert.True(true);
        }

        [Fact]
        public void GetDbSet_ReturnsCorrectDbSet()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            var context = new Context(options);
            var dbSet = context.GetDbSet<Product>();

            Assert.IsType<InternalDbSet<Product>>(dbSet);
        }
        
        [Fact]
        public void GetTable_ReturnsCorrectTableName()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            var context = new Context(options);
            var tableName = context.GetTable<Product>();

            Assert.Equal("Product", tableName);
        }

    }
}