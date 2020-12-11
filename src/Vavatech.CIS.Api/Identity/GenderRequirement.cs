using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.Api.Identity
{
    public class GenderRequirement : IAuthorizationRequirement      // mark interface
    {
        public Gender Gender { get; set; }

        public GenderRequirement(Gender gender)
        {
            Gender = gender;
        }
    }

    public class GenderHandler : AuthorizationHandler<GenderRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GenderRequirement requirement)
        {
            if (!context.User.HasClaim(c=>c.Type == ClaimTypes.Gender))
            {
                return Task.CompletedTask;
            }

            Gender gender = Enum.Parse<Gender>(context.User.FindFirst(ClaimTypes.Gender).Value);

            if (gender == requirement.Gender)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;

        }
    }
}
