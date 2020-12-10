using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.ReceiverConsoleClient
{
    class Program
    {
        // dotnet add package Microsoft.AspNetCore.SignalR.Client

        static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine("Hello Signal-R Receiver!");

            string url = "http://localhost:5000/signalr/customers";

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            connection.On<Customer>("YouHaveGotMessage",
               customer => Console.WriteLine($"Received {customer.FirstName} {customer.LastName}"));

            Console.WriteLine("Connecting...");
            await connection.StartAsync();


            string groupName = string.Format("Grupa {0}", connection.ConnectionId[0] % 3);

            Console.WriteLine($"Connected {groupName}.");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();
        }
    }
}
