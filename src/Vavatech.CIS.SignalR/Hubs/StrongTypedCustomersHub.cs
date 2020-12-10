using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.CIS.IServices;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.SignalR.Hubs
{
    public class StrongTypedCustomersHub : Hub<ICustomerClient>, ICustomerServer
    {
        public async Task SendCustomerAdded(Customer customer)
        {
            await Clients.Others.YouHaveGotMessage(customer);
        }

        public async Task Ping()
        {
            await Clients.Caller.Pong();
        }
    }
}
