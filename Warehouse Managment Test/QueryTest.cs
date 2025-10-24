using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.Commands;
using Warehouse_Managemet_System.Contexts;
using Warehouse_Managemet_System.Table_Models;
using Warehouse_Managment_Test.Mocks;
using Warehouse_Managment_Test.Mocks.External_Systems_mocks;
using Warehouse_Managment_Test.Mocks.QueryHandlers;
using Warehouse_Managment_Test.Mocks.RowModels;

namespace Warehouse_Managment_Test
{
    public class QueryTest
    {
        private IContext context;
        private QueryHandler handler;
        private TestSqlExecuter sqlExecuter;
        private MySqlConnection connection;
        private List<QueryTestRowModel> data;
        public QueryTest() 
        {
            context = new QueryTestContext();
            context.CreateTable(new ModelBuilder());
            List<QueryTestRowModel> defaultTestData = new List<QueryTestRowModel>()
            {
                new QueryTestRowModel("0", "test0", 3, 5, 1),
                new QueryTestRowModel("1", "test1", 5, 2, 7),
                new QueryTestRowModel("2", "test2", 4, 3, 2)
            };
            sqlExecuter = new TestSqlExecuter(defaultTestData);
            data = defaultTestData;
            handler = new QueryHandler(context, sqlExecuter);
        }
        
        [Fact]
        public void QueryHandlerReturnsCompleteTable()
        {
            List<QueryTestRowModel> result = handler.SelectFromTable<QueryTestRowModel>(new Dictionary<string, List<string>>(), new List<string>());
            Assert.Equal(data.Count, result.Count);
        }

        [Fact]
        public void QueryHandlerSelectReturnsOnlyDesiredCollumn()
        {
            List<string> collumn = ["Name"];
            List<QueryTestRowModel> result = handler.SelectFromTable<QueryTestRowModel>(new Dictionary<string, List<string>>(), collumn);
            bool onlyNameWasRetrieved = true;
            int extraCollumnValues = 0;
            foreach(QueryTestRowModel rowModel in result)
            {
                if(rowModel.Id != "" || rowModel.FilterValue1 != -1 || rowModel.FilterValue2 != -1 || rowModel.FilterValue3 != -1)
                {
                    extraCollumnValues++;
                }
            }
            Assert.Equal(0, extraCollumnValues);
        }

        [Fact]
        public void QueryHandlerSelectReturnsOnlyDesiredCollumns()
        {
            List<string> collumns = new List<string>()
            {
                "Name",
                "Id"
            };
            List<QueryTestRowModel> result = handler.SelectFromTable<QueryTestRowModel>(new Dictionary<string, List<string>>(), collumns);
            int extraCollumnValues = 0;
            foreach (QueryTestRowModel rowModel in result)
            {
                if (rowModel.FilterValue1 != -1 || rowModel.FilterValue2 != -1 || rowModel.FilterValue3 != -1)
                {
                    extraCollumnValues++;
                }
            }
            Assert.Equal(0, extraCollumnValues);
        }

        [Fact]
        public void QueryHandlerThrowsErrorOnSQLExceptions()
        {
            List<string> collumns = new List<string>()
            {
                "Name",
                "FilterValue4"
            };
            Assert.ThrowsAny<Exception>(() => handler.SelectFromTable<QueryTestRowModel>(new Dictionary<string, List<string>>(), collumns));
        }

        [Fact]
        public void QueryHandlerExceptionHasCorrectMessageOnSQLExceptions()
        {
            List<string> collumns = new List<string>()
            {
                "Name",
                "FilterValue4"
            };
            var Exception = Record.Exception(() => handler.SelectFromTable<QueryTestRowModel>(new Dictionary<string, List<string>>(), collumns));
            Assert.Equal("Sql query failed", Exception.Message);
        }

        [Theory()]
        [InlineData("= 3", 1)]
        [InlineData("<= 4", 2)]
        [InlineData("< 4", 1)]
        [InlineData(">= 4", 2)]
        [InlineData("> 4", 1)]
        [InlineData("<> 4", 2)]
        public void QueryHandlerFiltersOnOneParamater(string filter, int returnedRowModels)
        {
            Dictionary<string, List<string>> filterDictionary = new Dictionary<string, List<string>>
            {
                { "FilterValue1", new List<string>(){ filter} }
            };
            List<QueryTestRowModel> result = handler.SelectFromTable<QueryTestRowModel>(filterDictionary, new List<string>());
            Assert.Equal(returnedRowModels, result.Count);
        }

        [Fact]
        public void QueryHandlerFiltersByMultipleParamatersOnSingleCollumn()
        {
            List<string> filters = new List<string>()
            {
                "> 3",
                "< 5"
            };
            Dictionary<string, List<string>> filterDictionary = new Dictionary<string, List<string>>
            {
                { "FilterValue1", filters }
            };
            List<QueryTestRowModel> result = handler.SelectFromTable<QueryTestRowModel>(filterDictionary, new List<string>());
            Assert.Single(result);
        }

        [Fact]
        public void QueryHandlerFiltersByParamatersOnMultipleCollumns()
        {
            Dictionary<string, List<string>> filterDictionary = new Dictionary<string, List<string>>
            {
                { "FilterValue1", new List<string>(){"> 3"} },
                { "FilterValue2", new List<string>(){"= 2" } }
            };
            List<QueryTestRowModel> result = handler.SelectFromTable<QueryTestRowModel>(filterDictionary, new List<string>());
            Assert.Single(result);
        }

        [Fact]
        public void QueryHandlerDeletesEntireTableIfNoArgumentsAreGiven()
        {
            handler.DeleteFromTable<QueryTestRowModel>(new Dictionary<string, List<string>>());
            Assert.Empty(sqlExecuter.results);
        }

        [Fact]
        public void QueryHandlerDeletesPartiallyBasedOnSingleFilter()
        {
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>
            {
                { "FilterValue1", new List<string>(){"> 3"} }
            };
            handler.DeleteFromTable<QueryTestRowModel>(filters);
            Assert.Single(sqlExecuter.results);
        }

        [Fact]
        public void QueryHandlerDeletesPartiallyBasedOnMultipleFilters()
        {
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>
            {
                { "FilterValue1", new List<string>(){"> 3"} },
                { "FilterValue2", new List<string>(){"= 2" } }

            };
            handler.DeleteFromTable<QueryTestRowModel>(filters);
            Assert.Equal(2, sqlExecuter.results.Count);
        }

        [Fact]
        public void QueryHandlerThrowsExceptionWithDelete()
        {
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>
            {
                { "FilterValue4", new List<string>(){"> 3"} }
            };
            Assert.ThrowsAny<Exception>(() => handler.DeleteFromTable<QueryTestRowModel>(filters));
        }

        [Fact]
        public void QueryHandlerUpdatesAllWithNoConditions()
        {
            Dictionary<string, string> updateValues = new Dictionary<string, string>()
            {
                {"FilterValue1", "1" }
            };
            handler.UpdateTable<QueryTestRowModel>(new Dictionary<string, List<string>>(), updateValues);
            int updatedRows = 0;
            foreach (QueryTestRowModel rowModel in sqlExecuter.results)
            {
                if (rowModel.FilterValue1 == 1)
                {
                    updatedRows++;
                }
            }
            Assert.Equal(3, updatedRows);
        }

        [Fact]
        public void QueryHandlerUpdatesString()
        {
            Dictionary<string, string> updateValues = new Dictionary<string, string>()
            {
                {"Name", "test" }
            };
            handler.UpdateTable<QueryTestRowModel>(new Dictionary<string, List<string>>(), updateValues);
            int updatedRows = 0;
            foreach (QueryTestRowModel rowModel in sqlExecuter.results)
            {
                if (rowModel.Name == "test")
                {
                    updatedRows++;
                }
            }
            Assert.Equal(3, updatedRows);
        }

        [Fact]
        public void QueryHandlerUpdatesPartiallyBasedOnFilter()
        {
            Dictionary<string, string> updateValues = new Dictionary<string, string>()
            {
                {"FilterValue1", "1" }
            };
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>
            {
                { "FilterValue1", new List<string>(){"> 3"} }
            };
            handler.UpdateTable<QueryTestRowModel>(filters, updateValues);
            int updatedRows = 0;
            foreach (QueryTestRowModel rowModel in sqlExecuter.results)
            {
                if (rowModel.FilterValue1 == 1)
                {
                    updatedRows++;
                }
            }
            Assert.Equal(2, updatedRows);
        }

        [Fact]
        public void QueryHandlerThrowsExceptionOnUnknownCollumnName()
        {
            Dictionary<string, string> updateValues = new Dictionary<string, string>()
            {
                {"FilterValue4", "1" }
            };
            var exception = Record.Exception(() => handler.UpdateTable<QueryTestRowModel>(new Dictionary<string, List<string>>(), updateValues));
            Assert.Equal("Sql query failed", exception.Message);
        }
    }
}
