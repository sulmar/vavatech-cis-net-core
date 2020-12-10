using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.IServices
{
    public interface ICustomerClient
    {
        Task YouHaveGotMessage(Customer customer);
        Task Pong();
    }
}
