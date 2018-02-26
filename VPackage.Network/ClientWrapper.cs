using System;
using System.Net;

namespace VPackage.Network
{
    /// <summary>
    /// Représente un client
    /// </summary>
    public class ClientWrapper
    {
        /// <summary>
        /// Nom d'hôte du client
        /// </summary>
        private string hostname;

        /// <summary>
        /// Port du client
        /// </summary>
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
        /// <returns>La terminaison réseau correspondant à cette instance</returns>
        /// <exception cref="ArgumentNullException">Lever lors ce que le nom d'hôte ou le port n'est pas spécifié</exception>
        /// <exception cref="FormatException">Lever lors ce que le nom d'hôte ou le port n'a pas le bon format</exception>
        /// <exception cref="ArgumentOutOfRangeException">Lever lors ce que le port spécifié ne se trouve pas entre 0 et 65 535</exception>
        public IPEndPoint ToIPEndPoint ()
        {
            if (hostname == null || hostname == string.Empty)
                throw new ArgumentNullException("Le nom d'hôte n'est pas spécifié ou vide");
            if (port == null || port == string.Empty)
                throw new ArgumentNullException("Le port n'est pas spécifié ou vide");


            IPAddress translatedHostname;
            int translatedPort;

            if (!IPAddress.TryParse(hostname, out translatedHostname))
                throw new FormatException("Le nom d'hôte spécifié n'est pas valide");
            if (!int.TryParse(port, out translatedPort))
                throw new FormatException("Le port spécifié n'est pas valide");
            if (translatedPort < IPEndPoint.MinPort || translatedPort > IPEndPoint.MaxPort)
                throw new ArgumentOutOfRangeException(string.Format("Le port spécifié ne se trouve pas entre {0} et {1}", IPEndPoint.MinPort, IPEndPoint.MaxPort));

            return new IPEndPoint(translatedHostname, translatedPort);
        }
    }
}
