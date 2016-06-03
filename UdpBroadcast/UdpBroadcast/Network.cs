using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UdpBroadcast
{
    class Network
    {
        public static void sendUdpMessage(string ip, int port, string message)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress ipAddress = IPAddress.Parse(ip);

            byte[] buffer = Encoding.ASCII.GetBytes(message);
            IPEndPoint ep = new IPEndPoint(ipAddress, port);

            sock.SendTo(buffer, ep);
        }
    }
}
