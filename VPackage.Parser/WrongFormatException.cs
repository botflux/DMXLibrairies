using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPackage.Parser
{
    /// <summary>
    /// Représente les erreurs liées au format de la données encodée
    /// </summary>
    public class WrongFormatException : Exception
    {
        /// <summary>
        /// Message par défaut
        /// </summary>
        private const string MESSAGE = "Data not properly formatted";

        /// <summary>
        /// Initialise une nouvelle instance de WrongFormatException
        /// </summary>
        public WrongFormatException () : base (MESSAGE) { }

        /// <summary>
        /// Initialise une nouvelle instance de WrongFormatException
        /// </summary>
        /// <param name="message">Message complémentaire</param>
        public WrongFormatException (string message) : base (string.Format("{0} - {1}", MESSAGE, message)) { }
    }
}
