using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Topshelf;

namespace Vavatech.CIS.FileService
{
    // dotnet add package Microsoft.Extensions.Logging

    public class FileSystemWatcherService : ServiceControl
    {
        private readonly FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();

        public FileSystemWatcherService(string directory)
        {
            // string directory = @"C:\temp\CIS";

            fileSystemWatcher.Path = directory;
            fileSystemWatcher.Filters.Add("*.txt");
            fileSystemWatcher.Filters.Add("*.bmp");

            fileSystemWatcher.Created += FileSystemWatcher_Created;
            fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
            fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;

        }

        public bool Start(HostControl hostControl)
        {
            fileSystemWatcher.EnableRaisingEvents = true;

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            fileSystemWatcher.EnableRaisingEvents = false;

            return true;
        }

        private void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"{e.ChangeType} {e.Name}");
        }

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"{e.ChangeType} {e.Name}");
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"{e.ChangeType} {e.Name}");
        }


        private void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"{e.ChangeType} {e.Name}");
        }
    }
}
