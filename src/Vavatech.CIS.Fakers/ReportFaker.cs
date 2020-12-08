using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.Fakers
{
    public class ReportFaker : Faker<Report>
    {
        public ReportFaker(Faker<ReportDetail> reportDetailFaker, Faker<Customer> customerFaker, Faker<Period> periodFaker)
        {
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Title, f => f.Lorem.Word());
            RuleFor(p => p.CreateDate, f => f.Date.Recent(30));
            RuleFor(p => p.Period, f => periodFaker.Generate());   
            RuleFor(p => p.Details, f=> reportDetailFaker.Generate(f.Random.Int(3, 7)));
            RuleFor(p => p.Customer, f => customerFaker.Generate());
        }
    }
}
