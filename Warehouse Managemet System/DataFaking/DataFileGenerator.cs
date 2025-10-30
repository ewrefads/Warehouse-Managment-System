using System.IO;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

public class DataFileGenerator
{
    public void GenerateDataFile(string filePath, List<IRowModel> rows)
    {
        using(StreamWriter sw = File.CreateText(filePath))
        {
            bool columnNamesWritten = false;
            foreach (IRowModel row in rows)
            {
                if (!columnNamesWritten)
                {
                    string columnNamesLine = RowToColumnNamesString(row);
                    sw.WriteLine(columnNamesLine);
                    columnNamesWritten = true;
                }
                string rowLine = RowToString(row);
                sw.WriteLine(rowLine);
            }
        }
    }

    private string RowToString(IRowModel row)
    {
        List<string> valueStrings = row.GetAllValues();
        string rowLine = String.Join(";", valueStrings);
        return rowLine;
    }

    private string RowToColumnNamesString(IRowModel row)
    {
        List<string> valueStrings = row.GetColumnNames();
        string rowLine = String.Join(";", valueStrings);
        return rowLine;
    }
}