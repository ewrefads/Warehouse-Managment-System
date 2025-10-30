using Microsoft.AspNetCore.Mvc;
using Warehouse_Management_System.Commands;
using Warehouse_Managemet_System.Commands;
using Warehouse_Managemet_System.RowModels;
using Warehouse_Managemet_System.SQL_Executer;
using Web_Management_API.DisplayRowModels;

namespace Web_Management_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductHandlingController : Controller
    {
        private readonly ILogger<ProductHandlingController> _logger;
        private GetItem<Product> getProduct;
        private QueryHandler<Product> queryHandler;
        private GetItem<InventoryItem> getInventoryItem;
        private QueryHandler<InventoryItem> inventoryItemHandler;
        private QueryHandler<OrderItem> orderItemHandler;
        private GetItem<OrderItem> getOrderItem;
        private AddItem<Product> addProduct;
        private AddItem<InventoryItem> addInventoryItem;
        private QueryHandler<Warehouse> warehouseHandler;
        private GetItem<Warehouse> getWarehouse;
        private UpdateItem<Product> updateProduct;
        private UpdateItem<InventoryItem> updateInventoryItem;
        private UpdateItem<OrderItem> updateOrderItem;
        private DeleteItem<Product> deleteProduct;
        private DeleteItem<InventoryItem> deleteInventoryItem;
        private DeleteItem<OrderItem> deleteOrderItem;
        public ProductHandlingController(ILogger<ProductHandlingController> logger)
        {
            queryHandler = new QueryHandler<Product>("Product", new SqlExecuter());
            getProduct = new GetItem<Product>(queryHandler);
            orderItemHandler = new QueryHandler<OrderItem>("Order", new SqlExecuter());
            inventoryItemHandler = new QueryHandler<InventoryItem>("InventoryItem", new SqlExecuter());
            warehouseHandler = new QueryHandler<Warehouse>("Warehouse", new SqlExecuter());
            getInventoryItem = new GetItem<InventoryItem>(inventoryItemHandler);
            getOrderItem = new GetItem<OrderItem>(orderItemHandler);
            getWarehouse = new GetItem<Warehouse>(warehouseHandler);
            addProduct = new AddItem<Product>(queryHandler);
            addInventoryItem = new AddItem<InventoryItem>(inventoryItemHandler);
            updateProduct = new UpdateItem<Product>(queryHandler);
            updateInventoryItem = new UpdateItem<InventoryItem>(inventoryItemHandler);
            updateOrderItem = new UpdateItem<OrderItem>(orderItemHandler);
            deleteProduct = new DeleteItem<Product>(queryHandler);
            deleteInventoryItem = new DeleteItem<InventoryItem>(inventoryItemHandler);
            deleteOrderItem = new DeleteItem<OrderItem>(orderItemHandler);
            _logger = logger;
        }
        [HttpGet(Name = "GetProduct")]
        public IActionResult GetProduct(string? productId, string? Name,string? Price, bool? loadExtendedInfo)
        {
            if(productId != null)
            {
                (bool, Product) res = getProduct.RetrieveItem(productId);
                bool shouldExtendedInfoBeLoaded = false;
                if (loadExtendedInfo != null)
                {
                    shouldExtendedInfoBeLoaded = (bool)loadExtendedInfo;
                }
                if (res.Item1 && !shouldExtendedInfoBeLoaded)
                {
                    return Ok(res.Item2);
                }
                else if (res.Item1 && shouldExtendedInfoBeLoaded)
                {
                    return HandleExtendedInfo(new List<Product>() { res.Item2 });
                }
                else
                {
                    return BadRequest(res.Item2.Id);
                }
            }
            else
            {
                (bool, List<Product>) res;
                if(Price != null)
                {
                    Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>() { {"Price", Price.Split(',').ToList() } };
                    res = getProduct.RetrieveItems(filters, null);
                    
                }
                else if(Name != null)
                {
                    res = getProduct.RetrieveItems(new Dictionary<string, List<string>>() { {"Name", new List<string>() { " = " + Name } } }, null);
                }
                else
                {
                    res = getProduct.RetrieveItems(null, null);
                }
                bool shouldExtendedInfoBeLoaded = false;
                if(loadExtendedInfo != null)
                {
                    shouldExtendedInfoBeLoaded = (bool)loadExtendedInfo;
                }
                if (res.Item1 && !shouldExtendedInfoBeLoaded)
                {
                    return Ok(res.Item2);
                }
                else if(res.Item1 && shouldExtendedInfoBeLoaded)
                {
                    return HandleExtendedInfo(res.Item2);
                }
                else
                {
                    return BadRequest(res.Item2[0].Id);
                }

            }
        }

        private IActionResult HandleExtendedInfo(List<Product> items)
        {
            List<ExtendedInfoProduct> products = new List<ExtendedInfoProduct>();
            foreach(Product product in items)
            {
                List<InventoryItem> inventoryItems = getInventoryItem.RetrieveItems(new Dictionary<string, List<string>>() { {"ProductId", new List<string>() { " = " + product.Id } } }, null).Item2;
                ExtendedInfoProduct extendedInfoProduct = new ExtendedInfoProduct() { Id = product.Id, Name = product.Name, Price = product.Price, ReservedAmount = 0, StorageAmount = 0, TotalAmount = 0 };
                if (inventoryItems[0].Id != "No items matching the criteria could be found")
                {
                    foreach(InventoryItem inventoryItem in inventoryItems)
                    {
                        extendedInfoProduct.StorageAmount += inventoryItem.Amount;
                    }
                }
                List<OrderItem> orderItems = getOrderItem.RetrieveItems(new Dictionary<string, List<string>>() { { "ProductId", new List<string>() { " = " + product.Id } } }, null).Item2;
                if (orderItems[0].Id != "No items matching the criteria could be found")
                {
                    foreach (OrderItem orderItem in orderItems)
                    {
                        if(orderItem.Amount != null)
                        {
                            extendedInfoProduct.ReservedAmount += (int) orderItem.Amount;
                        }
                        
                    }
                }
                extendedInfoProduct.TotalAmount = extendedInfoProduct.StorageAmount + extendedInfoProduct.ReservedAmount;
                products.Add(extendedInfoProduct);
            }
            return Ok(products);
        }

        [HttpPost(Name = "AddProduct")]
        public IActionResult AddProduct(string Name, double Price, string? WarehouseId, int? Amount)
        {
            (bool, List<Product>) itemAllreadyExists = getProduct.RetrieveItems(new Dictionary<string, List<string>>() { {"Name", new List<string>() { " = " + Name } } },null);
            (bool, string) res;
            if (itemAllreadyExists.Item1 && itemAllreadyExists.Item2[0].Name != null && WarehouseId == null)
            {
                return BadRequest("Product Allready Exists");
            }
            else if(itemAllreadyExists.Item1 && itemAllreadyExists.Item2 != null && WarehouseId != null)
            {
                if(Amount == null)
                {
                    return BadRequest("Amount must be given for an item to be added to inventory");
                }
                Warehouse warehouse = getWarehouse.RetrieveItem((string) WarehouseId).Item2;
                InventoryItem inventoryItem = new InventoryItem() {Product = itemAllreadyExists.Item2[0], ProductId = itemAllreadyExists.Item2[0].Id, Amount = (int) Amount, WarehouseId = (string) WarehouseId, Warehouse = warehouse };
                res = addInventoryItem.AddNewItem(inventoryItem);
                
            }
            else if(!itemAllreadyExists.Item1 || itemAllreadyExists.Item2[0].Name == null)
            {
                Product product = new Product() { Name = Name, Price = Price };
                string extraInfo = "";
                if(WarehouseId != null && Amount != null)
                {
                    Warehouse warehouse = getWarehouse.RetrieveItem((string)WarehouseId).Item2;
                    if(warehouse.Id == WarehouseId)
                    {
                        InventoryItem inventoryItem = new InventoryItem() { Amount = (int)Amount, WarehouseId = (string)WarehouseId, Product = product, ProductId = product.Id, Warehouse = warehouse };
                        product.InventoryItems.Add(inventoryItem);
                        addInventoryItem.AddNewItem(inventoryItem);
                        extraInfo = " Initial Inventory was also added";
                    }
                    else
                    {
                        extraInfo = $" Initial Inventory was not added as the warehouse with an id of {WarehouseId} could not be located";

                    }

                }
                res = addProduct.AddNewItem(product);
                res.Item2 += extraInfo;
            }
            else
            {
                res = (false, "unknown error");
            }
            if(res.Item1)
            {
                return Ok(res.Item2);
            }
            else
            {
                return BadRequest(res.Item2);
            }
        }

        [HttpPut(Name = "UpdateProduct")]
        public IActionResult UpdateProduct(string? productId, string? name, double? price, string? inventoryId, int? amount, string? orderId)
        {
            (bool, string) res = (false, "Unknown error");
            if(productId != null)
            {
                Product product = new Product() { Id = productId, Name = name, Price = price };
                res = updateProduct.UpdateTableItem(product);
            }
            else if(inventoryId != null)
            {
                if (amount != null)
                {
                    InventoryItem inventoryItem = new InventoryItem()
                    {
                        Id = inventoryId,
                        Amount = (int)amount
                    };
                    res = updateInventoryItem.UpdateTableItem(inventoryItem);
                }
                else
                {
                    res.Item2 = "Amount must have a value to update an inventory Item";
                }
            }
            else if (orderId != null)
            {
                if (amount != null)
                {
                    OrderItem orderItem = new OrderItem()
                    {
                        Id = inventoryId,
                        Amount = (int)amount
                    };
                    res = updateOrderItem.UpdateTableItem(orderItem);
                }
                else
                {
                    res.Item2 = "Amount must have a value to update a order item";
                }
            }
            if (res.Item1)
            {
                return Ok(res.Item2);
            }
            else
            {
                return BadRequest(res.Item2);
            }
        }

        [HttpDelete(Name = "DeleteProduct")]
        public IActionResult DeleteProduct(string? productId, string? inventoryId, string? orderId)
        {
            (bool, string) res = (false, "unknown error");
            if(productId != null)
            {
                int totalAmount = 0;
                List<string> inventoryItemIds = new List<string>();
                List<InventoryItem> inventoryItems = getInventoryItem.RetrieveItems(new Dictionary<string, List<string>>() { { "ProductId", new List<string>() { " = " + productId } } }, null).Item2;
                foreach(InventoryItem inventoryItem in inventoryItems)
                {
                    if(inventoryItem.Amount != null && inventoryItem.Amount > 0)
                    {
                        totalAmount += inventoryItem.Amount;
                        inventoryItemIds.Add(inventoryItem.Id);
                    }
                }
                List<string> orderItemsIds = new List<string>();
                List<OrderItem> orderItems = getOrderItem.RetrieveItems(new Dictionary<string, List<string>>() { { "ProductId", new List<string>() { " = " + productId } } }, null).Item2;
                foreach (OrderItem orderItem in orderItems)
                {
                    if (orderItem.Amount != null && orderItem.Amount > 0)
                    {
                        totalAmount += (int)orderItem.Amount;
                        orderItemsIds.Add(orderItem.Id);
                    }
                }
                string message = "";
                if(totalAmount > 0)
                {
                    message = $"The product with an id of {productId} cant be deleted as there are still {totalAmount} of it left.";
                    
                    res = (false, message);
                }
                else
                {
                    foreach(InventoryItem inventoryItem in inventoryItems)
                    {
                        deleteInventoryItem.DeleteSpecificItem(inventoryItem.Id);
                    }
                    foreach(OrderItem orderItem in orderItems)
                    {
                        deleteOrderItem.DeleteSpecificItem(orderItem.Id);
                    }
                    res = deleteInventoryItem.DeleteSpecificItem(productId);
                }
            }
            else if(inventoryId != null)
            {
                res = deleteInventoryItem.DeleteSpecificItem(inventoryId);
            }
            else if(orderId != null)
            {
                res = deleteOrderItem.DeleteSpecificItem(orderId);
            }
            if(res.Item1)
            {
                return Ok(res.Item2);
            }
            else
            {
                return BadRequest(res.Item2);
            }
        }
    }
}
