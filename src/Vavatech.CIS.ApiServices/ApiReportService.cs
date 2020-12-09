using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vavatech.CIS.IServices;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.ApiServices
{
    public class ApiReportService : IReportServiceAsync
    {
        private readonly HttpClient client;

        public ApiReportService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<Report>> GetAsync(Period period)
        {
            string url = $"api/reports?from={period.From:yyyy-MM-dd}&to={period.To:yyyy-MM-dd}";

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var response = await client.GetStringAsync(url);

            var reports = JsonConvert.DeserializeObject<IEnumerable<Report>>(response);

            return reports;
        }
    }
}
