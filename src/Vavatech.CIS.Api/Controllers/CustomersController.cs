using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.CIS.IServices;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.Api.Controllers
{
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }


        // GET api/customers
        [HttpGet]
        public IActionResult Get()
        {
            var customers = customerService.Get();

            return Ok(customers);
        }

        // GET api/customers/female
        [HttpGet("female")]
        public IActionResult GetFemales()
        {
            var customers = customerService.Get(Gender.Female);

            return Ok(customers);
        }

        // GET api/customers/male
        [HttpGet("male")]
        public IActionResult GetMales()
        {
            var customers = customerService.Get(Gender.Male);

            return Ok(customers);
        }
    }
}
