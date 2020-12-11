using System;
using System.Collections.Generic;
using System.Text;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.IServices
{
    public interface IAuthorizationService
    {
        bool TryAuthorize(string username, string password, out Customer customer);
    }

    public interface IApiKeyService
    {
        bool TryAuthorize(string apikey, out Customer customer);
    }
}
