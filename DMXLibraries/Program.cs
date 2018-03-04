using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VPackage.Parser;
using VPackage.Network;
using VPackage.Files;

using System.Net;

using System.IO;

namespace DMXLibraries
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DMXLib");

            NetworkManager networkManager = new NetworkManager("127.0.0.1", 5000, 12000, 5641);

            networkManager.OnMessageReceived += (message) =>
            {
                Console.WriteLine("Message recu: {0}", message);
            };

            networkManager.StartListening();

            do
            {
                Console.WriteLine("Message: ");

                string message = Console.ReadLine();
                networkManager.Send(message);

                Console.WriteLine("Continuer? (O/N)");
            }
            while (Console.ReadKey().Key == ConsoleKey.O);

            Console.ReadKey();
        }
    }
}
