using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using VPackage.Parser;

namespace VPackage.Parser.Test
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

        /// <summary>
        /// Cas: Le datawrapper est correcte
        /// Renvoie: Le datagramme encodé
        /// </summary>
        [TestMethod]
        public void Encode_RightDataWrapper_EncodedFrame ()
        {
            string frame = FrameParser.Encode(new DataWrapper("blue", 200));

            Assert.AreEqual("BLUE=200", frame);
        }
        
        /// <summary>
        /// Cas: Le datawrapper est null
        /// Renvoie: ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Encode_DataWrapperNull_ArgumentNullException ()
        {
            DataWrapper dw = null;
            FrameParser.Encode(dw);
        }

        /// <summary>
        /// Cas: La liste datawrapper est null
        /// Renvoie: ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Encode_DataWrapperListNull_ArgumentNullException()
        {
            System.Collections.Generic.List<DataWrapper> dw = null;
            FrameParser.Encode(dw);
        }

        /// <summary>
        /// Cas: Le tableau datawrapper est null
        /// Renvoie: ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Encode_DataWrapperArrayNull_ArgumentNullException()
        {
            DataWrapper[] dw = null;
            FrameParser.Encode(dw);
        }

        /// <summary>
        /// Cas: Le tableau datawrapper est correcte
        /// Renvoie: La trame encodée
        /// </summary>
        [TestMethod]
        public void Encode_RightDataWrapperArray_EncodedFrame()
        {
            DataWrapper[] dw = new DataWrapper[] {
                new DataWrapper("BLUE", 200),
                new DataWrapper("RED", 120)
            };
            string frame = FrameParser.Encode(dw);
            Assert.AreEqual("BLUE=200;RED=120", frame);
        }

        /// <summary>
        /// Cas: La liste datawrapper est correcte
        /// Renvoie: La trame encodée
        /// </summary>
        [TestMethod]
        public void Encode_RightDataWrapperList_ArgumentNullException()
        {
            System.Collections.Generic.List<DataWrapper> dw = new System.Collections.Generic.List<DataWrapper>()
            {
                new DataWrapper("BLUE", 200),
                new DataWrapper("RED", 120)
            };
            string frame = FrameParser.Encode(dw);
            Assert.AreEqual("BLUE=200;RED=120", frame);
        }

        /// <summary>
        /// Cas: Le tableau ne contient qu'un élément
        /// Renvoie: La trame encodée
        /// </summary>
        [TestMethod]
        public void Encode_ArrayWithOneElement_EncodedFrame ()
        {
            string excepted = "BLUE=100";
            DataWrapper[] dws = new DataWrapper[] { new DataWrapper("BLUE", 100) };

            string actual = FrameParser.Encode(dws);

            Assert.AreEqual(excepted, actual);
        }

        #endregion

        #region Decode test

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

        /// <summary>
        /// Cas: La trame est null
        /// Renvoie: ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Decode_NullFrame_ArgumentNullException ()
        {
            FrameParser.Decode(null);
        }

        /// <summary>
        /// Cas: La trame est vide
        /// Renvoie: ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Decode_EmptyFrame_ArgumentNullException()
        {
            FrameParser.Decode("");
        }

        /// <summary>
        /// Cas: La trame ne contient pas de séparateur nom/valeur
        /// Renvoie: WrongFormatException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongFormatException))]
        public void Decode_NoValueNameSeparator_WrongFormatException ()
        {
            string frame = "BLUE200";

            DataWrapper actual = FrameParser.Decode(frame);

        }
        #endregion

        #region Merge test
        /// <summary>
        /// Cas: Les deux trames sont correctes
        /// Renvoie: Les deux trames assemblées
        /// </summary>
        [TestMethod]
        public void Merge_RightParameters_MergedFrame ()
        {
            string s0 = "BLUE=210";
            string s1 = "RED=120";

            string merged = FrameParser.Merge(s0, s1);

            Assert.AreEqual("BLUE=210;RED=120", merged);
        }

        /// <summary>
        /// Cas: Le premier paramètre est null
        /// Renvoie: ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Merge_S0Wrong_ArgumentNullException()
        {
            string s0 = null;
            string s1 = "RED=120";

            FrameParser.Merge(s0, s1);
        }

        /// <summary>
        /// Cas: Le second paramètre est null
        /// Renvoie: ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Merge_S1Wrong_ArgumentNullException()
        {
            string s1 = null;
            string s0 = "RED=120";

            FrameParser.Merge(s0, s1);
        }
        #endregion
         
        #region DecodeArray test
        /// <summary>
        /// Cas: La trame est correcte
        /// Renvoie: La trame désencodée
        /// </summary>
        [TestMethod]
        public void DecodeArray_RightFrame_DecodedFrame ()
        {
            string frame = "RED=120;BLUE=100";

            List<DataWrapper> dws = FrameParser.DecodeArray(frame);
            List<DataWrapper> excepted = new List<DataWrapper>()
            {
                new DataWrapper("RED", 120),
                new DataWrapper("BLUE", 100)
            };

            bool actual = true;

            for (int i = 0; i < dws.Count; i++)
            {
                if (dws[i].Name != excepted[i].Name || 
                    dws[i].Value.ToString() != excepted[i].Value.ToString())
                {
                    actual = false;
                }
            }

            Assert.IsTrue(actual);
        }

        /// <summary>
        /// Cas: La trame encodée est nulle
        /// Renvoie: ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DecodeArray_NullFrame_ArgumentNullException ()
        {
            FrameParser.DecodeArray(null);
        }

        /// <summary>
        /// Cas: La trame encodée est vide
        /// Renvoie: ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DecodeArray_EmptyFrame_ArgumentNullException()
        {
            FrameParser.DecodeArray(string.Empty);
        }

        /// <summary>
        /// Cas: La trame contient trop de séparateur de trame
        /// Renvoie: WrongFormatException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongFormatException))]
        public void DecodeArray_FrameContainsTooMuchSepator_WrongFormatException()
        {
            FrameParser.DecodeArray("RED=120;BLUE=100;");
        }
        #endregion
    }
}
