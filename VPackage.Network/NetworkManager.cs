using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using VPackage.Json;

namespace VPackage.Network
{
    public class NetworkManager
    {
        /// <summary>
        /// Taille maximum des paquets
        /// </summary>
        private int mtu = 5;

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

        private Queue<DatagramPacket> packetBuffer = new Queue<DatagramPacket>();

        private bool useFragmentation = false;

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
        /// Renseigne si le serveur doit prendre en charge la fragmentation lors de la reception.
        /// </summary>
        public bool UseFragmentation
        {
            get
            {
                return useFragmentation;
            }

            set
            {
                useFragmentation = value;
            }
        }

        public int Mtu
        {
            get
            {
                return mtu;
            }

            set
            {
                mtu = value;
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

            if (UseFragmentation)
            {
                DatagramPacket p = JSONSerializer.Deserialize<DatagramPacket>(receiveString);
                if (p.IsFragmented)
                {
                    packetBuffer.Enqueue(p);

                    if (p.Index == p.PacketCount - 1)
                    {
                        string fullMessage = "";
                        while (packetBuffer.Count > 0)
                        {
                            fullMessage += packetBuffer.Dequeue().Data;
                        }
                        OnMessageReceived?.Invoke(fullMessage);
                    }
                }
                else
                {
                    OnMessageReceived?.Invoke(p.Data);
                }
            }
            else
            {
                OnMessageReceived?.Invoke(receiveString);
            }
            //if (OnMessageReceived != null) OnMessageReceived(receiveString);

            if (!stopListening)
                StartListening();
        }
        
        public void SendFragmented (string message)
        {
            // déduit le nombre de paquets qu'il y aura
            int packetCount = message.Length / Mtu;
            // déduit le nombre de charactères restant
            int lastPacketSize = message.Length % Mtu;

            // si il reste des charactères restant il faut compter un nouveau paquet qui ne sera pas plein
            //packetCount += (lastPacketSize != 0) ? 1 : 0;

            if (packetCount > 1 || (packetCount == 1 && lastPacketSize != 0))
            {
                List<string> buffer = new List<string>();
                for (int i = 0; i < packetCount; i++)
                {
                    int start = i * Mtu;
                    int length = Mtu;

                    buffer.Add(message.Substring(start, length));
                }

                buffer.Add(message.Substring(Mtu * packetCount, message.Length - (Mtu * packetCount)));
                // ajoute le packet restant si il y a un reste
                packetCount += (lastPacketSize != 0) ? 1 : 0;

                for (int i = 0; i < buffer.Count; i++)
                {
                    DatagramPacket p = new DatagramPacket()
                    {
                        Data = buffer[i],
                        Index = i,
                        IsFragmented = true,
                        PacketCount = packetCount
                    };

                    string json = JSONSerializer.Serialize<DatagramPacket>(p);
                    byte[] bs = Encoding.ASCII.GetBytes(json);
                    int bc = bs.Length;
                    udpClient.Send(bs, bc, sendEndPoint);
                    Console.WriteLine(json);
                }
                
            }
            else
            {
                DatagramPacket dp = new DatagramPacket()
                {
                    Index = -1,
                    PacketCount = -1,
                    IsFragmented = false,
                    Data = message
                };

                byte[] bs = Encoding.ASCII.GetBytes(JSONSerializer.Serialize<DatagramPacket>(dp));
                int bc = bs.Length;
                udpClient.Send(bs, bc, SendEndPoint);
            }

            Console.WriteLine("{0}; {1}", packetCount, lastPacketSize);
        }

        public void Send(string message)
        {

            byte[] bs = Encoding.ASCII.GetBytes(message);
            int bc = bs.Length;
            udpClient.Send(bs, bc, sendEndPoint);

            /*
             
             int messageLength = message.Length;

            List<string> data = new List<string>();
            
            if (messageLength > MTU)
            {
                int packetCount = messageLength / MTU;
                int lastByteCount = messageLength % MTU;

                Console.WriteLine("{0} packets, {1} bytes", packetCount, lastByteCount);

                for (int i = 0; i < packetCount - 1; i++)
                {
                    int offset = i * MTU;
                    int offsetEnd = (i + 1) * MTU;
                    Console.WriteLine("Offset: {0} / OffsetEnd: {1} /  Added: {2} / Length: {3}", offset, offsetEnd, (offset + offsetEnd),messageLength);

                    try
                    {
                        if (offset < messageLength && offsetEnd < messageLength)
                            data.Add(message.Substring(offset, offsetEnd - 1));
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message + " A");
                    }
                }

                //Console.WriteLine("Offset: {0} / End: {1} / Length: {2}", (packetCount - 1) * MTU, (packetCount - 1) * MTU + lastByteCount, messageLength);
                Console.WriteLine("Offset: {0} / End: {1} / Length: {2}", (packetCount - 2) * MTU, messageLength - 1, messageLength);
                try
                {
                    data.Add(message.Substring((packetCount - 2) * MTU, messageLength));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " B");
                }


                for (int i = 0; i < data.Count; i++)
                {
                    DatagramPacket packet = new DatagramPacket()
                    {
                        Index = i,
                        IsFragmented = true,
                        Data = data[i],
                        PacketCount = data.Count
                    };

                    byte[] bs = Encoding.ASCII.GetBytes(JSONSerializer.Serialize<DatagramPacket>(packet));
                    udpClient.Send(bs, bs.Length, sendEndPoint);
                }
            }
            else
            {
                DatagramPacket packet = new DatagramPacket()
                {
                    Index = -1,
                    PacketCount = -1,
                    IsFragmented = false,
                    Data = message
                };

                byte[] bs = Encoding.ASCII.GetBytes(JSONSerializer.Serialize<DatagramPacket>(packet));
                int bc = bs.Length;
                udpClient.Send(bs, bc, sendEndPoint);
            }
             */
        }

        [DataContract]
        private class DatagramPacket
        {
            [DataMember]
            private bool isFragmented;
            [DataMember]
            private int index;
            [DataMember]
            private int packetCount;
            [DataMember]
            private string data;

            public string Data { get { return data; } set { data = value; } }
            public int PacketCount { get { return packetCount; } set { packetCount = value; } }
            public int Index { get { return index; } set { index = value; } }
            public bool IsFragmented { get { return isFragmented; } set { isFragmented = value; } }
        }
    }
}
