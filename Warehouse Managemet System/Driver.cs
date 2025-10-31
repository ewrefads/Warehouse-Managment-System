using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Warehouse_Management_System.ClassServices;
using Warehouse_Management_System.Commands;
using Warehouse_Managemet_System.Commands;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.DataFaking;
using Warehouse_Managemet_System.Parsers;
using Warehouse_Managemet_System.RowModels;
using Warehouse_Managemet_System.Seeders;
using Warehouse_Managemet_System.SQL_Executer;

namespace Warehouse_Managemet_System.Driver
{
    public class Driver
    {
        public WebApplication _app;
        public GetItem<Product> _getProdcuct;
        public GetItem<OrderItem> _getOrderItem;
        public GetItem<Order> _getOrder;
        public GetItem<Transaction> _getTransaction;
        public GetItem<InventoryItem> _getInventoryItem;
        public GetItem<Warehouse> _getWarehouse;
        public AddItem<Product> _addProduct;
        public AddItem<OrderItem> _addOrderItem;
        public AddItem<Order> _addOrder;
        public AddItem<Transaction> _addTransaction;
        public AddItem<InventoryItem> _addInventoryItem;
        public AddItem<Warehouse> _addWarehouse;
        public UpdateItem<Product> _updateProduct;
        public UpdateItem<OrderItem> _updateOrderItem;
        public UpdateItem<Order> _updateOrder;
        public UpdateItem<Transaction> _updateTransaction;
        public UpdateItem<InventoryItem> _updateInventoryItem;
        public UpdateItem<Warehouse> _updateWarehouse;
        public DeleteItem<Product> _deleteProduct;
        public DeleteItem<OrderItem> _deleteOrderItem;
        public DeleteItem<Order> _deleteOrder;
        public DeleteItem<Transaction> _deleteTransaction;
        public DeleteItem<InventoryItem> _deleteInventoryItem;
        public DeleteItem<Warehouse> _deleteWarehouse;
        private string connectionString;

        public Driver(WebApplicationBuilder builder)
        {
            connectionString = builder.Configuration.GetConnectionString("DBConnection");
        }

        public IServiceScope GetScope()
        {
            return _app.Services.CreateScope();
        }

        public void SetUpDatabase(Context context)
        {
            SqlExecuter executer = new SqlExecuter();
            string productTable = context.GetTable<Product>();
            string orderItemTable = context.GetTable<OrderItem>();
            string orderTable = context.GetTable<Order>();
            string transactionTable = context.GetTable<Transaction>();
            string inventoryItemTable = context.GetTable<InventoryItem>();
            string warehouseTable = context.GetTable<Warehouse>();
            QueryHandler<Product> productHandler = new QueryHandler<Product>(productTable, executer);
            //productHandler.connectionString = connectionString;
            QueryHandler<OrderItem> orderItemHandler = new QueryHandler<OrderItem>(orderItemTable, executer);
            //orderItemHandler.connectionString = connectionString;
            QueryHandler<Order> orderHandler = new QueryHandler<Order>(orderTable, executer);
            //orderHandler.connectionString = connectionString;
            QueryHandler<Transaction> transactionHandler = new QueryHandler<Transaction>(transactionTable, executer);
            //transactionHandler.connectionString = connectionString;
            QueryHandler<InventoryItem> inventoryItemHandler = new QueryHandler<InventoryItem>(inventoryItemTable, executer);
            //inventoryItemHandler.connectionString = connectionString;
            QueryHandler<Warehouse> warehouseHandler = new QueryHandler<Warehouse>(warehouseTable, executer);

            _getProdcuct = new GetItem<Product>(productHandler);
            _getOrderItem = new GetItem<OrderItem>(orderItemHandler);
            _getOrder = new GetItem<Order>(orderHandler);
            _getTransaction = new GetItem<Transaction>(transactionHandler);
            _getInventoryItem = new GetItem<InventoryItem>(inventoryItemHandler);
            _getWarehouse = new GetItem<Warehouse>(warehouseHandler);
            _addProduct = new AddItem<Product>(productHandler);
            _addOrderItem = new AddItem<OrderItem>(orderItemHandler);
            _addOrder = new AddItem<Order>(orderHandler);
            _addTransaction = new AddItem<Transaction>(transactionHandler);
            _addInventoryItem = new AddItem<InventoryItem>(inventoryItemHandler);
            _addWarehouse = new AddItem<Warehouse>(warehouseHandler);
            _updateProduct = new UpdateItem<Product>(productHandler);
            _updateOrderItem = new UpdateItem<OrderItem>(orderItemHandler);
            _updateOrder = new UpdateItem<Order>(orderHandler);
            _updateTransaction = new UpdateItem<Transaction>(transactionHandler);
            _updateInventoryItem = new UpdateItem<InventoryItem>(inventoryItemHandler);
            _updateWarehouse = new UpdateItem<Warehouse>(warehouseHandler);
            _deleteProduct = new DeleteItem<Product>(productHandler);
            _deleteOrderItem = new DeleteItem<OrderItem>(orderItemHandler);
            _deleteOrder = new DeleteItem<Order>(orderHandler);
            _deleteTransaction  = new DeleteItem<Transaction>(transactionHandler);
            _deleteInventoryItem = new DeleteItem<InventoryItem>(inventoryItemHandler);
            _deleteWarehouse = new DeleteItem<Warehouse>(warehouseHandler);
        }
    
    }
}