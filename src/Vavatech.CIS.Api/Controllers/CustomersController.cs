using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.CIS.IServices;
using Vavatech.CIS.Models;
using Vavatech.CIS.Models.SearchCriterias;

namespace Vavatech.CIS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }


        // GET api/customers
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var customers = customerService.Get();

        //    return Ok(customers);
        //}

        // GET api/customers/female
        [HttpGet("female")]
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status200OK)]
        public IActionResult GetFemales()
        {
            var customers = customerService.Get(Gender.Female);

            return Ok(customers);
        }

        // GET api/customers/male
        [HttpGet("male")]
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status200OK)]
        public IActionResult GetMales()
        {
            var customers = customerService.Get(Gender.Male);

            return Ok(customers);
        }

        // GET api/customers/{customerId}
        [HttpGet("{customerId:int:min(1):max(200)}", Name = "GetByCustomerId")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int customerId)
        {
            var customer = customerService.Get(customerId);

            if (customer == null)
                return NotFound();

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

        /// <summary>
        /// Pobiera klienta wg PESEL
        /// </summary>
        /// <param name="number">Numer w formacie PESEL</param>
        /// <returns>Klient</returns>
        [HttpGet("{number:pesel}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetByPesel(string number)
        {
            var customer = customerService.GetByPesel(number);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // GET api/customers?FirstName=John&From=100&To=200
        //[HttpGet]
        //public IActionResult Get(string firstname, decimal? from, decimal? to)
        //{
        //    var customers = customerService.Get(firstname, from, to);

        //    return Ok(customers);
        //}

        // GET api/customers?FirstName=John&From=100&To=200
        [HttpGet]
        public IActionResult Get(CustomerSearchCriteria searchCriteria)
        {
            var customers = customerService.Get(searchCriteria);

            return Ok(customers);
        }

        // GET api/customers/10/orders
        //[HttpGet("{customerId}/orders")]
        //public IActionResult GetOrders(int customerId)
        //{
        //    throw new NotImplementedException();
        //}

        // GET api/customers/10/invoices
        //[HttpGet("{customerId}/invoices")]
        //public IActionResult GetInvoices(int customerId)
        //{
        //    throw new NotImplementedException();
        //}

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            customerService.Add(customer);

            // return Created($"http://localhost:5000/api/customers/{customer.Id}", customer);

            return CreatedAtRoute("GetByCustomerId", new { customerId = customer.Id }, customer);
        }


        // PUT api/customers/{customerId}
        [HttpPut("{customerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Put(int customerId, [FromBody] Customer customer)
        {
            if (customerId != customer.Id)
            {
                return BadRequest();
            }

            customerService.Update(customer);

            return NoContent();
        }

        // PATCH api/customers/{customerId}
        // Content-Type: application/json
        //[HttpPatch("{customerId}")]
        //public IActionResult Patch(int customerId, [FromBody] Customer customer)
        //{
        //    throw new NotImplementedException();
        //}


        // http://jsonpatch.com/

        // PATCH api/customers/{customerId}
        // Content-Type: application/merge-patch+json

        // dotnet add package Microsoft.AspNetCore.JsonPatch
        [HttpPatch("{customerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Patch(int customerId, [FromBody] JsonPatchDocument patchDocument)
        {
            Customer customer = customerService.Get(customerId);

            if (customer == null)
                return NotFound();

            patchDocument.ApplyTo(customer);

            return NoContent();
        }

        [HttpDelete("{customerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public IActionResult Delete(int customerId)
        {
            customerService.Remove(customerId);

            return NoContent();
        }

    }
}
