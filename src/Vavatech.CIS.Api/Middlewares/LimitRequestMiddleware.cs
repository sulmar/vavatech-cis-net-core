using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vavatech.CIS.Api.Middlewares
{
    public class LimitRequestMiddleware
    {
        private readonly RequestDelegate next;

        private int counter = 0;

        private const int questLimit = 5;
        private const int authenticatedLimit = 50;

        public LimitRequestMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            int limit = questLimit;

            if (context.User.Identity.IsAuthenticated)
            {
                limit = authenticatedLimit;
            }

            if (counter > limit)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            }
            else
            {
                await next(context);

                counter++;
            }
        }
    }
}
