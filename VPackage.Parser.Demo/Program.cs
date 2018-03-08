using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPackage.Parser;

namespace VPackage.Parser.Demo
{
    class Program
    {
        static void Main(string[] args)
        { 
            do
            {
                Console.WriteLine("Choisir option: \n1: Construire\n2: Déconstuire");
                int choice = -1;

                int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        try
                        {
                            Console.WriteLine("Choisir un nom (ne doit pas contenir {0} {1})", FrameParser.FrameSeparator, FrameParser.NameValueSeparator);
                            string name = Console.ReadLine();
                            Console.WriteLine("Choisir une valeur (ne doit pas contenir {0} {1})", FrameParser.FrameSeparator, FrameParser.NameValueSeparator);
                            string value = Console.ReadLine();

                            Console.WriteLine("Trame contruite: {0}", FrameParser.Encode(name, value));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Entrer une trame (exemple: NOM{0}valeur):", FrameParser.NameValueSeparator);
                            string frame = Console.ReadLine();

                            Console.WriteLine("Trame décodée: {0}", FrameParser.Decode(frame).ToString());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case -1:
                        Console.WriteLine("Erreur");
                        break;
                }
                Console.WriteLine("Continuez? (O/N)");
            }
            while (Console.ReadKey().Key != ConsoleKey.N);
        }
    }
}
