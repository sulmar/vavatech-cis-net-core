using System;
using System.IO;

namespace Vavatech.CIS.FileService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ResetColor();

            Console.WriteLine("Hello File Service!");

            string directory = @"C:\temp\CIS";

            MonitorDirectory(directory);

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();

        }

        private static void MonitorDirectory(string directory)
        {
            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.Path = directory;
            // fileSystemWatcher.Filter = "*.txt";

            fileSystemWatcher.Filters.Add("*.txt");
            fileSystemWatcher.Filters.Add("*.bmp");

            fileSystemWatcher.Created += FileSystemWatcher_Created;
            fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
            fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;

            // Włączyć !!!
            fileSystemWatcher.EnableRaisingEvents = true;

        }

        private static void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"{e.ChangeType} {e.Name}");
            Console.ResetColor();
        }

        private static void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{e.ChangeType} {e.Name}");
            Console.ResetColor();
        }

        private static void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{e.ChangeType} {e.Name}");
            Console.ResetColor();
        }


        private static void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{e.ChangeType} {e.Name}");
            Console.ResetColor();
        }

     
       


    }
}
