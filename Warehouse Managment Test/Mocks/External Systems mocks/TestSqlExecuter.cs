using Moq;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Managemet_System.SQL_Executer;
using Warehouse_Managment_Test.Mocks.RowModels;

namespace Warehouse_Managment_Test.Mocks.External_Systems_mocks
{
    public class TestSqlExecuter : ISQLExecuter
    {
        public List<QueryTestRowModel> results;

        public TestSqlExecuter(List<QueryTestRowModel> results)
        {
            this.results = results;
        }

        public string ExecuteNonReturningQuery(string command, MySqlConnection connection, Dictionary<string, string> paramaters)
        {
            throw new NotImplementedException();
        }

        public (bool, DataTable?) ExecuteQuery(string command, MySqlConnection connection, Dictionary<string, string> paramaters)
        {
            if(command.Contains("SELECT") && command.Contains("testTable"))
            {
                DataTable result = new DataTable();
                result.Columns.Add("Id", typeof(int));
                result.Columns.Add("Name", typeof(string));
                result.Columns.Add("FilterValue1", typeof(int));
                result.Columns.Add("FilterValue2", typeof(int));
                result.Columns.Add("FilterValue3", typeof(int));
                bool selectedCollumns = false;
                if(paramaters.ContainsKey("@selectedCollumns"))
                {
                    selectedCollumns = true;
                }
                foreach(QueryTestRowModel rowModel in results)
                {
                    object[] values = new object[5];
                    values[0] = GetIntRowValue(rowModel.Id, paramaters, "@selectedCollumns", "Id");
                    values[1] = rowModel.Name;
                    values[2] = GetIntRowValue(rowModel.FilterValue1, paramaters, "@selectedCollumns", "FilterValue1");
                    values[3] = GetIntRowValue(rowModel.FilterValue2, paramaters, "@selectedCollumns", "FilterValue2");
                    values[4] = GetIntRowValue(rowModel.FilterValue3, paramaters, "@selectedCollumns", "FilterValue3");
                    result.Rows.Add(values);
                }
                return (true, result);
            }
            else
            {
                return (false, null);
            }
        }

        private int GetIntRowValue(int value, Dictionary<string, string> paramaters, string paramater, string field)
        {
            if(paramaters.ContainsKey(paramater) && !paramaters[paramater].Contains(field))
            {
                return -1;
            }
            else
            {
                return value;
            }
        }
    }

}
