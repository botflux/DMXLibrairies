using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPackage.Parser
{
    public class ValueContentException : Exception
    {
        private const string message = "Value contains the same character as frame separator";

        public ValueContentException () : base (message) { }

        public ValueContentException(string _message) : base(string.Format("{0} - {1}", message, _message)) { }
    }
}
