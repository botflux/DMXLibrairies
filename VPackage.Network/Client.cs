using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace VPackage.Network
{
    public class Client
    {
        private UdpClient udpClient;
        private IPEndPoint endPoint;

        /// <summary>
        /// Renseigne la configuration de la connexion
        /// </summary>
        public IPEndPoint EndPoint
        {
            get
            {
                return endPoint;
            }

            set
            {
                endPoint = value;
            }
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe Client
        /// </summary>
        /// <param name="hostname">Adresse IP destinataire</param>
        /// <param name="port">Port distant</param>
        public Client (string hostname, int port)
        {
            this.EndPoint = new IPEndPoint(IPAddress.Parse(hostname), port);
            this.udpClient = new UdpClient();
        }

        /// <summary>
        /// Initiliase une nouvelle instance de la classe Client
        /// </summary>
        /// <param name="wrapper">Wrapper</param>
        public Client (ClientWrapper wrapper)
        {
            this.EndPoint = wrapper.ToIPEndPoint();
            this.udpClient = new UdpClient();
        }

        /// <summary>
        /// Envoie un message sur le réseau à l'adresse et port spécifiés dans EndPoint
        /// </summary>
        /// <param name="message">Message à envoyer</param>
        public void Send (string message)
        {
            byte[] bs = Encoding.ASCII.GetBytes(message);
            int bc = Encoding.ASCII.GetByteCount(message);

            udpClient.Send(bs, bc, EndPoint);
        }
    }
}
