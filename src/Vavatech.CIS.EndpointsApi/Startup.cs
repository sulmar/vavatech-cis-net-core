using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Vavatech.CIS.Fakers;
using Vavatech.CIS.FakeServices;
using Vavatech.CIS.IServices;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.EndpointsApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICustomerService, FakeCustomerService>();
            services.AddSingleton<Faker<Customer>, CustomerFaker>();
            services.Configure<FakeCustomerServiceOptions>(options => options.Quantity = 20);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/api/customers", async context =>
                {
                    await context.Response.WriteAsync("Hello Customers!");
                });

                endpoints.MapGet("/api/customers/{id:int}", async context =>
                {
                    string id = context.Request.RouteValues["id"].ToString();

                    int customerId = int.Parse(id);

                    ICustomerService customerService = context.RequestServices.GetRequiredService<ICustomerService>();

                    var customer = customerService.Get(customerId);

                    string json = JsonConvert.SerializeObject(customer);

                    context.Response.Headers.Append("Content-Type", "application/json");

                    await context.Response.WriteAsync(json);
                });

                endpoints.MapPost("/api/customers", async context =>
                {
                    StreamReader streamReader = new StreamReader(context.Request.Body);

                    string json = await streamReader.ReadToEndAsync();

                    Customer customer = JsonConvert.DeserializeObject<Customer>(json);

                    ICustomerService customerService = context.RequestServices.GetRequiredService<ICustomerService>();

                    customerService.Add(customer);

                    context.Response.StatusCode = StatusCodes.Status201Created;
                    await context.Response.WriteAsync("Created");
                });

                endpoints.MapGet("/api/reports", async context =>
                {
                    await context.Response.WriteAsync("Hello Reports!");
                });

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
