using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.IServices
{
    public interface IReportService
    {
        IEnumerable<Report> Get(Period period);
        Report Get(int id);
        IEnumerable<Report> GetByCustomer(int customerId);
        Report Get(int customerId, int reportId);
        void Add(Report report);
    }

    public interface IReportServiceAsync
    {
        Task<IEnumerable<Report>> GetAsync(Period period);
    }
}
