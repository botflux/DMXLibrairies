using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Net;

namespace VPackage.Network.Test
{
    [TestClass]
    public class ClientWrapperTest
    {
        [TestMethod]
        public void ToIPEndPoint_AllCorrect_IPEndPoint ()
        {
            ClientWrapper wrapper = new ClientWrapper("127.0.0.1", "1200");
            IPEndPoint actual = wrapper.ToIPEndPoint();

            IPEndPoint excepted = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1200);

            Assert.AreEqual(excepted, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToIPEndPoint_HostnameNull_ArgumentNullException ()
        {
            ClientWrapper wrapper = new ClientWrapper()
            {
                Hostname = null
            };

            wrapper.ToIPEndPoint();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToIPEndPoint_HostnameEmpty_ArgumentNullException()
        {
            ClientWrapper wrapper = new ClientWrapper()
            {
                Hostname = ""
            };

            wrapper.ToIPEndPoint();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToIPEndPoint_PortNull_ArgumentNullException()
        {
            ClientWrapper wrapper = new ClientWrapper("127.0.0.1")
            {
                Port = null
            };

            wrapper.ToIPEndPoint();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToIPEndPoint_PortEmpty_ArgumentNullException()
        {
            ClientWrapper wrapper = new ClientWrapper("127.0.0.1")
            {
                Port = ""
            };

            wrapper.ToIPEndPoint();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToIPEndPoint_WrongHostnameFormat_FormatException()
        {
            ClientWrapper wrapper = new ClientWrapper("dzefezrgre", "5464");

            wrapper.ToIPEndPoint();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToIPEndPoint_WrongPortFormat_FormatException()
        {
            ClientWrapper wrapper = new ClientWrapper("127.0.0.1", "ad5464");

            wrapper.ToIPEndPoint();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToIPEndPoint_PortNotValid_ArgumentOutOfRangeException()
        {
            ClientWrapper wrapper = new ClientWrapper("127.0.0.1", "5555464");

            wrapper.ToIPEndPoint();
        }
    }
}
