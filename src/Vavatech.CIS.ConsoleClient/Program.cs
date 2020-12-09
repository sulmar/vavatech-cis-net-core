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
            
            // VarTest();

            string baseUri = "https://localhost:5001/";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUri);

            IReportServiceAsync reportService = new ApiReportService(client);
            ICustomerServiceAsync customerService = new ApiCustomerService(client);

            await GetReportsTest(reportService);

            await GetCustomersTest(customerService);

            await AddCustomerTest(customerService);

            await UpdateCustomerTest(customerService);

            await customerService.RemoveAsync(10);

            // TODO: pobrać raporty na podany zakres dat dla klienta na podst. PESEL


            // TODO: zmodyfikować tytuł raportu

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();


        }

        private static void VarTest()
        {
            var x = new { Imie = "Marcin", Nazwisko = "Sulecki", Salary = 1000 };

            Console.WriteLine(x.Imie);
        }

        private static async Task GetReportsTest(IReportServiceAsync reportService)
        {
            Console.Write("Podaj datę początkową: ");
            DateTime from = DateTime.Parse(Console.ReadLine());

            Console.Write("Podaj datę końcową: ");
            DateTime to = DateTime.Parse(Console.ReadLine());

            Period period = new Period { From = from, To = to };

            await reportService.GetAsync(period);
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
