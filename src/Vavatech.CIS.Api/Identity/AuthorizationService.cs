using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.CIS.IServices;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.Api.Identity
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ICustomerService customerService;

        public AuthorizationService(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public bool TryAuthorize(string username, string password, out Customer customer)
        {
            customer = customerService.GetByUsername(username);

            if (customer!=null)
            {
                // TODO: hashowanie hasła!
                return customer.HashedPassword == password;
            }

            return false;
        }
    }

    
}
