﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPackage.Json
{
    /// <summary>
    /// Lever lors ce qu'une classe n'implémente pas un attribut
    /// </summary>
    public class AttributeMissingException : Exception
    {
        private const string MESSAGE = "Missing a attribute declaration";

        public AttributeMissingException () : base (MESSAGE) { }
        public AttributeMissingException (string message) : base (string.Format("{0} - {1}", MESSAGE, message)) { }
    }
}
