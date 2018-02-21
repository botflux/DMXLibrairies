using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VPackage.Network
{
    public class Server
    {
        public event Action<string> OnMessageReceived;

        private UdpClient udpClient;
        private IPEndPoint endPoint;

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

        public Server (int listenPort)
        {
            udpClient = new UdpClient(listenPort);
            EndPoint = new IPEndPoint(IPAddress.Any, listenPort);
        }

        public void StartListen ()
        {
            UdpState s = new UdpState();

            s.E = EndPoint;
            s.U = udpClient;
            
            udpClient.BeginReceive(new AsyncCallback(ReceiveCallBack), s);
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).U;
            IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).E;

            byte[] receiveBytes = u.EndReceive(ar, ref e);
            string receiveString = Encoding.ASCII.GetString(receiveBytes);

            if (OnMessageReceived != null) OnMessageReceived(receiveString);
            
            StartListen();

        }
    }
}
