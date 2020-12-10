using System;
using System.IO;
using Topshelf;

namespace Vavatech.CIS.FileService
{
    // dotnet add package Topshelf

    class Program
    {
        static void Main(string[] args)
        {
            string directory = @"C:\temp\CIS";

            HostFactory.Run(x =>
            {
                x.Service<FileSystemWatcherService>(service =>
                {
                    service.ConstructUsing(s => new FileSystemWatcherService(directory));
                });

                x.SetServiceName("CISService");
                x.SetDescription("CIS Service using Topshelf");
                x.StartAutomatically();
            });

            // Vavatech.CIS.FileService.exe install start
        }
    }
}
