using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using Vavatech.CIS.Fakers;
using Vavatech.CIS.Models;

namespace Vavatech.CIS.SenderConsoleClient
{
    class Program
    {

        // dotnet add package Microsoft.AspNetCore.SignalR.Client

        static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Hello Signal-R Sender!");


            string url = "http://localhost:5000/signalr/customers";

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            connection.On<Customer>("YouHaveGotMessage",
               customer => Console.WriteLine($"Received {customer.FirstName} {customer.LastName}"));

            connection.On("Pong", 
                () => Console.WriteLine("Pong"));

            Console.WriteLine("Connecting...");
            await connection.StartAsync();

            string groupName = string.Format("Grupa {0}", connection.ConnectionId[0] % 3);

            Console.WriteLine($"Connected {groupName}.");

            await connection.SendAsync("Ping");

            CustomerFaker customerFaker = new CustomerFaker();

            var customers = customerFaker.GenerateForever();

            foreach (var customer in customers)
            {
                Console.WriteLine($"Send {customer.FirstName} {customer.LastName}");
                // await connection.SendAsync("SendCustomerAdded", customer);

                await connection.SendAsync("SendCustomerAddedToGroup", customer, groupName);

                await Task.Delay(TimeSpan.FromMilliseconds(100));
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();
        }
    }
}
