using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VPackage.Parser;
using VPackage.Network;
using VPackage.Files;
using VPackage.Json;

using System.Net;

using System.IO;
using System.Runtime.Serialization;

namespace DMXLibraries
{
    class Program
    {
        static void Main(string[] args)
        {
            Packet p = new Packet();
            string json = JSONSerializer.Serialize<Packet>(p);

            NetworkManager networkManager = new NetworkManager("10.129.22.26", 5000, 15000, 15000);

            networkManager.OnMessageReceived += (message) =>
            {
                Console.WriteLine("Message recu: {0}", message);
            };
            networkManager.StartListening();

            do
            {
                Console.WriteLine("Message: ");

                string message = Console.ReadLine();
                networkManager.Send(json);

                Console.WriteLine("Continuer? (O/N)");
            }
            while (Console.ReadKey().Key == ConsoleKey.O);

            Console.ReadKey();
        }

        [DataContract]
        public class Packet
        {
            [DataMember]
            public string CIBLE = "PROJO";
            [DataMember]
            private string ADDRCIBLE = "1";
            [DataMember]
            private string RED = "0";
            [DataMember]
            private string GREEN = "10";
            [DataMember]
            private string BLUE = "255";
            [DataMember]
            private string INTENSITY = "255";
        }
    }
}

/*
            // Test NetworkManager
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
            */
