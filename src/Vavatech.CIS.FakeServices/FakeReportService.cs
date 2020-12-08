using System;
using System.Collections.Generic;
using System.Text;
using Vavatech.CIS.IServices;
using Vavatech.CIS.Models;
using System.Linq;
using Bogus;

namespace Vavatech.CIS.FakeServices
{
    public class FakeReportService : IReportService
    {
        private readonly ICollection<Report> reports;

        public FakeReportService(Faker<Report> faker)
        {
            reports = faker.Generate(10);
        }

        public IEnumerable<Report> Get(Period period)
        {
            return reports.Where(r => r.CreateDate >= period.From && r.CreateDate <= period.To).ToList();
        }

        public Report Get(int id)
        {
            return reports.SingleOrDefault(r => r.Id == id);
        }

        public Report Get(int customerId, int reportId)
        {
            return reports.SingleOrDefault(r => r.Id == reportId && r.Customer.Id == customerId);
        }

        public IEnumerable<Report> GetByCustomer(int customerId)
        {
            return reports.Where(r => r.Customer.Id == customerId).ToList();
        }
    }
}
