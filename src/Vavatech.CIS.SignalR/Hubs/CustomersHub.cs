using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.SignalR.Hubs
{
    public class CustomersHub : Hub
    {

        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;

            string groupName = string.Format("Grupa {0}", connectionId[0] % 3);

            await Groups.AddToGroupAsync(connectionId, groupName);

            await base.OnConnectedAsync();
        }

        public async Task SendCustomerAdded(Customer customer)
        {
            // await Clients.All.SendAsync("YouHaveGotMessage", customer);

            await Clients.Others.SendAsync("YouHaveGotMessage", customer);
        }

        public async Task SendCustomerAddedToGroup(Customer customer, string groupName)
        {
            await Clients.Group(groupName).SendAsync("YouHaveGotMessage", customer);
        }

        public async Task Ping()
        {
            await Clients.Caller.SendAsync("Pong");
        }
    }
}
