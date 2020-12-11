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

            string username = "Justin_Konopelski43";
            string password = "12345";

            string credentials = $"{username}:{password}";

            string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {auth}");

            var response = await client.GetStringAsync(url);

            var reports = JsonConvert.DeserializeObject<IEnumerable<Report>>(response);

            return reports;
        }
    }
}
