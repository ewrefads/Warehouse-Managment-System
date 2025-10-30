using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Management_System.QueryHandlers;
using Warehouse_Managemet_System.Commands;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Management_System.Commands
{
    public class UpdateItem<RowModel>:Command where RowModel : class, IRowModel, new()
    {
        public UpdateItem(IQueryHandler queryHandler)
        {
            queryHandlers.Add(queryHandler);
        }

        public (bool, string) UpdateTableItem(RowModel updateValues)
        {
            try
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                var bindingFlags = BindingFlags.Instance |
                                   BindingFlags.NonPublic |
                                   BindingFlags.Public;
                var fieldNames = typeof(RowModel).GetFields(bindingFlags)
                                                 .Select(field => field.Name)
                                                 .ToList();

                var fieldValues = updateValues.GetType()
                                              .GetFields(bindingFlags)
                                              .Select(field => field.GetValue(updateValues))
                                              .ToList();
                for(int i = 0; i < fieldNames.Count; i++)
                {
                    if (fieldValues[i] == null)
                    {
                        continue;
                    }
                    else if ((fieldValues[i].GetType() == typeof(int) || fieldValues[i].GetType() == typeof(double)))
                    {
                        double numValue = double.Parse(fieldValues[i].ToString());
                        if(numValue > 0)
                        {
                            values.Add(fieldNames[i].Split('>')[0].Remove(0, 1), fieldValues[i].ToString());
                        }
                    }
                    else if (fieldValues[i].ToString().Length > 0 && fieldNames[i].Split('>')[0].Remove(0, 1) != "Id")
                    {
                        values.Add(fieldNames[i].Split('>')[0].Remove(0, 1), fieldValues[i].ToString());
                    }
                }
                if(values.Count == 0)
                {
                    throw new Exception("No update values were given");
                }
                return queryHandlers[0].UpdateTable<RowModel>(new Dictionary<string, List<string>>() { { "Id", new List<string>() { " = " + updateValues.Id } } }, values);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
    }
}
