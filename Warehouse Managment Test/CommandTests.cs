using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Management_System.Commands;
using Warehouse_Management_System.QueryHandlers;
using Warehouse_Management_Test.Mocks.External_Systems_mocks;
using Warehouse_Management_Test.Mocks.QueryHandlers;
using Warehouse_Managemet_System.Commands;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Management_Test
{
    public class CommandTests
    {
    }
    
    public class AddProductTests
    {
        AddItemToList<InventoryItem> addProduct;
        AddProductQueryHandler addProductQueryHandler = new AddProductQueryHandler();
        public AddProductTests() 
        { 
            addProduct = new AddItemToList<InventoryItem>(addProductQueryHandler);
        }

        [Fact]
        public void ProductGetsAdded()
        {
            addProduct.AddNewProduct(new InventoryItem());
            Assert.Single(addProductQueryHandler.inventoryItems);
        }

        [Fact]
        public void ProductsGetsAdded()
        {
            List<InventoryItem> items = new List<InventoryItem>()
            {
                new InventoryItem(),
                new InventoryItem()
            };
            addProduct.AddNewProducts(items);
            Assert.Equal(2, addProductQueryHandler.inventoryItems.Count);
        }

        [Fact]
        public void AllIdsAreUniqueOnAddingToEmptyTable()
        {
            List<InventoryItem> items = new List<InventoryItem>()
            {
                new InventoryItem(),
                new InventoryItem()
            };
            addProduct.AddNewProducts(items);
            Assert.False(addProductQueryHandler.inventoryItems[0].Id == addProductQueryHandler.inventoryItems[1].Id);
        }

        [Fact]
        public void AllIdsAReUniqueOnAddingToExisistingTable()
        {
            List<InventoryItem> items = new List<InventoryItem>()
            {
                new InventoryItem(),
                new InventoryItem()
            };
            addProduct.AddNewProducts(items);
            addProduct.AddNewProducts(items);
            int matchingIds = 0;
            foreach(InventoryItem item in addProductQueryHandler.inventoryItems)
            {
                foreach(InventoryItem item1 in addProductQueryHandler.inventoryItems)
                {
                    if(item != item1 && item.Id == item1.Id)
                    {
                        matchingIds++;
                    }
                }
            }
            Assert.Equal(0, matchingIds);
        }

        [Fact]
        public void CommandHandlesExceptionOnEmptyList()
        {
            (bool, string) res = addProduct.AddNewProducts(new List<InventoryItem>());
            Assert.True(!res.Item1 && res.Item2 == "List is empty");
        }

        [Fact]
        public void ActualQueryHandlerHandlesInputFromCommand()
        {
            AddItemToList<InventoryItem> addProductWithActualQueryHandler = new AddItemToList<InventoryItem>();
            CommandTestSqlExecuter testSqlExecuter = new CommandTestSqlExecuter();
            addProductWithActualQueryHandler.queryHandler.sQLExecuter = testSqlExecuter;
            (bool, string) res = addProductWithActualQueryHandler.AddNewProduct(new InventoryItem());
            Assert.True(res.Item1);
        }
    }
}
