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
using Newtonsoft.Json.Linq;

namespace ServerProject.commands
{
    class listCommand : ICommand
    {
        private Model model;
        public listCommand(Model model)
        {
            this.model = model;
        }

        /// <summary>
        /// Executes the specified command - get list of games.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>string of the outcome</returns>
        public string Execute(string[] args, TcpClient client)
        {

            string[] listOfGames = model.getListOfGames();
            Console.WriteLine(JsonConvert.SerializeObject(listOfGames));
            return JsonConvert.SerializeObject(listOfGames)+'\n';
        }
    }
}