using System;
using Xunit;
using Warehouse_Managemet_System;
using Warehouse_Managemet_System.Parsers;
using Warehouse_Managment_Test.Mocks.RowModels;

namespace Warehouse_Managment_Test
{
    public class ParserUnitTest
    {
        [Fact]
        public void Parse_ValidCsvFile_ReturnsCorrectListOfRows()
        {
            Parser parser = new Parser();
            string path = Path.Combine(AppContext.BaseDirectory, "CsvFilesForTesting", "ValidFileFake.csv");
            List<ValidFileMock> rowList = parser.Parse<ValidFileMock>(path);
            Assert.Equal("A1", rowList[0].Id);
        }

        [Fact]
        public void Parse_EmptyCsvFile_ReturnsEmptyListOfRows()
        {
            Parser parser = new Parser();
            string path = Path.Combine(AppContext.BaseDirectory, "CsvFilesForTesting", "EmptyFile.csv");
            List<ValidFileMock> rowList = parser.Parse<ValidFileMock>(path);
            Assert.Empty(rowList);
        }

        [Fact]
        public void Parse_InvalidFileType_ReturnsEmptyListOfRows()
        {
            Parser parser = new Parser();
            string path = Path.Combine(AppContext.BaseDirectory, "CsvFilesForTesting", "NotCsvFile.txt");
            List<ValidFileMock> rowList = parser.Parse<ValidFileMock>(path);
            Assert.Empty(rowList);
        }

        [Fact]
        public void Parse_InvariantCultureInTable_ReturnsListOfRowsWithCorrectTypes()
        {
            string fileContent = "Id,Date\nA1,2025-10-24"; 
            string filePath = Path.GetTempFileName();
            File.WriteAllText(filePath, fileContent);
            Parser parser = new Parser();
            List<ValidFileWithDateMock> rowList = parser.Parse<ValidFileWithDateMock>(filePath);
            Assert.Equal(new DateTime(2025, 10, 24), rowList[0].Date);
        }

    }
}