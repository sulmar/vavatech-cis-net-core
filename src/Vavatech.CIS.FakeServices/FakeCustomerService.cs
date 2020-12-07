using System;
using System.Collections.Generic;
using Vavatech.CIS.IServices;
using Vavatech.CIS.Models;
using System.Linq;
using Bogus;

namespace Vavatech.CIS.FakeServices
{
    public class FakeCustomerService : ICustomerService
    {
        private readonly IList<Customer> customers;

        public FakeCustomerService(Faker<Customer> faker)
        {
            customers = faker.Generate(100);

            customers[0].Partner = customers[1];
            customers[1].Partner = customers[0];
        }

        public void Add(Customer customer)
        {
            int lastId = customers.Max(c => c.Id);

            customer.Id = ++lastId;

            customers.Add(customer);
        }

        public IEnumerable<Customer> Get()
        {
            return customers.OrderBy(c=>c.Id);
        }

        public Customer Get(int id)
        {
            return customers.SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<Customer> Get(Gender gender)
        {
            return customers.Where(c => c.Gender == gender).ToList();
        }

        public void Remove(int id)
        {
            customers.Remove(Get(id));
        }

        public void Update(Customer customer)
        {
            Remove(customer.Id);
            customers.Add(customer);
        }
    }
}
