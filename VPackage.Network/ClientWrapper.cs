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
            IPAddress hostname;
            int port;

            try
            {
                hostname = IPAddress.Parse(this.hostname);
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                port = int.Parse(this.port);
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort) throw new ArgumentOutOfRangeException("Le port spécifié n'est pas valide");

            return new IPEndPoint(hostname, port);
        }
    }
}
