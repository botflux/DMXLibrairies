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
                Console.WriteLine("Received: {0}", message);
            };
            server.StartListening();
            do
            {
                Console.WriteLine("Entrer un message?");
                string message = Console.ReadLine();
                client.Send(@"Aenean sollicitudin orci odio, et placerat tellus malesuada lobortis. Aliquam et sapien eu dui maximus sodales eget vitae orci. Nullam pretium ultricies tellus maximus semper. Quisque eu ex erat. Vivamus neque nulla, aliquam et justo sit amet, mollis feugiat nibh. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Praesent consequat, massa eu consequat pharetra, urna odio interdum libero, et tristique ex tortor ac leo. In mattis diam viverra nisi rhoncus, vel malesuada tortor varius. Ut sed lectus id metus semper sollicitudin. Aenean ac lectus condimentum, scelerisque quam feugiat, ultrices tortor. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae;

Nulla semper ante vitae rhoncus imperdiet. Mauris sapien mauris, scelerisque in urna quis, feugiat scelerisque dolor. Ut a convallis ligula, elementum condimentum mi. Praesent ornare a eros at aliquet. Proin et odio quis odio pulvinar varius. Duis at risus nec ipsum euismod tincidunt at nec nisi. Suspendisse ultrices placerat orci ac posuere. Nulla egestas fringilla ultricies. Nunc fringilla eu diam mollis viverra. Quisque placerat et metus in faucibus. Fusce id mattis tortor. Nulla pharetra, mi in eleifend feugiat, purus ipsum dignissim ligula, vel pharetra ex leo a turpis. Pellentesque purus dolor, molestie vel est sit amet, porttitor efficitur enim. Donec vel leo dolor.

Ut in ornare libero. In hac habitasse platea dictumst. Sed vitae ipsum nec nisl bibendum tincidunt at ut est. Nunc tempus vel mi nec blandit. Praesent vitae posuere urna. Vestibulum nullam.Sed vitae ipsum nec nisl bibendum tincidunt at ut est. Nunc tempus vel mi nec blandit. Praesent vitae posuere urna. Vestibulum nullam.Sed vitae ipsum nec nisl bibendum tincidunt at ut est. Nunc tempus vel mi nec blandit. Praesent vitae posuere urna. Vestibulum nullam.Sed vitae ipsum nec nisl bibendum tincidunt at ut est. Nunc tempus vel mi nec blandit. Praesent vitae posuere urna. Vestibulum nullam.Sed vitae ipsum nec nisl bibendum tincidunt at ut est. Nunc tempus vel mi nec blandit. Praesent vitae posuere urna. Vestibulum nullam.Sed vitae ipsum nec nisl bibendum tincidunt at ut est. Nunc tempus vel mi nec blandit. Praesent vitae posuere urna. Vestibulum nullam.Sed vitae ipsum nec nisl bibendum tincidunt at ut est. Nunc tempus vel mi nec blandit. Praesent vitae posuere urna. Vestibulum nullam.");
                Console.WriteLine("Espace pour continuer...");
            }
            while (Console.ReadKey().Key == ConsoleKey.Spacebar);

            server.StopListening();
        }
    }
}
