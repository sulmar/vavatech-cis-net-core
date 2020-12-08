using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vavatech.CIS.IServices;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.Api.Controllers
{
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService reportService;

        public ReportsController(IReportService reportService)
        {
            this.reportService = reportService;
        }


        [HttpGet("{reportId}")]
        public IActionResult Get(int reportId)
        {
            Report report = reportService.Get(reportId);

            if (report == null)
                return NotFound();

            return Ok(report);
        }

        [HttpGet]
        public IActionResult Get(Period period)
        {
            var reports = reportService.Get(period);

            return Ok(reports);
        }

        [HttpGet("~/api/customers/{customerId}/reports")]
        public IActionResult GetByCustomer(int customerId)
        {
            var reports = reportService.GetByCustomer(customerId);

            return Ok(reports);
        }

        [HttpGet("~/api/customers/{customerId}/reports/{reportId}")]
        public IActionResult GetByCustomer(int customerId, int reportId)
        {
            var report = reportService.Get(customerId, reportId);

            if (report == null)
                return NotFound();

            return Ok(report);
        }

    }
}
