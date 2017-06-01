using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerProject.commands
{
    class CloseCommand : ICommand
    {
        private Model model;
        private IView clientHendler;

        public CloseCommand(IView clientHendler, Model model)
        {
            this.model = model;
            this.clientHendler = clientHendler;
        }

        /// <summary>
        /// Executes the specified command - close game.closes the game 
        /// and notifies the other player.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>string of the outcome</returns>
        public string Execute(string[] args, TcpClient client)
        {
            TcpClient clientToNotify = model.getOtherPlayerClient(client);
            //removing the game from the games being played
            model.close(client, clientToNotify);
            clientHendler.notifyClient("close", clientToNotify);
            Console.WriteLine("other player notified to close connection");
            Console.WriteLine("Closing client connection");

            return "close connection" + '\n';
        }
    }
}