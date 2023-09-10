using Microsoft.AspNetCore.Mvc;
using CustomerAPi.DataRepostitories;
using CustomerData.Models;

namespace CustomerAPi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IDataService _service;

        public CustomerController(ILogger<CustomerController> logger, IDataService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("customers")]
        public ActionResult<IEnumerable<Customers>> GetCustomers()
        {
            var customers = _service.GetCustomers();
            if (customers == null)
            {
                return NotFound(); 
            }

            return Ok(customers); 
        }

        [HttpGet("orders")]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            var orders = _service.GetOrders();
            if (orders == null)
            {
                return NotFound(); 
            }

            return Ok(orders); 
        }

        [HttpGet("orderitems")]
        public ActionResult<IEnumerable<OrderItems>> GetOrderItems()
        {
            var orderItems = _service.GetOrderItems();
            if (orderItems == null)
            {
                return NotFound(); 
            }

            return Ok(orderItems); 
        }
    }
}