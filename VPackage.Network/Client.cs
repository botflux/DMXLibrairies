﻿using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace VPackage.Network
{
    /// <summary>
    /// Représente un Client UDP
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Représente le Client Udp utilisé pour cette instance
        /// </summary>
        private UdpClient udpClient;
        /// <summary>
        /// Représente le point de terminaison réseau utilisé pour cette instance
        /// </summary>
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
        /// <exception cref="ArgumentNullException">Lever lors ce que les champs du ClientWrapper passé ne sont pas renseigner</exception>
        /// <exception cref="FormatException">Lever lors ce que les champs du ClientWrapper ne sont pas valide</exception>
        /// <exception cref="ArgumentOutOfRangeException">Lever lors ce que le port renseigner par le ClientWrapper n'est pas valide</exception>
        /// <exception cref="SocketException">Lever lors ce que le socket rencontre un problème</exception>
        public Client (string hostname, int port)
        {
            try
            {
                this.EndPoint = new IPEndPoint(IPAddress.Parse(hostname), port);
                this.udpClient = new UdpClient();
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw ex;
            }
            catch (SocketException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Initiliase une nouvelle instance de la classe Client
        /// </summary>
        /// <param name="wrapper">Wrapper</param>
        /// <exception cref="ArgumentNullException">Lever lors ce que les champs du ClientWrapper passé ne sont pas renseigner</exception>
        /// <exception cref="FormatException">Lever lors ce que les champs du ClientWrapper ne sont pas valide</exception>
        /// <exception cref="ArgumentOutOfRangeException">Lever lors ce que le port renseigner par le ClientWrapper n'est pas valide</exception>
        /// <exception cref="SocketException">Lever lors ce que le socket rencontre un problème</exception>
        public Client (ClientWrapper wrapper)
        {
            try
            {
                this.EndPoint = wrapper.ToIPEndPoint();
                this.udpClient = new UdpClient();
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw ex;
            }
            catch (SocketException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Envoie un message sur le réseau à l'adresse et port spécifiés dans EndPoint
        /// </summary>
        /// <param name="message">Message à envoyer</param>
        /// <exception cref="ArgumentNullException">Lever lors ce que le message spécifié en paramètre n'est pas valide</exception>
        /// <exception cref="ObjectDisposedException">Lever lors ce que le Client Udp est fermé</exception>
        /// <exception cref="SocketException">Lever lors ce que le Socket rencontre des problèmes (voir code erreur)</exception>
        public void Send (string message)
        {
            if (message == null || message == string.Empty) throw new ArgumentNullException("Le message passé en paramètre est nul ou vide");

            byte[] bs = Encoding.ASCII.GetBytes(message);
            int bc = Encoding.ASCII.GetByteCount(message);

            try
            {
                udpClient.Send(bs, bc, EndPoint);
            }
            catch (ObjectDisposedException ex)
            {
                throw ex;
            }
            catch (SocketException ex)
            {
                throw ex;
            }
        }
    }
}
