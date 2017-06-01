using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace ServerProject
{
    public class Server
    {
        private int port;
        private TcpListener listener;
        private IView ch;
        private List<TcpClient> clientsList;

        public Server(int port, IView ch)
        {
            this.port = port;
            this.ch = ch;
            this.clientsList = new List<TcpClient>();
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine("Waiting for connections...");

            Task task = new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        clientsList.Add(client);
                        Console.WriteLine("Got new connection..!");
                        ch.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                    Console.WriteLine("Server keep listening");
                }
            });
            task.Start();
            task.Wait();

        }

        /// <summary>
        /// Stops the listner.
        /// </summary>
        public void Stop()
        {
            listener.Stop();
        }

    }
}