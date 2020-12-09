using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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

        private readonly IWebHostEnvironment hostEnvironment;

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

        [HttpGet("~/api/customers/{customerId}/reports/{reportId}", Name = "GetReportById")]
        public IActionResult GetByCustomer(int customerId, int reportId)
        {
            var report = reportService.Get(customerId, reportId);

            if (report == null)
                return NotFound();

            return Ok(report);
        }


        [HttpPost("~/api/customers/{customerId}/reports")]
        public IActionResult Post(int customerId, [FromBody] Report report)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            reportService.Add(report);

            return CreatedAtRoute("GetReportById", new { customerId, reportId = report.Id }, report);
        }


        // api/reports/upload
        [HttpPost("upload")]
        public IActionResult Post(IFormFile file, [FromServices] IWebHostEnvironment hostEnvironment)
        {
            // TODO: process file
            // MemoryStream stream = new MemoryStream();

            string uploads = Path.Combine(hostEnvironment.ContentRootPath, "uploads");
            string filename = Path.Combine(uploads, file.FileName);

            using (Stream stream = new FileStream(filename, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Accepted();
        }
        


    }
}
