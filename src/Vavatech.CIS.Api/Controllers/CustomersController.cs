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

        // GET api/customers/{customerId}
        [HttpGet("{customerId:int:min(1):max(100)}")]
        public IActionResult Get(int customerId)
        {
            var customer = customerService.Get(customerId);

            return Ok(customer);
        }

        // GET api/customers/{lastname}
        [HttpGet("{lastname:alpha:minlength(3)}")]
        public IActionResult Get(string lastname)
        {
            var customers = customerService.Get(lastname);

            return Ok(customers);
        }

        // GET api/customers/01-434
        [HttpGet("{postcode:regex(^\\d{{2}}-\\d{{3}}$)}")]
        public IActionResult GetByPostCode(string postcode)
        {
            var customers = customerService.Get(postcode);

            return Ok(customers);
        }

        // GET api/customers/{pesel}

        [HttpGet("{number:pesel}")]
        public IActionResult GetByPesel(string number)
        {
            var customer = customerService.GetByPesel(number);

            return Ok(customer);
        }

    }
}
