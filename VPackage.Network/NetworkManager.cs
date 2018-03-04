using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace VPackage.Network
{
    public class NetworkManager
    {
        /// <summary>
        /// Client udp
        /// </summary>
        private UdpClient udpClient;
        /// <summary>
        /// Description du point de terminaison de reception
        /// </summary>
        private IPEndPoint receiveEndPoint;
        /// <summary>
        /// Description du point de terminaison d'envoie
        /// </summary>
        private IPEndPoint sendEndPoint;
        /// <summary>
        /// Renseigne si l'écoute doit être arreter ou pas
        /// </summary>
        private bool stopListening;

        /// <summary>
        /// Renseigne ou renvoie le point de terminaison de reception
        /// </summary>
        public IPEndPoint ReceiveEndPoint
        {
            get
            {
                return receiveEndPoint;
            }

            set
            {
                receiveEndPoint = value;
            }
        }

        /// <summary>
        /// Renseigne ou renvoie le point de terminaison d'envoie
        /// </summary>
        public IPEndPoint SendEndPoint
        {
            get
            {
                return sendEndPoint;
            }

            set
            {
                sendEndPoint = value;
            }
        }

        /// <summary>
        /// Lancer lors ce qu'un message est reçu
        /// </summary>
        public event Action<string> OnMessageReceived;

        /// <summary>
        /// Initialise une nouvelle instance de NetworkManager
        /// </summary>
        /// <param name="hostname">Nom d'hôte pour l'envoie</param>
        /// <param name="sendPort">Port d'envoie distant</param>
        /// <param name="receivePort">Port de réception distant</param>
        /// <param name="localPort">Port locale</param>
        /// <exception cref="ArgumentNullException">Le nom d'hôte est vide ou nul</exception>
        /// <exception cref="ArgumentOutOfRangeException">Un des port spécifié n'est pas valide</exception>
        /// <exception cref="FormatException">Le nom d'hôte n'a pas le bon format</exception>
        /// <exception cref="SocketException">Le socket a rencontré un problème</exception>
        public NetworkManager(string hostname, int sendPort, int receivePort, int localPort)
        {
            if (string.IsNullOrEmpty(hostname))
                throw new ArgumentNullException("Le nom d'hôte spécifié est vide");
            if (receivePort < IPEndPoint.MinPort || receivePort > IPEndPoint.MaxPort)
                throw new ArgumentOutOfRangeException(string.Format("Le port de reception spécifié \"{0}\" n'est pas comprit entre {1} et {2}", receivePort, IPEndPoint.MinPort, IPEndPoint.MaxPort));
            if (sendPort < IPEndPoint.MinPort || sendPort > IPEndPoint.MaxPort)
                throw new ArgumentOutOfRangeException(string.Format("Le port d'envoie spécifié \"{0}\" n'est pas comprit entre {1} et {2}", sendPort, IPEndPoint.MinPort, IPEndPoint.MaxPort));
            if (localPort < IPEndPoint.MinPort || localPort > IPEndPoint.MaxPort)
                throw new ArgumentOutOfRangeException(string.Format("Le port local spécifié \"{0}\" n'est pas comprit entre {1} et {2}", localPort, IPEndPoint.MinPort, IPEndPoint.MaxPort));

            IPAddress h;

            if (!IPAddress.TryParse(hostname, out h))
                throw new FormatException(string.Format("Le nom d'hôte {0} ne correspond pas au format IPv4", hostname));
            
            receiveEndPoint = new IPEndPoint(IPAddress.Any, receivePort);
            sendEndPoint = new IPEndPoint(h, sendPort);
            udpClient = new UdpClient(localPort);
        }

        public void StartListening()
        {
            stopListening = false;

            UdpState state = new UdpState();
            state.E = receiveEndPoint;
            state.U = udpClient;

            udpClient.BeginReceive(new AsyncCallback(ReceiveListenCallBack), state);
        }

        public void StopListening()
        {
            stopListening = true;
        }

        private void ReceiveListenCallBack(IAsyncResult ar)
        {
            UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).U;
            IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).E;

            byte[] receiveBytes = u.EndReceive(ar, ref e);
            string receiveString = Encoding.ASCII.GetString(receiveBytes);

            if (OnMessageReceived != null) OnMessageReceived(receiveString);

            if (!stopListening)
                StartListening();
        }
        
        public void Send(string message)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            int byteCount = bytes.Length;

            udpClient.Send(bytes, byteCount, sendEndPoint);
        }
    }
}
