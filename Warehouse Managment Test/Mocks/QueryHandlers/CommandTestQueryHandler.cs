using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Management_System.QueryHandlers;
using Warehouse_Management_Test.Mocks.RowModels;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Management_Test.Mocks.QueryHandlers
{
    public class CommandTestQueryHandler : IQueryHandler
    {
        public List<IRowModel> inventoryItems = new List<IRowModel>();
        public (bool, string) DeleteFromTable<RowModel>(Dictionary<string, List<string>> filters) where RowModel : IRowModel
        {
            try
            {
                if (filters.ContainsKey("Id") && filters["Id"][0].Contains("throwsException"))
                {
                    throw new Exception("exception in queryHandler");
                }
                List<IRowModel> itemsToBeDeleted = new List<IRowModel>();
                foreach (QueryTestRowModel rowModel in inventoryItems)
                {
                    bool shouldBeDeleted = true;
                    foreach (string identifier in filters.Keys)
                    {
                        foreach (string condition in filters[identifier])
                        {
                            if (!IsConditionMet(rowModel, identifier, condition))
                            {
                                shouldBeDeleted = false;
                                break;
                            }
                        }
                        if (!shouldBeDeleted)
                        {
                            break;
                        }
                    }
                    if (shouldBeDeleted)
                    {
                        itemsToBeDeleted.Add(rowModel);
                    }
                }
                foreach (IRowModel model in itemsToBeDeleted)
                {
                    inventoryItems.Remove(model);
                }
                return (true, "deleted " + itemsToBeDeleted.Count + " items");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool IsConditionMet(QueryTestRowModel rowModel, string identifier, string condition)
        {
            string stringValue = "";
            int intValue = -1;
            switch(identifier)
            {
                case "Id":
                    stringValue = rowModel.Id;
                    break;
                case "Name":
                    stringValue = rowModel.Name;
                    break;
                case "FilterValue1":
                    intValue = rowModel.FilterValue1;
                    break;
                case "FilterValue2":
                    intValue = rowModel.FilterValue2;
                    break;
                case "FilterValue3":
                    intValue = rowModel.FilterValue3;
                    break;
            }
            string[] conditionParts = condition.Split(' ');
            if(stringValue.Length == 0)
            {
                int intCondition = int.Parse(conditionParts[2]);
                switch (conditionParts[1])
                {
                    case "=": return intValue == intCondition;
                    case "<>": return intValue != intCondition;
                    case ">": return intValue > intCondition;
                    case ">=": return intValue >= intCondition;
                    case "<": return intValue < intCondition;
                    case "<=": return intValue <= intCondition;
                    default: throw new Exception($"Unknown operator: {conditionParts[1]}");
                }
            }
            else
            {
                switch (conditionParts[1])
                {
                    case "=": return stringValue == conditionParts[2];
                    case "<>": return stringValue != conditionParts[2];
                    default: throw new Exception($"Unknown operator: {conditionParts[1]}");
                }
            }
            
        }

        public (bool, string) InsertIntoTable<RowModel>(List<RowModel> itemsToBeInserted) where RowModel : IRowModel, new()
        {
            foreach (var item in itemsToBeInserted)
            {
                inventoryItems.Add(item);
            }
            return (true, "sucess");
        }

        public List<RowModel> SelectFromTable<RowModel>(Dictionary<string, List<string>> filters, List<string> desiredCollumns) where RowModel : IRowModel, new()
        {
            List<RowModel> returnList = new List<RowModel>();
            foreach (IRowModel item in inventoryItems)
            {
                
                if(filters.Count > 0)
                {
                    if (filters.ContainsKey("Id") && filters["Id"][0].Contains(item.Id))
                    {
                        returnList.Add((RowModel)item);
                    }
                }
                else
                {
                    returnList.Add((RowModel)item);
                }
            }
            return returnList;
        }

        public (bool, string) UpdateTable<RowModel>(Dictionary<string, List<string>> filters, Dictionary<string, string> updateValues) where RowModel : IRowModel
        {
            if(typeof(RowModel) == typeof(InventoryItem))
            {
                InventoryItem item = (InventoryItem)inventoryItems[0];
                item.Amount = int.Parse(updateValues["Amount"]);
                return (true, "table succesfully updated");
            }
            else if(typeof(RowModel) == typeof(Order))
            {
                Order order = (Order)inventoryItems[0];
                OrderStatus orderStatus;
                if (Enum.TryParse(updateValues["Status"], out orderStatus))
                {
                    order.Status = orderStatus;
                }
                return (true, "table succesfully updated");
            }
            else if (typeof(RowModel) == typeof(Transaction))
            {
                Order order = (Order)inventoryItems[0];
                List<Transaction> transactions = order.Transactions.ToList();
                foreach(Transaction transaction in transactions)
                {
                    if (filters["Id"][0].Contains(transaction.Id))
                    {
                        TransactionStatus orderStatus;
                        if (Enum.TryParse(updateValues["Status"], out orderStatus))
                        {
                            transaction.Status = orderStatus;
                        }
                    }
                }
                return (true, "table succesfully updated");
            }
            else
            {
                return (false, "not implemented yet");
            }
        }
    }
}
