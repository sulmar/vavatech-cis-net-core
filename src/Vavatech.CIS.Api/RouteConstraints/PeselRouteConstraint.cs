using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Validators.Abstractions;
using Validators.Polish;

namespace Vavatech.CIS.Api.RouteConstraints
{

    // dotnet add package PolishValidators
    public class PeselRouteConstraint : IRouteConstraint
    {
        private readonly PeselValidator validator;

        public PeselRouteConstraint(PeselValidator validator)
        {
            this.validator = validator;
        }

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out object routeValue))
            {
                string number = routeValue.ToString();

                try
                {
                    return validator.IsValid(number);
                }
                catch(FormatException)
                {
                    return false;   
                }
            }

            return false;
        }
    }
}
