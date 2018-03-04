using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace VPackage.Network
{
    // TODO : Mettre en place un système pour pouvoir arrêter l'écoute du serveur si besoin
    
    /// <summary>
    /// Représente un Serveur UDP
    /// </summary>
    [Obsolete("Utilisé la classe NetworkManager", false)]
    public class Server
    {
        /// <summary>
        /// Evènement appelé lors ce que le serveur reçoit un nouveau message
        /// </summary>
        public event Action<string> OnMessageReceived;

        /// <summary>
        /// Client Udp utilisé pour cette instance
        /// </summary>
        private UdpClient udpClient;

        /// <summary>
        /// Terminaison réseau utilisée pour cette instance
        /// </summary>
        private IPEndPoint endPoint;

        /// <summary>
        /// Renvoie ou renseigne la terminaison réseau
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
        /// Initialise une nouvelle instance de Server
        /// </summary>
        /// <param name="listenPort">Port d'écoute du serveur</param>
        /// <exception cref="ArgumentOutOfRangeException">Le port renseigné n'est pas valide</exception>
        /// <exception cref="SocketException">Lever lors ce qu'il y a une erreur lors de la création du socket (voir code d'erreur)</exception>
        public Server (int listenPort)
        {
            try
            {
                udpClient = new UdpClient(listenPort);
                EndPoint = new IPEndPoint(IPAddress.Any, listenPort);
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
        /// Commence l'écoute du Server
        /// </summary>
        public void StartListen ()
        {
            UdpState s = new UdpState();

            s.E = EndPoint;
            s.U = udpClient;
            
            udpClient.BeginReceive(new AsyncCallback(ReceiveCallBack), s);
        }

        /// <summary>
        /// Appelée lors ce que le serveur est lancé
        /// </summary>
        /// <param name="ar"></param>
        /// <exception cref="ArgumentException">Lever lors ce que cette méthode n'est pas appelé depuis BeginReceive</exception>
        /// <exception cref="ObjectDisposedException">Lever lors ce que le Socket à déjà été fermé</exception>
        /// <exception cref="SocketException">Lever lors ce que le Socket rencontre un problème (voir code d'erreur)</exception>
        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                // récupère le client udp
                UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).U;
                // récupère la terminaison réseau
                IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).E;

                // stock les octets reçu sur le réseau
                byte[] receiveBytes = u.EndReceive(ar, ref e);
                // convertit le tableau d'octet en chaîne de caractères
                string receiveString = Encoding.ASCII.GetString(receiveBytes);

                // appele l'évènement de reception de message
                if (OnMessageReceived != null) OnMessageReceived(receiveString);

                // redémarre l'écoute
                StartListen();
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (ObjectDisposedException ex)
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
    }
}
