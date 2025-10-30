using Microsoft.AspNetCore.Mvc;
using Warehouse_Management_System.Commands;
using Warehouse_Managemet_System.Commands;
using Warehouse_Managemet_System.RowModels;
using Warehouse_Managemet_System.SQL_Executer;

namespace Web_Management_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderHandlingController : Controller
    {
        private GetItem<Order> getOrder;
        private QueryHandler<Order> orderHandler;

        public OrderHandlingController()
        {
            orderHandler = new QueryHandler<Order>("Order", new SqlExecuter());
            getOrder = new GetItem<Order>(orderHandler);
        }

        [HttpGet(Name = "GetOrder")]
        public IActionResult GetOrder(string? id, string? customer, string? status, string? creationTime)
        {
            if(id != null)
            {
                (bool, Order) res = getOrder.RetrieveItem(id);
                if(res.Item1 && res.Item2.Id == id)
                {
                    return Ok(res.Item2);
                }
                else
                {
                    return BadRequest(res.Item2.Id);
                }
            }
            else
            {
                (bool, List<Order>) res = (false, new List<Order>() { new Order() { Id = "unknown error" } });
                if(customer != null || status != null || creationTime != null)
                {
                    Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>();
                    if(customer != null)
                    {
                        filters.Add("Customer", customer.Split(',').ToList());
                    }
                    if(status != null)
                    {
                        filters.Add("Status", status.Split(',').ToList());
                    }
                    if(creationTime != null)
                    {
                        filters.Add("CreationTime", creationTime.Split(',').ToList());
                    }
                    res = getOrder.RetrieveItems(filters, null);
                }
                else
                {
                    res = getOrder.RetrieveItems(null, null);
                }
                if (res.Item1 && res.Item2[0].Id != "No items matching the criteria could be found")
                {
                    return Ok(res.Item2);
                }
                else
                {
                    return BadRequest(res.Item2[0].Id);
                }
            }
        }

        [HttpPost(Name = "CreateOrder")]
        public IActionResult CreateOrder(string customer, string OrderItems, string amounts)
        {
            Order order = new Order() {Customer = customer, CreationTime = DateTime.Now };
            return BadRequest("Not implemented yet");
        }
    }
}
