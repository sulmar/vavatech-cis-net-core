﻿using System;
using System.Collections.Generic;
using Vavatech.CIS.IServices;
using Vavatech.CIS.Models;
using System.Linq;
using Bogus;
using Vavatech.CIS.Models.SearchCriterias;

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
            return customers.OrderBy(c => c.Id);
        }

        public Customer Get(int id)
        {
            return customers.SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<Customer> Get(Gender gender)
        {
            return customers.Where(c => c.Gender == gender).ToList();
        }

        public IEnumerable<Customer> Get(string lastName)
        {
            return customers.Where(c => c.LastName.StartsWith(lastName)).ToList();
        }

        public IEnumerable<Customer> Get(string firstname, decimal? from, decimal? to)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria)
        {
            IQueryable<Customer> query = customers.AsQueryable();

            if (!string.IsNullOrEmpty(searchCriteria.FirstName))
            {
                query = query.Where(c => c.FirstName.StartsWith(searchCriteria.FirstName));
            }

            if (searchCriteria.From.HasValue)
            {
                query = query.Where(c => c.Salary >= searchCriteria.From);
            }

            if (searchCriteria.To.HasValue)
            {
                query = query.Where(c => c.Salary <= searchCriteria.To);
            }

            return query.ToList();

        }

        public Customer GetByPesel(string number)
        {
            return customers.SingleOrDefault(c => c.Pesel == number);
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