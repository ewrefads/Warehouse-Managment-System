using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

public interface IDataFileGenerator
{
    public void GenerateDataFile(string filePath, List<IRowModel> rows);
}