using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Vavatech.CIS.IServices;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.Api.Identity
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {

        private const string apiKey = "X-API-KEY";

        private readonly IApiKeyService apiKeyService;


        public ApiKeyAuthenticationHandler(
            IApiKeyService apiKeyService,
            IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            this.apiKeyService = apiKeyService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(apiKey))
            {
                return AuthenticateResult.Fail("Missing X-API-KEY header");
            }

            string key = Request.Headers[apiKey];

            if (!apiKeyService.TryAuthorize(key, out Customer customer))
            {
                return AuthenticateResult.Fail("Invalid api key");
            }

            ClaimsIdentity identity = new ClaimsIdentity("ApiKey");

            identity.AddClaim(new Claim("Pesel", customer.Pesel));
            identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "trainer"));
            identity.AddClaim(new Claim(ClaimTypes.Email, customer.Email));

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, "ApiKey");

            return AuthenticateResult.Success(ticket);
        }
    }
}
