using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Management_System.Commands;
using Warehouse_Management_System.QueryHandlers;
using Warehouse_Management_Test.Mocks.External_Systems_mocks;
using Warehouse_Management_Test.Mocks.QueryHandlers;
using Warehouse_Management_Test.Mocks.RowModels;
using Warehouse_Managemet_System.Commands;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Management_Test
{
    public class CommandTests
    {
    }
    
    public class AddItemTests
    {
        AddItem<QueryTestRowModel> addItem;
        CommandTestQueryHandler addProductQueryHandler = new CommandTestQueryHandler();
        public AddItemTests() 
        { 
            addItem = new AddItem<QueryTestRowModel>(addProductQueryHandler);
        }

        [Fact]
        public void ItemGetsAdded()
        {
            addItem.AddNewItem(new QueryTestRowModel());
            Assert.Single(addProductQueryHandler.inventoryItems);
        }

        [Fact]
        public void ItemsGetsAdded()
        {
            List<QueryTestRowModel> items = new List<QueryTestRowModel>()
            {
                new QueryTestRowModel(),
                new QueryTestRowModel()
            };
            addItem.AddNewItems(items);
            Assert.Equal(2, addProductQueryHandler.inventoryItems.Count);
        }

        [Fact]
        public void AllIdsAreUniqueOnAddingToEmptyTable()
        {
            List<QueryTestRowModel> items = new List<QueryTestRowModel>()
            {
                new QueryTestRowModel(),
                new QueryTestRowModel()
            };
            addItem.AddNewItems(items);
            Assert.False(addProductQueryHandler.inventoryItems[0].Id == addProductQueryHandler.inventoryItems[1].Id);
        }

        [Fact]
        public void AllIdsAReUniqueOnAddingToExisistingTable()
        {
            List<QueryTestRowModel> items = new List<QueryTestRowModel>()
            {
                new QueryTestRowModel(),
                new QueryTestRowModel()
            };
            addItem.AddNewItems(items);
            addItem.AddNewItems(items);
            int matchingIds = 0;
            foreach(QueryTestRowModel item in addProductQueryHandler.inventoryItems)
            {
                foreach(QueryTestRowModel item1 in addProductQueryHandler.inventoryItems)
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
            (bool, string) res = addItem.AddNewItems(new List<QueryTestRowModel>());
            Assert.True(!res.Item1 && res.Item2 == "List is empty");
        }

        [Fact]
        public void ActualQueryHandlerHandlesInputFromCommand()
        {
            AddItem<QueryTestRowModel> addProductWithActualQueryHandler = new AddItem<QueryTestRowModel>();
            CommandTestSqlExecuter testSqlExecuter = new CommandTestSqlExecuter();
            addProductWithActualQueryHandler.queryHandler.sQLExecuter = testSqlExecuter;
            (bool, string) res = addProductWithActualQueryHandler.AddNewItem(new QueryTestRowModel());
            Assert.True(res.Item1);
        }
    }

    public class DeleteItemTests
    {
        DeleteItem<QueryTestRowModel> deleteItem;
        CommandTestQueryHandler commandTestQueryHandler = new CommandTestQueryHandler();
        public DeleteItemTests()
        {
            deleteItem = new DeleteItem<QueryTestRowModel>(commandTestQueryHandler);
        }

        [Fact]
        public void DeleteItemDeletesSpecificItem()
        {
            List<IRowModel> queryTestRowModels = new List<IRowModel>()
            {
                new QueryTestRowModel(),
                new QueryTestRowModel()
            };
            queryTestRowModels[0].Id = "0";
            queryTestRowModels[1].Id = "1";
            commandTestQueryHandler.inventoryItems = queryTestRowModels;
            deleteItem.DeleteSpecificItem("0");
            Assert.Equal("1", commandTestQueryHandler.inventoryItems[0].Id);

        }

        [Fact]
        public void DeleteItemDeletesAllMeetingCondition()
        {
            List<IRowModel> queryTestRowModels = new List<IRowModel>()
            {
                new QueryTestRowModel("0", "test0", 0, 0, 0),
                new QueryTestRowModel("1", "test1", 1, 0, 0),
                new QueryTestRowModel("2", "test2", 2, 0, 0)
            };
            commandTestQueryHandler.inventoryItems = queryTestRowModels;
            deleteItem.DeleteItems(new Dictionary<string, List<string>>() { {"FilterValue1", new List<string>() {" >= 1" } } });
            Assert.Single(commandTestQueryHandler.inventoryItems);
        }

        [Fact]
        public void DeleteItemHandlesExceptionsFromQueryHandler()
        {
            List<IRowModel> queryTestRowModels = new List<IRowModel>()
            {
                new QueryTestRowModel(),
                new QueryTestRowModel()
            };
            queryTestRowModels[0].Id = "throwsException";
            commandTestQueryHandler.inventoryItems = queryTestRowModels;
            (bool, string) res = deleteItem.DeleteSpecificItem("throwsException");
            Assert.True(!res.Item1 && res.Item2 == "exception in queryHandler");
        }

        [Fact]
        public void ActualQueryHandlerHandlesInputFromCommand()
        {
            DeleteItem<QueryTestRowModel> DeleteItemWithActualQueryHandler = new DeleteItem<QueryTestRowModel>();
            CommandTestSqlExecuter testSqlExecuter = new CommandTestSqlExecuter();
            DeleteItemWithActualQueryHandler.queryHandler.sQLExecuter = testSqlExecuter;
            (bool, string) res = DeleteItemWithActualQueryHandler.DeleteSpecificItem("0");
            Assert.True(res.Item1);
        }
    }
}
