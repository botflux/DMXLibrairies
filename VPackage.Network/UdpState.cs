using System.Net;
using System.Net.Sockets;

namespace VPackage.Network
{
    /// <summary>
    /// Encapsule un client Udp et un IPEndPoint
    /// </summary>
    public class UdpState
    {
        /// <summary>
        /// Client UDP
        /// </summary>
        private UdpClient u;
        /// <summary>
        /// Représente le point de terminaison du réseau 
        /// </summary>
        private IPEndPoint e;

        /// <summary>
        /// Renvoie ou renseigne le client UDP
        /// </summary>
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

        /// <summary>
        /// Renvoie ou renseigne le point de terminaison du réseau
        /// </summary>
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