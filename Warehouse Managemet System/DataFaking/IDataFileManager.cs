namespace Warehouse_Managemet_System.DataFaking;

public interface IDataFileManager
{
    public void CreateDataFiles(IDataGenerator dataGenerator, IDataFileGenerator dataFileGenerator, string destinationPath);
}