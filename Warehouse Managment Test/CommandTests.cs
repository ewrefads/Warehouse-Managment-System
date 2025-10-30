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
            foreach (QueryTestRowModel item in addProductQueryHandler.inventoryItems)
            {
                foreach (QueryTestRowModel item1 in addProductQueryHandler.inventoryItems)
                {
                    if (item != item1 && item.Id == item1.Id)
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
            deleteItem.DeleteItems(new Dictionary<string, List<string>>() { { "FilterValue1", new List<string>() { " >= 1" } } });
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
        public void InventoryItemAmountGetsSuccessfullyRemoved()
        {
            InventoryItem item = new InventoryItem();
            item.Id = "0";
            item.Amount = 3;
            List<IRowModel> queryTestRowModels = new List<IRowModel>()
            {
                item
            };
            DeleteItem<InventoryItem> deleteItemWithInventoryItem = new DeleteItem<InventoryItem>(commandTestQueryHandler);
            commandTestQueryHandler.inventoryItems = queryTestRowModels;
            deleteItemWithInventoryItem.RemoveSomeItemsFromInventory("0", 2);
            Assert.Equal(1, item.Amount);
        }

        [Fact]
        public void InventoryItemAmountCanGetToZero()
        {
            InventoryItem item = new InventoryItem();
            item.Id = "0";
            item.Amount = 3;
            List<IRowModel> queryTestRowModels = new List<IRowModel>()
            {
                item
            };
            DeleteItem<InventoryItem> deleteItemWithInventoryItem = new DeleteItem<InventoryItem>(commandTestQueryHandler);
            commandTestQueryHandler.inventoryItems = queryTestRowModels;
            deleteItemWithInventoryItem.RemoveSomeItemsFromInventory("0", 3);
            Assert.Equal(0, item.Amount);
        }

        [Fact]
        public void OnlyInventoryItemsCanHaveAmountRemoved()
        {
            Assert.False(deleteItem.RemoveSomeItemsFromInventory("0", 2).Item1);
        }

        [Fact]
        public void InventoryItemIdMustExistsForAmountChange()
        {
            InventoryItem item = new InventoryItem();
            item.Id = "0";
            item.Amount = 3;
            List<IRowModel> queryTestRowModels = new List<IRowModel>()
            {
                item
            };
            DeleteItem<InventoryItem> deleteItemWithInventoryItem = new DeleteItem<InventoryItem>(commandTestQueryHandler);
            commandTestQueryHandler.inventoryItems = queryTestRowModels;
            Assert.False(deleteItemWithInventoryItem.RemoveSomeItemsFromInventory("1", 2).Item1);
        }

        [Fact]
        public void AmountToBeRemovedCannotBeHigherThanAmountInInventoryItem()
        {
            InventoryItem item = new InventoryItem();
            item.Id = "0";
            item.Amount = 3;
            List<IRowModel> queryTestRowModels = new List<IRowModel>()
            {
                item
            };
            DeleteItem<InventoryItem> deleteItemWithInventoryItem = new DeleteItem<InventoryItem>(commandTestQueryHandler);
            commandTestQueryHandler.inventoryItems = queryTestRowModels;
            Assert.False(deleteItemWithInventoryItem.RemoveSomeItemsFromInventory("0", 4).Item1);
        }

        [Fact]
        public void OnlyOrdersCanGetCancelled()
        {
            (bool, string) res = deleteItem.CancelOrder("0");
            Assert.True(!res.Item1 && res.Item2 == "this command only works on Orders");
        }

        [Fact]
        public void OrderMustExist()
        {
            commandTestQueryHandler.inventoryItems = new List<IRowModel>()
            {
                new Order(){Id = "0" }
            };
            DeleteItem<Order> deleteItemWithOrder = new DeleteItem<Order>(commandTestQueryHandler);
            (bool, string) res = deleteItemWithOrder.CancelOrder("1");
            Assert.True(!res.Item1 && res.Item2 == "no orders exists with an id of 1");
        }

        [Fact]
        public void OrderCannotBeCancelledIfItHasBeenProccessed()
        {
            commandTestQueryHandler.inventoryItems = new List<IRowModel>()
            {
                new Order(){Id = "0", Status = OrderStatus.Processed }
            };
            DeleteItem<Order> deleteItemWithOrder = new DeleteItem<Order>(commandTestQueryHandler);
            (bool, string) res = deleteItemWithOrder.CancelOrder("0");
            Assert.True(!res.Item1 && res.Item2 == "Order has allready been completed");
        }

        [Fact]
        public void OrderCannotBeCancelledIfItHasAllreadyBeenCancelled()
        {
            commandTestQueryHandler.inventoryItems = new List<IRowModel>()
            {
                new Order(){Id = "0", Status = OrderStatus.Cancelled }
            };
            DeleteItem<Order> deleteItemWithOrder = new DeleteItem<Order>(commandTestQueryHandler);
            (bool, string) res = deleteItemWithOrder.CancelOrder("0");
            Assert.True(!res.Item1 && res.Item2 == "Order has allready been cancelled");
        }

        [Fact]
        public void OrderGetsCancelled()
        {
            Order order = new Order() { Id = "0", Status = OrderStatus.Reserved, Transactions = new List<Transaction>() };
            commandTestQueryHandler.inventoryItems = new List<IRowModel>()
            {
                order
            };
            DeleteItem<Order> deleteItemWithOrder = new DeleteItem<Order>(commandTestQueryHandler);
            (bool, string) res = deleteItemWithOrder.CancelOrder("0");

            Assert.Equal(OrderStatus.Cancelled, order.Status);
        }

        [Theory]
        [InlineData(TransactionStatus.Waiting)]
        [InlineData(TransactionStatus.Active)]
        public void TransactionGetsAbortedOnOrderCancellation(TransactionStatus transactionStatus)
        {
            Transaction transaction = new Transaction()
            {
                Id = "0",
                OrderId = "0",
                Status = transactionStatus
            };
            Order order = new Order() { Id = "0", Status = OrderStatus.Reserved, Transactions = new List<Transaction>() { transaction } };
            commandTestQueryHandler.inventoryItems = new List<IRowModel>()
            {
                order
            };
            DeleteItem<Order> deleteItemWithOrder = new DeleteItem<Order>(commandTestQueryHandler);
            deleteItemWithOrder.CancelOrder("0");
            Assert.Equal(TransactionStatus.Aborted, transaction.Status);
        }

        [Theory]
        [InlineData(TransactionStatus.Done)]
        [InlineData(TransactionStatus.Aborted)]
        public void TransactionIsUnchangedOnOrderCancellation(TransactionStatus transactionStatus)
        {
            Transaction transaction = new Transaction()
            {
                Id = "0",
                OrderId = "0",
                Status = transactionStatus
            };
            Order order = new Order() { Id = "0", Status = OrderStatus.Reserved, Transactions = new List<Transaction>() { transaction } };
            commandTestQueryHandler.inventoryItems = new List<IRowModel>()
            {
                order
            };
            DeleteItem<Order> deleteItemWithOrder = new DeleteItem<Order>(commandTestQueryHandler);
            deleteItemWithOrder.CancelOrder("0");
            Assert.Equal(transactionStatus, transaction.Status);
        }
    }

    public class GetItemTests
    {
        GetItem<QueryTestRowModel> getitem;
        CommandTestQueryHandler commandTestQueryHandler = new CommandTestQueryHandler();
        public GetItemTests()
        {
            getitem = new GetItem<QueryTestRowModel>(commandTestQueryHandler);
        }

        [Fact]
        public void GetItemShouldReturnItem()
        {
            commandTestQueryHandler.inventoryItems.Add(new QueryTestRowModel() { Id = "0", Name = "Test" });
            (bool, QueryTestRowModel) res = getitem.RetrieveItem("0");
            Assert.Equal("Test", res.Item2.Name);
        }
        [Fact]
        public void GetItemHandlesNoItemsReturned()
        {
            commandTestQueryHandler.inventoryItems.Add(new QueryTestRowModel() { Id = "0", Name = "Test" });
            (bool, QueryTestRowModel) res = getitem.RetrieveItem("1");
            Assert.Equal("No item with an id of 1 could be found", res.Item2.Id);
        }

        [Fact]
        public void GetItemHandlesExceptions()
        {
            commandTestQueryHandler.inventoryItems.Add(new QueryTestRowModel() { Id = "0", Name = "Test" });
            (bool, QueryTestRowModel) res = getitem.RetrieveItem("throwsException");
            Assert.Equal("testException", res.Item2.Id);
        }

        [Fact]
        public void GetItemsRetrieveCompleteList()
        {
            List<IRowModel> rowModels = new List<IRowModel>()
            {
                new QueryTestRowModel { Id = "0" },
                new QueryTestRowModel { Id = "1" },
                new QueryTestRowModel { Id = "2" }
            };
            commandTestQueryHandler.inventoryItems = rowModels;
            (bool, List<QueryTestRowModel>) res = getitem.RetrieveItems(null, null);
            Assert.Equal(rowModels.Count, res.Item2.Count);
        }

        [Fact]
        public void GetItemsPassesFiltersOn()
        {
            List<IRowModel> rowModels = new List<IRowModel>()
            {
                new QueryTestRowModel { Id = "0", FilterValue1 = 1 },
                new QueryTestRowModel { Id = "1", FilterValue1 = 2 },
                new QueryTestRowModel { Id = "2", FilterValue1 = 3 }
            };
            commandTestQueryHandler.inventoryItems = rowModels;
            (bool, List<QueryTestRowModel>) res = getitem.RetrieveItems(new Dictionary<string, List<string>>() { {"FilterValue1", new List<string>() {" >= 2"} } }, null);
            Assert.Equal(2, res.Item2.Count);
        }

        [Fact]
        public void GetItemsRetrievesById()
        {
            List<IRowModel> rowModels = new List<IRowModel>()
            {
                new QueryTestRowModel { Id = "0", FilterValue1 = 1 },
                new QueryTestRowModel { Id = "1", FilterValue1 = 2 },
                new QueryTestRowModel { Id = "2", FilterValue1 = 3 }
            };
            commandTestQueryHandler.inventoryItems = rowModels;
            (bool, List<QueryTestRowModel>) res = getitem.RetrieveItems(null, new List<string>() {"0", "2" });
            bool containsId0 = false;
            bool containsId1 = false;
            foreach(QueryTestRowModel rowModel in res.Item2)
            {
                if(rowModel.Id == "0")
                {
                    containsId0 = true;
                }
                if(rowModel.Id == "2")
                {
                    containsId1 = true;
                }
            }
            Assert.True(containsId0 && containsId1 && res.Item2.Count == 2);
        }

        [Fact]
        public void GetItemsRetrievesNothingOnInvalidIds()
        {
            List<IRowModel> rowModels = new List<IRowModel>()
            {
                new QueryTestRowModel { Id = "0", FilterValue1 = 1 },
                new QueryTestRowModel { Id = "1", FilterValue1 = 2 },
                new QueryTestRowModel { Id = "2", FilterValue1 = 3 }
            };
            commandTestQueryHandler.inventoryItems = rowModels;
            (bool, List<QueryTestRowModel>) res = getitem.RetrieveItems(null, new List<string>() { "3", "4" });
            bool containsId0 = false;
            bool containsId1 = false;
            foreach (QueryTestRowModel rowModel in res.Item2)
            {
                if (rowModel.Id == "3")
                {
                    containsId0 = true;
                }
                if (rowModel.Id == "4")
                {
                    containsId1 = true;
                }
            }
            Assert.True(!containsId0 && !containsId1 && res.Item2.Count == 2);
        }

        [Fact]
        public void GetItemsRetrievesNothingOnNoItemsMatchingFilters()
        {
            List<IRowModel> rowModels = new List<IRowModel>()
            {
                new QueryTestRowModel { Id = "0", FilterValue1 = 1 },
                new QueryTestRowModel { Id = "1", FilterValue1 = 2 },
                new QueryTestRowModel { Id = "2", FilterValue1 = 3 }
            };
            commandTestQueryHandler.inventoryItems = rowModels;
            (bool, List<QueryTestRowModel>) res = getitem.RetrieveItems(new Dictionary<string, List<string>>() { {"FilterValue1", new List<string>() {" > 3" } } }, null);
            Assert.True(res.Item1 && res.Item2.Count == 1 && res.Item2[0].Id == "No items matching the criteria could be found");
        }

        [Fact]
        public void GetItemsHandlesQueryHandlerExceptions()
        {
            List<IRowModel> rowModels = new List<IRowModel>()
            {
                new QueryTestRowModel { Id = "0", FilterValue1 = 1 },
                new QueryTestRowModel { Id = "1", FilterValue1 = 2 },
                new QueryTestRowModel { Id = "2", FilterValue1 = 3 }
            };
            commandTestQueryHandler.inventoryItems = rowModels;
            (bool, List<QueryTestRowModel>) res = getitem.RetrieveItems(new Dictionary<string, List<string>>() { {"Id", new List<string>() {" = throwsException" } } }, null);
            Assert.True(!res.Item1 && res.Item2.Count == 1 && res.Item2[0].Id == "testException");
        }
    }

    public class UpdateItemTests
    {
        UpdateItem<QueryTestRowModel> updateItem;
        CommandTestQueryHandler commandTestQueryHandler = new CommandTestQueryHandler();
        public UpdateItemTests()
        {
            updateItem = new UpdateItem<QueryTestRowModel>(commandTestQueryHandler);
        }

        [Fact]
        public void UpdateItemUpdatesEveryValue()
        {
            QueryTestRowModel tableValues = new QueryTestRowModel { Id = "0", Name = "test", FilterValue1 = 1, FilterValue2 = 1, FilterValue3 = 1 };
            List<IRowModel> rowModels = new List<IRowModel>()
            {
                tableValues
            };
            commandTestQueryHandler.inventoryItems = rowModels;
            QueryTestRowModel updateValues = new QueryTestRowModel() { Id = "0", Name = "test1", FilterValue1 = 2, FilterValue2 = 3, FilterValue3 = 4 };
            updateItem.UpdateTableItem(updateValues);
            Assert.True(tableValues.CompareTo(updateValues));
        }

        [Fact]
        public void UpdateItemSkipsNegativeNumbers()
        {
            QueryTestRowModel tableValues = new QueryTestRowModel { Id = "0", Name = "test", FilterValue1 = 1, FilterValue2 = 1, FilterValue3 = 1 };
            List<IRowModel> rowModels = new List<IRowModel>()
            {
                tableValues
            };
            commandTestQueryHandler.inventoryItems = rowModels;
            QueryTestRowModel updateValues = new QueryTestRowModel() { Id = "0", Name = "test1", FilterValue1 = 2, FilterValue2 = -1, FilterValue3 = 4 };
            updateItem.UpdateTableItem(updateValues);
            updateValues.FilterValue2 = tableValues.FilterValue2;
            Assert.True(tableValues.CompareTo(updateValues));
        }
        [Fact]
        public void UpdateItemSkipsEmptyStrings()
        {
            QueryTestRowModel tableValues = new QueryTestRowModel { Id = "0", Name = "test", FilterValue1 = 1, FilterValue2 = 1, FilterValue3 = 1 };
            List<IRowModel> rowModels = new List<IRowModel>()
            {
                tableValues
            };
            commandTestQueryHandler.inventoryItems = rowModels;
            QueryTestRowModel updateValues = new QueryTestRowModel() { Id = "0", Name = "", FilterValue1 = 2, FilterValue2 = 3, FilterValue3 = 4 };
            updateItem.UpdateTableItem(updateValues);
            updateValues.Name = tableValues.Name;
            Assert.True(tableValues.CompareTo(updateValues));
        }

        [Fact]
        public void UpdateItemSkipsNullValues()
        {
            QueryTestRowModel tableValues = new QueryTestRowModel { Id = "0", Name = "test", FilterValue1 = 1, FilterValue2 = 1, FilterValue3 = 1 };
            List<IRowModel> rowModels = new List<IRowModel>()
            {
                tableValues
            };
            commandTestQueryHandler.inventoryItems = rowModels;
            QueryTestRowModel updateValues = new QueryTestRowModel() { Id = "0", Name = null, FilterValue1 = 2, FilterValue2 = 3, FilterValue3 = 4 };
            updateItem.UpdateTableItem(updateValues);
            updateValues.Name = tableValues.Name;
            Assert.True(tableValues.CompareTo(updateValues));
        }

        [Fact]
        public void UpdateItemThrowsAndHandlesExceptionIfNoUpdateValuesWereGiven()
        {
            QueryTestRowModel tableValues = new QueryTestRowModel { Id = "0", Name = "test", FilterValue1 = 1, FilterValue2 = 1, FilterValue3 = 1 };
            List<IRowModel> rowModels = new List<IRowModel>()
            {
                tableValues
            };
            commandTestQueryHandler.inventoryItems = rowModels;
            QueryTestRowModel updateValues = new QueryTestRowModel() { Id = "0", Name = "", FilterValue1 = -1, FilterValue2 = -1, FilterValue3 = -1 };
            (bool, string) res = updateItem.UpdateTableItem(updateValues);
            Assert.True(!res.Item1 && res.Item2 == "No update values were given");
        }
        [Fact]
        public void UpdateItemHandlesQueryHandlerExceptions()
        {
            QueryTestRowModel tableValues = new QueryTestRowModel { Id = "0", Name = "test", FilterValue1 = 1, FilterValue2 = 1, FilterValue3 = 1 };
            List<IRowModel> rowModels = new List<IRowModel>()
            {
                tableValues
            };
            commandTestQueryHandler.inventoryItems = rowModels;
            QueryTestRowModel updateValues = new QueryTestRowModel() { Id = "throwsException", Name = "test1", FilterValue1 = -1, FilterValue2 = -1, FilterValue3 = -1 };
            (bool, string) res = updateItem.UpdateTableItem(updateValues);
            Assert.True(!res.Item1 && res.Item2 == "testException");
        }
    }
}
