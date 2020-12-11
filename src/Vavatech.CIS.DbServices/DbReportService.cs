using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Vavatech.CIS.IServices;
using Vavatech.CIS.Models;
using Dapper;
using System.Linq;

namespace Vavatech.CIS.DbServices
{
    public class DbReportService : IReportService
    {
        private readonly IDbConnection connection;

        public DbReportService(IDbConnection connection)
        {
            this.connection = connection;
        }

        public void Add(Report report)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Report> Get(Period period)
        {
            throw new NotImplementedException();
        }


        public Report Get(int id)
        {
            string sql = @"select ReportId, Title, CreateDate, PeriodFrom, PeriodTo, c.CustomerId, c.*
                            from dbo.Reports as r inner join dbo.Customers as c on r.CustomerId = c.CustomerId
                            WHERE r.ReportId = @ReportId";

            var reports = connection.Query<Report, Customer, Report>(sql,
                (report, customer) =>
                {
                    report.Customer = customer;

                    return report;
                },
                param: new { ReportId = id },
                splitOn: "CustomerId"
                );

            return reports.SingleOrDefault();
        }

      

        public Report Get(int customerId, int reportId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Report> GetByCustomer(int customerId)
        {
            string procedure = @"GetReportByCustomerId";

            var reports = connection.Query<Report, Customer, Report>(procedure,
                (report, customer) =>
                {
                    report.Customer = customer;

                    return report;
                },
                param: new { @CustomerId = customerId },
                splitOn: "CustomerId",
                commandType: CommandType.StoredProcedure
                );

            return reports;

  
        }

        public IEnumerable<Report> GetByCustomer2(int customerId)
        {
            string sql = @"select ReportId, Title, CreateDate, PeriodFrom, PeriodTo, c.CustomerId, c.*
                            from dbo.Reports as r inner join dbo.Customers as c on r.CustomerId = c.CustomerId
                            WHERE r.CustomerId = @CustomerId";

            var reports = connection.Query<Report, Customer, Report>(sql,
                (report, customer) =>
                {
                    report.Customer = customer;

                    return report;
                },
                param: new { @CustomerId = customerId },
                splitOn: "CustomerId"
                );

            return reports;


        }
    }
}
