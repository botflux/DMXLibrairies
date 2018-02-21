using System.Net;
using System.Net.Sockets;

namespace VPackage.Network
{
    public class UdpState
    {
        private UdpClient u;
        private IPEndPoint e;

        public UdpClient U
        {
            get
            {
                return u;
            }

            set
            {
                u = value;
            }
        }

        public IPEndPoint E
        {
            get
            {
                return e;
            }

            set
            {
                e = value;
            }
        }
    }
}