using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Timers;
using System.Threading;

namespace RosettaToRevit
{
    class RacketChannel
    {
        TcpListener server;
        TcpClient client;

        NetworkStream channel;

        System.Timers.Timer t;

        private char[] finalizer = { '!' };

        public RacketChannel()
        {
            server = new TcpListener(Dns.GetHostAddresses("localhost")[0], 53800);

            //Thread th = new Thread(AcceptConnections);
            //th.Start();

            StartRevitServer();
 
        }

        public void StartRevitServer()
        {
            t = new System.Timers.Timer();
            try
            {

                server.Start();
                t.Interval += 120000;
                t.Elapsed += new ElapsedEventHandler(timeoutEvent);
                t.Enabled = true;
                while (t.Enabled)
                {
                    if (server.Pending())
                    {
                        client = server.AcceptTcpClient();
                        channel = client.GetStream();
                        return;
                    }
                }

                server.Stop();
            }
            catch (Exception e)
            {
                client.Close();
                server.Stop();
            }
        }

        public void AcceptConnections()
        {
            while (true)
            {
                if (server.Pending())
                {
                    client = server.AcceptTcpClient();
                    channel = client.GetStream();
                }
                else
                {
                    Thread.Sleep(10);
                } 
            }
        }

        public void StopRevitServer()
        {
            try
            {
                server.Stop();
                client.Close();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error closing the connection.");
            }
        }

        private void timeoutEvent(object sender, ElapsedEventArgs e)
        {
            t.Enabled = false;
        }

        //ProtoBuff
        
        public bool ReadCommandBuff()
        {
            return channel.DataAvailable;
        }

        public bool IsConnected()
        {
            Socket socket = client.Client;
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException) { return false; }
        }

        public NetworkStream getChannel()
        {
            return channel;
        }
    }
}
