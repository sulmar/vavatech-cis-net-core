using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Vavatech.CIS.FileWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;

            string directory = @"C:\temp\CIS";

            fileSystemWatcher.Path = directory;
            fileSystemWatcher.Filters.Add("*.txt");
            fileSystemWatcher.Filters.Add("*.bmp");

            fileSystemWatcher.Created += FileSystemWatcher_Created;
            fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
            fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            fileSystemWatcher.EnableRaisingEvents = true;

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            fileSystemWatcher.EnableRaisingEvents = false;

            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("File System Watcher running at: {time}", DateTimeOffset.Now);
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }

        private void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            _logger.LogInformation($"{e.ChangeType} {e.Name}");
        }

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            _logger.LogInformation($"{e.ChangeType} {e.Name}");
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            _logger.LogInformation($"{e.ChangeType} {e.Name}");
        }


        private void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            _logger.LogInformation($"{e.ChangeType} {e.Name}");
        }

    }
}
