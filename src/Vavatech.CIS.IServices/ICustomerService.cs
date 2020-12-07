using System;
using System.Collections.Generic;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.IServices
{
    public interface ICustomerService
    {
        IEnumerable<Customer> Get();
        Customer Get(int id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Remove(int id);

        IEnumerable<Customer> Get(Gender gender);
        IEnumerable<Customer> Get(string lastName);
        Customer GetByPesel(string number);

    }
}
