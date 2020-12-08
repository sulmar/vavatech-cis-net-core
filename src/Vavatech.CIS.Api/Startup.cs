using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Validators.Polish;
using Vavatech.CIS.Api.RouteConstraints;
using Vavatech.CIS.Fakers;
using Vavatech.CIS.FakeServices;
using Vavatech.CIS.IServices;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICustomerService, FakeCustomerService>();
            services.AddSingleton<Faker<Customer>, CustomerFaker>();

            services.AddSingleton<IReportService, FakeReportService>();
            services.AddSingleton<Faker<Report>, ReportFaker>();
            services.AddSingleton<Faker<ReportDetail>, ReportDetailFaker>();
            services.AddSingleton<Faker<Period>, PeriodFaker>();

            services.AddSingleton<PeselValidator>();

            // Rejestracja w�asnej regu�y tras
            services.Configure<RouteOptions>(options => options.ConstraintMap.Add("pesel", typeof(PeselRouteConstraint)));

            // Rejestracja opcji z pliku konfiguracyjnego
            services.Configure<FakeCustomerServiceOptions>(Configuration.GetSection("FakeCustomer"));

            // Rejestracja opcji w kodzie
            //services.Configure<FakeCustomerServiceOptions>(options =>
            //{
            //    options.Quantity = 40;
            //});

            // dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());                   // konwersja enum na tekst
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;    // pomijanie warto�ci null
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;  // zapobiega zap�tleniu
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string smsApi = Configuration["SmsApiAddress"];
            int port = int.Parse(Configuration["SmsApiPort"]);

            string gatewayAddress = Configuration["GatewayApi:Address"];
            int gatewayPort = int.Parse(Configuration["GatewayApi:Port"]);


            // %APPDATA%\Microsoft\UserSecrets
            string googleMapSecretKey = Configuration["GoogleMapSecretKey"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
