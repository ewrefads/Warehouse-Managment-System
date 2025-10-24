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
                new QueryTestRowModel(0, "test0", 3, 5, 1),
                new QueryTestRowModel(1, "test1", 5, 2, 7),
                new QueryTestRowModel(2, "test2", 4, 5, 2)
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
                if(rowModel.Id != -1 || rowModel.FilterValue1 != -1 || rowModel.FilterValue2 != -1 || rowModel.FilterValue3 != -1)
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
            bool onlyNameWasRetrieved = true;
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
    }
}
