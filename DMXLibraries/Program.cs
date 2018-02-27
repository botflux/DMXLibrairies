using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VPackage.Parser;
using VPackage.Network;
using VPackage.Files;

using System.IO;

namespace DMXLibraries
{
    class Program
    {
        const string FILE_NAME = @"c:\mes_tests\sous-dossier";

        static void Main(string[] args)
        {
            try
            {
                FileManager.Write(FILE_NAME + @"\test.txt", "Hello world", FileManager.WriteOptions.CreateDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
