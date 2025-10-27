using System;
using Warehouse_Management_Test.Mocks.RowModels;
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
            Parser<ValidFileMock> parser = new Parser<ValidFileMock>();
            string path = Path.Combine(AppContext.BaseDirectory, "CsvFilesForTesting", "ValidFileFake.csv");
            List<ValidFileMock> rowList = parser.Parse(path);
            Assert.Equal("A1", rowList[0].Id);
        }

        [Fact]
        public void Parse_EmptyCsvFile_ReturnsEmptyListOfRows()
        {
            Parser<ValidFileMock> parser = new Parser<ValidFileMock>();
            string path = Path.Combine(AppContext.BaseDirectory, "CsvFilesForTesting", "EmptyFile.csv");
            List<ValidFileMock> rowList = parser.Parse(path);
            Assert.Empty(rowList);
        }

        [Fact]
        public void Parse_InvalidFileType_ReturnsEmptyListOfRows()
        {
            Parser<ValidFileMock> parser = new Parser<ValidFileMock>();
            string path = Path.Combine(AppContext.BaseDirectory, "CsvFilesForTesting", "NotCsvFile.txt");
            List<ValidFileMock> rowList = parser.Parse(path);
            Assert.Empty(rowList);
        }

        [Fact]
        public void Parse_InvariantCultureInTable_ReturnsListOfRowsWithCorrectTypes()
        {
            string fileContent = "Id,Date\nA1,2025-10-24"; 
            string filePath = Path.GetTempFileName();
            File.WriteAllText(filePath, fileContent);
            Parser<ValidFileWithDateMock> parser = new Parser<ValidFileWithDateMock>();
            List<ValidFileWithDateMock> rowList = parser.Parse(filePath);
            Assert.Equal(new DateTime(2025, 10, 24), rowList[0].Date);
        }

    }
}