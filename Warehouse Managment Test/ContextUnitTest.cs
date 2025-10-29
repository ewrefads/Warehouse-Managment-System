using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.Internal;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Management_Test
{
    public class ContextUnitTest
    {
        public Context GetContext()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            return new Context(options);
        }

        [Fact]
        public void OnModelCreating_SetsCorrectPrimaryKeys()
        {
            using var context = GetContext();
            var productEntity = context.Model.FindEntityType(typeof(Product));
            var primaryKey = productEntity.FindPrimaryKey();

            Assert.Equal("Id", primaryKey.Properties[0].Name);
        }

        [Fact]
        public void OnModelCreating_SetsCorrectForeignKeys_ProductIdExistsInOrderItem()
        {
            using var context = GetContext();
            var orderItemEntity = context.Model.FindEntityType(typeof(OrderItem));
            var foreignKeys = orderItemEntity.GetForeignKeys();

            Assert.Contains(foreignKeys, k => k.PrincipalEntityType.ClrType == typeof(Product));
        }

        [Fact]
        public void OnModelCreating_SetsCorrectForeignKeys_OrderIdExistsInOrderItem()
        {
            using var context = GetContext();
            var orderItemEntity = context.Model.FindEntityType(typeof(OrderItem));
            var foreignKeys = orderItemEntity.GetForeignKeys();

            Assert.Contains(foreignKeys, k => k.PrincipalEntityType.ClrType == typeof(Order));
        }

        [Fact]
        public void OnModelCreating_MapsCorrectRelationships_NavigationFromProductToInventoryItems()
        {
            using var context = GetContext();
            var productEntity = context.Model.FindEntityType(typeof(Product));
            var navigations = productEntity.GetNavigations().Select(n => n.Name).ToList();

            Assert.Contains("InventoryItems", navigations);
        }

        [Fact]
        public void OnModelCreating_MapsCorrectRelationships_NavigationFromProductToOrderItems()
        {
            using var context = GetContext();
            var productEntity = context.Model.FindEntityType(typeof(Product));
            var navigations = productEntity.GetNavigations().Select(n => n.Name).ToList();

            Assert.Contains("OrderItems", navigations);
        }
        
        [Fact]
        public void OnModelCreating_MapsCorrectRelationships_NavigationFromProductToTransactions()
        {
            using var context = GetContext();
            var productEntity = context.Model.FindEntityType(typeof(Product));
            var navigations = productEntity.GetNavigations().Select(n => n.Name).ToList();

            Assert.Contains("Transactions", navigations);
        }

        [Fact]
        public void GetDbSet_ReturnsCorrectDbSet()
        {
            var context = GetContext();
            var dbSet = context.GetDbSet<Product>();

            Assert.IsType<InternalDbSet<Product>>(dbSet);
        }
        
        [Fact]
        public void GetTable_ReturnsCorrectTableName()
        {
            var context = GetContext();
            var tableName = context.GetTable<Product>();

            Assert.Equal("Product", tableName);
        }

    }
}