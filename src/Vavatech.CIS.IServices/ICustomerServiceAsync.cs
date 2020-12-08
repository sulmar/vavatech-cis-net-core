using System.Collections.Generic;
using System.Threading.Tasks;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.IServices
{
    public interface ICustomerServiceAsync
    {
        Task<IEnumerable<Customer>> GetAsync();
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task RemoveAsync(int customerId);
    }
}
