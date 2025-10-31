namespace Warehouse_Managemet_System.DataFaking;

/// <summary>
/// Class for managing the generation of fake data tables and the storing of these as .csv files
/// </summary>
public interface IDataFileManager
{
    /// <summary>
    /// Generates tables of fake data and stores these as .csv files
    /// </summary>
    /// <param name="dataGenerator">IDataGenerator implementation, for generating fake data.</param>
    /// <param name="dataFileGenerator">IDataFileGenerator implementation, for saving data to .csv files</param>
    /// <param name="destinationPath">string, path for a directory in which to save create .csv files</param>
    public void CreateDataFiles(IDataGenerator dataGenerator, IDataFileGenerator dataFileGenerator, string destinationPath);
}