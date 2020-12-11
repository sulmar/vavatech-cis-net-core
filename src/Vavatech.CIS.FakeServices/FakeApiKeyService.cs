using System;
using System.Collections.Generic;
using System.Text;
using Vavatech.CIS.IServices;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.FakeServices
{
    public class FakeApiKeyService : IApiKeyService
    {
        private readonly Dictionary<string, int> apiKeys = new Dictionary<string, int>();

        public FakeApiKeyService()
        {
            apiKeys.Add( "4f4frererte", 1);
            apiKeys.Add( "fdfsdfsdfsfs", 2);
            apiKeys.Add( "fsdfsd" , 3);
        }

        public bool TryAuthorize(string apiKey, out Customer customer)
        {
            customer = null;

            return apiKeys.ContainsKey(apiKey);
        }
    }
}
