using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeGeneratorLib;
using MazeLib;
using Newtonsoft.Json;
using SearchAlgorithmsLib;

namespace ServerProject.commands
{
    class JoinCommand : ICommand
    {
        private Model model;
        public JoinCommand(Model model)
        {
            this.model = model;
        }

        /// <summary>
        /// Executes the specified command - join game.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>string of the outcome</returns>
        public string Execute(string[] args, TcpClient client)
        {
            if (!model.isNameAlreadyExists(args[0]))
            {
                return "name does not exist. try another name" + '\n';
            }

            string joinedCompleted = model.join(args[0],client);

            return joinedCompleted + '\n';
        }
    }
}