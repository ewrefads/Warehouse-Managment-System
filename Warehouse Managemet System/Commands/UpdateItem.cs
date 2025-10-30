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
        /*public (bool, string) UpdateOrderStatus(OrderStatus newStatus, string orderId)
        {
            try
            {
                if (typeof(RowModel) != typeof(Order))
                {
                    throw new Exception("This method only works on orders");
                }
                GetItem<Order> getOrder = new(queryHandlers[0]);
                GetItem<Transaction> getTransaction = new(queryHandlers[0]);
                Order order = getOrder.RetrieveItem(orderId).Item2;
                if (order.Id != orderId)
                {
                    throw new Exception("Order could not be found");
                }
                List<Transaction> transactions = getTransaction.RetrieveItems(new Dictionary<string, List<string>>() { {"OrderId", new List<string>() { " = " + orderId} } }, null).Item2;
                List<string> uncompletedTransactions = new List<string>();
                if(newStatus == OrderStatus.Processed)
                {
                   
                    foreach (Transaction transaction in transactions)
                    {
                        if(transaction.Status == TransactionStatus.Waiting || transaction.Status == TransactionStatus.Active)
                        {
                            uncompletedTransactions.Add(transaction.Id);
                        }
                    }
                    if(uncompletedTransactions.Count > 0)
                    {
                        string message = "The following transactions should be marked as done or aborted before the order can be marked as proccessed: ";
                        string transactionIdList = "";

                        throw new Exception(message + transactionIdList);
                    }
                }
                order.Status = newStatus;
                UpdateItem<Order> updateOrder = new(queryHandlers[0]); 
                return updateOrder.UpdateTableItem(new Order() {Id = orderId, Status = newStatus });
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }*/
    }
}
