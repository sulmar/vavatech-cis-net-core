using System.Threading.Tasks;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.IServices
{
    public interface ICustomerServer
    {
        Task SendCustomerAdded(Customer customer);
        Task Ping();
    }
}
