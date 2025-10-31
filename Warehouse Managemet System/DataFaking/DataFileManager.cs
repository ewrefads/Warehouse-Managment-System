using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class DataFileManager : IDataFileManager
{
    int numberOfProducts = 50;
    int numberOfWarehouses = 4;
    int numberOfInventoryItems = 140;
    int numberOfTransactions = 30;
    int numberOfOrders = 20;
    int numberOfOrderItems = 30;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="dataGenerator"></param>
    /// <param name="dataFileGenerator"></param>
    /// <param name="destinationPath"></param>
    public void CreateDataFiles(IDataGenerator dataGenerator, IDataFileGenerator dataFileGenerator, string destinationPath)
    {
        Dictionary<string, List<IRowModel>> tables = GenerateTables(dataGenerator);
        GenerateTableFiles(dataFileGenerator, destinationPath, tables);
    }

    private Dictionary<string, List<IRowModel>> GenerateTables(IDataGenerator dataGenerator)
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
        Dictionary<string, List<IRowModel>> tables = new Dictionary<string, List<IRowModel>> {
            {"products", products},
            {"warehouses", warehouses},
            {"inventoryItems", inventoryItems},
            {"transactions", transactions},
            {"orders", orders},
            {"orderItems", orderItems}
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

    private void GenerateTableFiles(IDataFileGenerator dataFileGenerator, string destinationPath, Dictionary<string, List<IRowModel>> tables)
    {
        foreach (KeyValuePair<string, List<IRowModel>> table in tables)
        {
            string filePath = destinationPath + "\\" + table.Key + ".csv";
            dataFileGenerator.GenerateDataFile(filePath, table.Value);
        }
    }
}