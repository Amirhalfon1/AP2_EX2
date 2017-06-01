using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ServerProject
{
    public interface IView
    {
        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="client">The client.</param>
        void HandleClient(TcpClient client);

        /// <summary>
        /// Notifies the client.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="client">The client.</param>
        void notifyClient(string message, TcpClient client);
    }
}
