using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VPackage.Parser;

namespace DMXLibraries
{
    class Program
    {
        static void Main(string[] args)
        {
            string msg = "CODE=12;MESSAGE=Hello world";

            List<DataWrapper> dws = FrameParser.DecodeArray(msg);

            foreach (DataWrapper dw in dws)
            {
                Console.WriteLine(dw.ToString());
            }

            Console.ReadKey();
        }
    }
}
