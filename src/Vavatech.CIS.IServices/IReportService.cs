using System;
using System.Collections.Generic;
using System.Text;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.IServices
{
    public interface IReportService
    {
        IEnumerable<Report> Get(Period period);
        Report Get(int id);
        IEnumerable<Report> GetByCustomer(int customerId);
        Report Get(int customerId, int reportId);
    }
}
