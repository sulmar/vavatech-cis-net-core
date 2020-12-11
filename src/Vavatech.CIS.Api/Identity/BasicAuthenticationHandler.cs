﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Vavatech.CIS.IServices;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.Api.Identity
{


    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string authorizationKey = "Authorization";

        private readonly IAuthorizationService authorizationService;

        public BasicAuthenticationHandler(
            IAuthorizationService authorizationService,
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
            this.authorizationService = authorizationService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey(authorizationKey))
            {
                return AuthenticateResult.Fail("Missing authorization header");
            }

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers[authorizationKey]);

            if (authHeader.Scheme!="Basic")
            {
                return AuthenticateResult.Fail("Invalid authorization scheme");
            }

            //if (!authHeader.Parameter.IsBase64String())
            //{
            //    return AuthenticateResult.Fail("Invalid authorization parameter");
            //}

            byte[] credentialBytes = Convert.FromBase64String(authHeader.Parameter);

            string[] credentials = Encoding.UTF8.GetString(credentialBytes).Split(":");

            string username = credentials[0];
            string password = credentials[1];

            if (!authorizationService.TryAuthorize(username, password, out Customer customer))
            {
                return AuthenticateResult.Fail("Invalid username or password");
            }

            ClaimsIdentity identity = new ClaimsIdentity(Scheme.Name);
            identity.AddClaim(new Claim("Pesel", customer.Pesel));
            identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "trainer"));
            identity.AddClaim(new Claim(ClaimTypes.Email, customer.Email));

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }

    public static class StringExtensions
    {
        public static bool IsBase64String(this string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);

            return Convert.TryFromBase64String(base64, buffer, out int _);
        }
    }
}
