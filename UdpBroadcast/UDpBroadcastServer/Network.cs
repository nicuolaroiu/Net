using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UDpBroadcastServer
{
    static class Network
    {
        private static string broadcastIp = "192.168.0.255";
        private static int broadcastPort = 11000;

        public static void startServer(Label lbl)
        {
            Thread t = new Thread(new ParameterizedThreadStart(startListener));
            t.Start(lbl);
        }

        private static void startListener(object label)
        {
            //Label lbl = (Label)label;
            bool done = false;
             
            
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, broadcastPort);
            UdpClient listener = new UdpClient(groupEP);
            try
            {
                while (!done)
                {
                    //lbl.Text = "Waiting for broadcast";
                    MessageBox.Show("Waiting for broadcast.....");
                    byte[] bytes = listener.Receive(ref groupEP);

                    MessageBox.Show("Received broadcast from " + groupEP.Address +Environment.NewLine + groupEP.Port + Environment.NewLine +
                        Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                   // lbl.Text = "Received broadcast from " + groupEP.ToString() + Environment.NewLine +
                    //    Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                listener.Close();
            }
        }


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
