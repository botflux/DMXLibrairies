using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace VPackage.Network
{
    /// <summary>
    /// Représente un client
    /// </summary>
    public class ClientWrapper
    {
        private string hostname;
        private string port;

        /// <summary>
        /// Renseigne l'adresse IP de ce client
        /// </summary>
        public string Hostname
        {
            get { return this.hostname; }
            set { this.hostname = value; }
        }

        /// <summary>
        /// Renseigne le port de ce client
        /// </summary>
        public string Port
        {
            get { return this.port; }
            set { this.port = value; }
        }

        /// <summary>
        /// Initialise une nouvelle instance de ClientWrapper
        /// </summary>
        /// <param name="hostname">Hôte distant</param>
        /// <param name="port">Port distant</param>
        public ClientWrapper (string hostname = "", string port = "")
        {
            Hostname = hostname;
            Port = port;
        }

        /// <summary>
        /// Retourne cette classe sous forme de IPEndPoint
        /// </summary>
        /// <returns></returns>
        public IPEndPoint ToIPEndPoint ()
        {
            try
            {
                return new IPEndPoint(IPAddress.Parse(this.hostname), int.Parse(this.port));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
