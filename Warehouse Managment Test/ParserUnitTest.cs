using System;
using Xunit;
using Warehouse_Managemet_System;
using Warehouse_Managemet_System.Parsers;
using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managment_Test
{
    public class ParserUnitTest
    {
        [Fact]
        public void Parse_ValidFile_ReturnsCorrectListOfRows()
        {
            Parser parser = new Parser();
            string path = Path.Combine(AppContext.BaseDirectory, "CsvFilesForTesting", "ValidFileFake.csv");
            List<MockRowModel> rowList = parser.Parse(path);
            Assert.Equal("A1", rowList[0].Id);
        }

        [Fact]
        public void Parse_InvalidFile_ReturnsEmptyListOfRows()
        {
            Parser parser = new Parser();
            string path = Path.Combine(AppContext.BaseDirectory, "CsvFilesForTesting", "EmptyFile.csv");
            List<MockRowModel> rowList = parser.Parse(path);
            Assert.Empty(rowList);
        }

        [Fact]
        public void Parse_EmptyFile_ReturnsEmptyListOfRows()
        {
            Parser parser = new Parser();
            string path = Path.Combine(AppContext.BaseDirectory, "CsvFilesForTesting", "NotCsvFile.txt");
            List<MockRowModel> rowList = parser.Parse(path);
            Assert.Empty(rowList);
        }
    }
}