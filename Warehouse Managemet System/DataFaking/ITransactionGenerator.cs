using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managemet_System.DataFaking;

public interface ITransactionGenerator
{
    public Transaction GenerateTransaction(List<Product> products, List<Warehouse> warehouses, int? seed);
    public List<Transaction> GenerateTransactions(int amount, List<Product> products, List<Warehouse> warehouses, int? seed);
}