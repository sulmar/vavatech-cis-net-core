using Bogus;
using Bogus.Extensions;
using Bogus.Extensions.Poland;
using System;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.Fakers
{

    // dotnet add package Bogus
    // dotnet add package Sulmar.Bogus.Extensions.Poland
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker()
        {
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.Pesel, f => f.Person.Pesel());
            RuleFor(p => p.Gender, f => (Gender) f.Person.Gender);
            RuleFor(p => p.Salary, f =>  Math.Round( f.Random.Decimal(100, 1000), 0).OrNull(f, 0.7f));
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f));
        }
    }
}
