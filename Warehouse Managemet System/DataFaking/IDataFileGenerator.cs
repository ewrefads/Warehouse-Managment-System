using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

/// <summary>
/// Class for storing data tables (lists of IRowModel instances) as .csv files
/// </summary>
public interface IDataFileGenerator
{
    /// <summary>
    /// Stores a given table (list of IRowModel instances) as .csv file at given file path
    /// </summary>
    /// <param name="filePath">string, path to the file to be created</param>
    /// <param name="rows">List of IRowModel instances, table to be stored as .csv file</param>
    public void GenerateDataFile(string filePath, List<IRowModel> rows);
}