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
                bool returnValue = true;
                if (paramaters.ContainsKey("@selectedCollumns"))
                {
                    string[] desiredCollumns = paramaters["@selectedCollumns"].Split(", ");
                    for(int i = 0; i < desiredCollumns.Length; i++)
                    {
                        if (desiredCollumns[i] == "Name")
                        {
                            result.Columns.Add("Name", typeof(string));
                        }
                        else if(desiredCollumns[i] == "FilterValue4")
                        {
                            returnValue = false;
                        }
                        else
                        {
                            result.Columns.Add(desiredCollumns[i], typeof(int));
                        }
                    }
                }
                else
                {
                    result.Columns.Add("Id", typeof(int));
                    result.Columns.Add("Name", typeof(string));
                    result.Columns.Add("FilterValue1", typeof(int));
                    result.Columns.Add("FilterValue2", typeof(int));
                    result.Columns.Add("FilterValue3", typeof(int));
                }

                foreach (QueryTestRowModel rowModel in results)
                {
                    object[] values = new object[result.Columns.Count];
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = GetRowValue(result.Columns[i].ColumnName, rowModel);
                    }
                    result.Rows.Add(values);
                }
                if(returnValue)
                {
                    return (true, result);
                }
                else
                {
                    return (false, null);
                }
            }
            else
            {
                return (false, null);
            }
        }

        private object GetRowValue(string collumnName, QueryTestRowModel rowModel)
        {
            switch(collumnName)
            {
                case "Id":
                    return rowModel.Id;
                case "Name":
                    return rowModel.Name;
                case "FilterValue1":
                    return rowModel.FilterValue1;
                case "FilterValue2":
                    return rowModel.FilterValue2;
                case "FilterValue3":
                    return rowModel.FilterValue3;
                default:
                    return int.MinValue;
            }
        }
    }

}
