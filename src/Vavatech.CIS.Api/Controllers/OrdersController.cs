using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vavatech.CIS.Api.Controllers
{
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        // GET api/customers/{customerId}/orders
        [HttpGet("~/api/customers/{customerId}/orders")]
        public IActionResult GetOrders(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
