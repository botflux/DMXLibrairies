using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPackage.Parser
{
    public class WrongFormatException : Exception
    {
        private const string message = "Data not properly formatted";

        public WrongFormatException () : base (message) { }

        public WrongFormatException (string _message) : base (string.Format("{0} - {1}", message, _message)) { }
    }
}
