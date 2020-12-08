using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vavatech.CIS.IServices;
using Vavatech.CIS.Models;
using Vavatech.CIS.Models.SearchCriterias;

namespace Vavatech.CIS.ApiServices
{
    public class ApiCustomerService : ICustomerServiceAsync
    {
        private readonly HttpClient client;

        public ApiCustomerService(HttpClient client)
        {
            this.client = client;
        }

        public async Task AddAsync(Customer customer)
        {
            string json = JsonConvert.SerializeObject(customer);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/customers", content);

            if (response.IsSuccessStatusCode)
            {
                string responseJson = await response.Content.ReadAsStringAsync();

                customer = JsonConvert.DeserializeObject<Customer>(responseJson);

                if (response.Headers.TryGetValues("Location", out IEnumerable<string> locations))
                {
                    Console.WriteLine(locations.Single());
                }
            }
        }

        public async Task<IEnumerable<Customer>> GetAsync()
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            string response =  await client.GetStringAsync("api/customers");

            IEnumerable<Customer> customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(response);

            return customers;
        }

        public async Task RemoveAsync(int customerId)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/customers/{customerId}");

            if (response.IsSuccessStatusCode)
            {

            }
        }

        public async Task UpdateAsync(Customer customer)
        {
            string json = JsonConvert.SerializeObject(customer);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync($"api/customers/{customer.Id}", content);

            if (response.IsSuccessStatusCode)
            {
            }
        }
    }
}
