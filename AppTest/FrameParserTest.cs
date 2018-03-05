using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using VPackage.Parser;

namespace AppTest
{
    [TestClass]
    public class FrameParserTest
    {
        #region Encode test
        /// <summary>
        /// Cas: Nom et valeur correcte
        /// Renvoie: La trame encodée correctement
        /// </summary>
        [TestMethod]
        public void Encode_RightNameAndValue_NormalResult()
        {
            string rightName = "blue";
            string rightValue = "200";
            string excepted = "BLUE=200";
            string actual = FrameParser.Encode(rightName, rightValue);
            
            Assert.AreEqual(excepted, actual);
        }

        /// <summary>
        /// Cas: Nom nul et valeur correcte
        /// Renvoie: ArguementNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Encode_NullNameRightValue_ArgumentNullException ()
        {
            string frame = FrameParser.Encode(null, "200");
        }

        /// <summary>
        /// Cas: Nom correcte et valeur null
        /// Renvoie: ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Encode_RightNameNullValue_ArgumentNullException ()
        {
            string frame = FrameParser.Encode("a", null);
        }

        /// <summary>
        /// Cas: Nom vide et valeur correcte
        /// Renvoie: ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Encode_EmptyNameRightValue_ArgumentNullException ()
        {
            string frame = FrameParser.Encode("", "200");
        }

        /// <summary>
        /// Cas: Le nom contient le séparateur utilisé nom/valeur
        /// Renvoie: Une exception ValueContentException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ValueContentException))]
        public void Encode_NameContainValueSeparatorRightValue_ValueContentException ()
        {
            string nom = "bl=ue";
            int value = 100;

            string actual = FrameParser.Encode(nom, value);
        }

        /// <summary>
        /// Cas: La valeur contient le séparateur utilisé nom/valeur
        /// Renvoie: Une exception ValueContentException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ValueContentException))]
        public void Encode_RightNameValueContainValueSeparator_ValueContentException ()
        {
            string nom = "blue";
            string value = "=";

            string actual = FrameParser.Encode(nom, value);
        }

        /// <summary>
        /// Cas: Le nom contient le séparateur de trame
        /// Renvoie: Une exception ValueContentException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ValueContentException))]
        public void Encode_NameContainFrameSeparatorRightValue()
        {
            string nom = "bl;ue";
            string value = "200";

            string actual = FrameParser.Encode(nom, value);
        }

        /// <summary>
        /// Cas: La valeur contient le séparateur de trame
        /// Renvoie: Une exception ValueContentException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ValueContentException))]
        public void Encode_RightNameValueContainFrameSeparator ()
        {
            string nom = "blue";
            string value = ";200";

            string actual = FrameParser.Encode(nom, value);
        }
        #endregion

        /// <summary>
        /// Cas: La valeur et le nom sont correcte
        /// Renvoie la valeur décodée
        /// </summary>
        [TestMethod]
        public void Decode_RightNameAndValue_DecodedFrame ()
        {
            string frame = "BLUE=200";

            DataWrapper actual = FrameParser.Decode(frame);
            
            Assert.IsTrue(actual.Name == "BLUE" && actual.Value.ToString() == "200");
        }

        [TestMethod]
        [ExpectedException(typeof(WrongFormatException))]
        public void Decode_NoValueNameSeparator_WrongFormatException ()
        {
            string frame = "BLUE200";

            DataWrapper actual = FrameParser.Decode(frame);

        }
    }
}
