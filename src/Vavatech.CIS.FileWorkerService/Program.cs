using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vavatech.CIS.FileWorkerService
{
    // dotnet add package Microsoft.Extensions.Hosting.WindowsServices

    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
              .WriteTo.File("c:/temp/CIS/logs/log.txt", rollingInterval: RollingInterval.Day)
              .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()            // Rejestracja us³ugi Windows
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                })
                .UseSerilog();

        // dotnet publish -r win-x64 -c Release

        // sc create CISService BinPath="c:\...\Vavatech.CIS.FileWorkerService.exe"

        // sc start CISService
        // sc stop CISService
        // sc delete CISService

    }
}
