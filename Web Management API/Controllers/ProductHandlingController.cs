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
        public ProductHandlingController(ILogger<ProductHandlingController> logger)
        {
            queryHandler = new QueryHandler<Product>("Product", new SqlExecuter());
            getProduct = new GetItem<Product>(queryHandler);
            orderItemHandler = new QueryHandler<OrderItem>("Order", new SqlExecuter());
            inventoryItemHandler = new QueryHandler<InventoryItem>("InventoryItem", new SqlExecuter());
            getInventoryItem = new GetItem<InventoryItem>(inventoryItemHandler);
            getOrderItem = new GetItem<OrderItem>(orderItemHandler);
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
    }
}
