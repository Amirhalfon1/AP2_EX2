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
    class StartGameCommand : ICommand
    {
        private Model model;
                
        public StartGameCommand(Model model)
        {
            this.model = model;
        }

        /// <summary>
        /// Executes the specified command - start game.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>string of the outcome</returns>
        public string Execute(string[] args, TcpClient client)
        {
            string gameName = args[0];

            if (model.isNameAlreadyExists(gameName))
            {
                return "name already exists. try another name" + '\n';
            }

            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            Game currentGame = model.startGame(gameName, rows, cols,client);
            //waiting for an other player to join the game.
            while (!currentGame.CanStart()) { }
            return currentGame.PlayedMaze().ToJSON() + '\n';
        }

    }

}