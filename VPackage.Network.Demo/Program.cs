using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPackage.Network.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkManager client = new NetworkManager("127.0.0.1", 4999, 14999, 14999);
            NetworkManager server = new NetworkManager("127.0.0.1", 14999, 4999, 4999);
            server.OnMessageReceived += (message) =>
            {
                Console.WriteLine("Received: / {0} /", message);
            };
            server.StartListening();
            do
            {
                Console.WriteLine("Entrer un message?");
                string message = Console.ReadLine();
                
                //client.Send(message);
                client.SendFragmented(message);
                Console.WriteLine("Espace pour continuer...");
            }
            while (Console.ReadKey().Key == ConsoleKey.Spacebar);

            server.StopListening();
        }
    }
}
