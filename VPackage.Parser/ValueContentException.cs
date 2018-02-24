using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPackage.Parser
{
    /// <summary>
    /// Représente une erreur dans le contenu des valeurs encodées
    /// </summary>
    public class ValueContentException : Exception
    {
        #region Constants
        
        /// <summary>
        /// Message par défaut
        /// </summary>
        private const string MESSAGE = "Value contains the same character as frame separator";

        #endregion

        #region Constructors

        /// <summary>
        /// Intialise une nouvelle instance de la classe ValueContentExeption
        /// </summary>
        public ValueContentException () : base (MESSAGE) { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe ValueContentException
        /// </summary>
        /// <param name="message">Message complémentaire</param>
        public ValueContentException(string message) : base(string.Format("{0} - {1}", MESSAGE, message)) { }

        #endregion
    }
}
