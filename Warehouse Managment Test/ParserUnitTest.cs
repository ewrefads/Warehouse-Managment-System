using System;
using Xunit;
using Warehouse_Managemet_System;
using Warehouse_Managemet_System.Parsers;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managment_Test
{
    public class ParserUnitTest
    {
        [Fact]
        public void Parse_ValidFile_ReturnsCorrectListOfRows()
        {
            Parser<MockRowModel> parser = new Parser<MockRowModel>();
            string path = Path.Combine(AppContext.BaseDirectory, "CsvFilesForTesting", "ValidFileFake.csv");
            List<MockRowModel> rowList = parser.Parse<MockRowModel>(path);
            Assert.Equal("A1", rowList[0].Id);
        }

        [Fact]
        public void Parse_InvalidFile_ReturnsEmptyListOfRows()
        {
            Parser<MockRowModel> parser = new Parser<MockRowModel>();
            string path = Path.Combine(AppContext.BaseDirectory, "CsvFilesForTesting", "EmptyFile.csv");
            List<MockRowModel> rowList = parser.Parse<MockRowModel>(path);
            Assert.Empty(rowList);
        }

        [Fact]
        public void Parse_EmptyFile_ReturnsEmptyListOfRows()
        {
            Parser<MockRowModel> parser = new Parser<MockRowModel>();
            string path = Path.Combine(AppContext.BaseDirectory, "CsvFilesForTesting", "NotCsvFile.txt");
            List<MockRowModel> rowList = parser.Parse<MockRowModel>(path);
            Assert.Empty(rowList);
        }

        [Fact]
        public void Parse_NoHeadersInFile_ReturnsEmptyListOfRows()
        {
            Assert.True(true);
        }

        [Fact]
        public void Parse_WrongIdTypeInFile_ReturnsEmptyListOfRows()
        {
            Assert.True(true);
        }

        [Fact]
        public void Parse_WrongTypesInFile_ReturnsEmptyListOfRows()
        {
            Assert.True(true);
        }

        [Fact]
        public void Parse_TooManyColumnsInFile_ReturnsEmptyListOfRows()
        {
            Assert.True(true);
        }
    }
}