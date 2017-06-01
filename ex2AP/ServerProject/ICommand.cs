using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ServerProject
{
    interface ICommand
    {
        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>string with the exceution result</returns>
        string Execute(string[] args, TcpClient client = null);
    }
}