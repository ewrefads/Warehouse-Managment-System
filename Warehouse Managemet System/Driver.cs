using System;
using Microsoft.EntityFrameworkCore;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.RowModels;
using Warehouse_Managemet_System.Parsers;
using Warehouse_Managemet_System.Seeders;
using Warehouse_Managemet_System.SQL_Executer;
using Warehouse_Managemet_System.Commands;
using Warehouse_Management_System.Commands;

namespace Warehouse_Managemet_System.Driver
{
    public class Driver
    {
        private Context _context;
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

        public Driver(WebApplicationBuilder builder)
        {
            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<Context>(options =>
                options.UseMySql(
                    connectionString,
                    new MySqlServerVersion(new Version(8, 0, 30))
                ));

            _app = builder.Build();
        }

        public IServiceScope GetScope()
        {
            return _app.Services.CreateScope();
        }

        public void SetUpDatabase(IServiceScope scope)
        {
            _context = scope.ServiceProvider.GetRequiredService<Context>();
            Parser parser = new Warehouse_Managemet_System.Parsers.Parser();
            Seeder seeder = new Seeder(_context, parser);
            seeder.PopulateTable<Product>("");
            seeder.PopulateTable<OrderItem>("");
            seeder.PopulateTable<Order>("");
            seeder.PopulateTable<Transaction>("");
            seeder.PopulateTable<InventoryItem>("");
            seeder.PopulateTable<Warehouse>("");
            SqlExecuter executer = new SqlExecuter();
            string productTable = _context.GetTable<Product>();
            string orderItemTable = _context.GetTable<OrderItem>();
            string orderTable = _context.GetTable<Order>();
            string transactionTable = _context.GetTable<Transaction>();
            string inventoryItemTable = _context.GetTable<InventoryItem>();
            string warehouseTable = _context.GetTable<Warehouse>();
            QueryHandler<Product> productHandler = new QueryHandler<Product>(productTable, executer);
            QueryHandler<OrderItem> orderItemHandler = new QueryHandler<OrderItem>(orderItemTable, executer);
            QueryHandler<Order> orderHandler = new QueryHandler<Order>(orderTable, executer);
            QueryHandler<Transaction> transactionHandler = new QueryHandler<Transaction>(transactionTable, executer);
            QueryHandler<InventoryItem> inventoryItemHandler = new QueryHandler<InventoryItem>(inventoryItemTable, executer);
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