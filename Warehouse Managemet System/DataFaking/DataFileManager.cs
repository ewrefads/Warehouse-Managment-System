using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

public class DataFileManager
{
    int numberOfProducts = 50;
    int numberOfWarehouses = 4;
    int numberOfInventoryItems = 140;
    int numberOfTransactions = 30;
    int numberOfOrders = 20;
    int numberOfOrderItems = 30;

    public void CreateDataFiles(IDataGenerator dataGenerator, IDataFileGenerator dataFileGenerator, string destinationPath)
    {
        List<List<IRowModel>> tables = GenerateTables(dataGenerator);
        GenerateTableFiles(dataFileGenerator, destinationPath, tables);
    }

    private List<List<IRowModel>> GenerateTables(IDataGenerator dataGenerator)
    {
        //Generate product and warehouse data
        IRowGenerator productGenerator = new ProductGenerator();
        IRowGenerator warehouseGenerator = new WarehouseGenerator();
        List<IRowModel> products = dataGenerator.GenerateRows(productGenerator, numberOfProducts);
        List<IRowModel> warehouses = dataGenerator.GenerateRows(warehouseGenerator, numberOfWarehouses);
        //Generate inventoryItem and transaction data
        List<string> productIds = RowsToIds(products);
        List<string> warehouseIds = RowsToIds(warehouses);
        IRowGenerator inventoryItemGenerator = new InventoryItemGenerator(productIds, warehouseIds);
        IRowGenerator transactionGenerator = new TransactionGenerator(productIds, warehouseIds);
        List<IRowModel> inventoryItems = dataGenerator.GenerateRows(inventoryItemGenerator, numberOfInventoryItems);
        List<IRowModel> transactions = dataGenerator.GenerateRows(transactionGenerator, numberOfTransactions);
        //Generate order data
        List<string> transactionIds = RowsToIds(transactions);
        IRowGenerator orderGenerator = new OrderGenerator(transactionIds);
        List<IRowModel> orders = dataGenerator.GenerateRows(orderGenerator, numberOfOrders);
        //Generate orderItem data
        List<string> orderIds = RowsToIds(orders);
        IRowGenerator orderItemGenerator = new OrderItemGenerator(productIds, orderIds);
        List<IRowModel> orderItems = dataGenerator.GenerateRows(orderItemGenerator, numberOfOrderItems);
        //Return all lists of row data
        List<List<IRowModel>> tables = new List<List<IRowModel>> {
            products,
            warehouses,
            inventoryItems,
            transactions,
            orders,
            orderItems
        };
        return tables;
    }

    private List<string> RowsToIds(List<IRowModel> rows)
    {
        Func<IRowModel, string> rowToId = r => r.Id;
        Converter<IRowModel, string> converter = new Converter<IRowModel, string>(rowToId);
        List<string> ids = rows.ConvertAll(converter);
        return ids;
    }

    private void GenerateTableFiles(IDataFileGenerator dataFileGenerator, string destinationPath, List<List<IRowModel>> tables)
    {
        foreach (List<IRowModel> table in tables)
        {
            dataFileGenerator.GenerateDataFile(destinationPath, table);
        }
    }
}