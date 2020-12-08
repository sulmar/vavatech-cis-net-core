using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Vavatech.CIS.ApiServices;
using Vavatech.CIS.Fakers;
using Vavatech.CIS.IServices;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello REST API Client!");

            string baseUri = "https://localhost:5001/";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUri);

            ICustomerServiceAsync customerService = new ApiCustomerService(client);

            await GetCustomersTest(customerService);

            await AddCustomerTest(customerService);

            await UpdateCustomerTest(customerService);

            await customerService.RemoveAsync(10);


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();


        }



        private static async Task AddCustomerTest(ICustomerServiceAsync customerService)
        {
            CustomerFaker customerFaker = new CustomerFaker();

            Customer customer = customerFaker.Generate();

            await customerService.AddAsync(customer);
        }

        private static async Task UpdateCustomerTest(ICustomerServiceAsync customerService)
        {
            CustomerFaker customerFaker = new CustomerFaker();

            Customer customer = customerFaker.Generate();

            await customerService.UpdateAsync(customer);
        }

        private static async Task GetCustomersTest(ICustomerServiceAsync customerService)
        {
            IEnumerable<Customer> customers = await customerService.GetAsync();

            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.FirstName} {customer.LastName}");
            }
        }
    }
}
