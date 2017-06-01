using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Sockets;

namespace ServerProject
{
    public class Model : IModel
    {
        //contains all the BFS solutions
        private Dictionary<string, string> mazeBFSSolutions;
        //contains all the DFS solutions
        private Dictionary<string, string> mazeDFSSolutions;
        //contains all the mazes
        private Dictionary<string, Maze> multiplayerMazesDictionary;
        private Dictionary<string, Maze> singleplayerMazesDictionary;
        //contains all the games waiting for an other player.
        private Dictionary<string, Game> gamesLobby;
        //contains all the games that are being played.
        private Dictionary<TcpClient, Game> gamesBeingPlayed;
        //contains all the names af all the mazes names to prevent duplication.
        private HashSet<string> multiplayerMazesAndGamesNames;
        private HashSet<string> singleplayerMazesAndGamesNames;
        DFSMazeGenerator mazeGenerator;
        BFS<Position> bfsSolver;
        DFS<Position> dfsSolver;

        public Model()
        {
            mazeGenerator = new DFSMazeGenerator();
            mazeBFSSolutions = new Dictionary<string, string>();
            mazeDFSSolutions = new Dictionary<string, string>();
            multiplayerMazesDictionary = new Dictionary<string, Maze>();
            singleplayerMazesDictionary = new Dictionary<string, Maze>();
            gamesLobby = new Dictionary<string, Game>();
            gamesBeingPlayed = new Dictionary<TcpClient, Game>();
            bfsSolver = new BFS<Position>();
            dfsSolver = new DFS<Position>();
            //hash set that resposible to aware of two games and mazes with the same name.
            multiplayerMazesAndGamesNames = new HashSet<string>();
            singleplayerMazesAndGamesNames = new HashSet<string>();
        }


        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns>maze</returns>
        public Maze GenerateMaze(string name, int rows, int cols)
        {
            Maze maze = mazeGenerator.Generate(rows, cols);
            singleplayerMazesDictionary.Add(name, maze);
            singleplayerMazesAndGamesNames.Add(name);
            return maze;
        }


        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="firstPlayer">The first player.</param>
        /// <returns>a new game.</returns>
        public Game startGame(string name, int rows, int cols, TcpClient firstPlayer)
        {
            Maze maze = mazeGenerator.Generate(rows, cols);
            maze.Name = name;
            multiplayerMazesAndGamesNames.Add(name);
            var newGame = new Game(firstPlayer, maze); //publisher
            //adding the new maze to the maze dictionary.
            Console.WriteLine("the name is {0} ",name);
            multiplayerMazesDictionary.Add(name, maze);
            Console.WriteLine("before add to lobby");
            //adding the game to the lobby till someone asks to join
            gamesLobby.Add(name, newGame);
            return newGame;
        }

        /// <summary>
        /// Joins a player to an active game.
        /// </summary>
        /// <param name="gameName">Name of the game.</param>
        /// <param name="otherPlayer">The other player.</param>
        /// <returns>string of the played game.</returns>
        public string join(string gameName, System.Net.Sockets.TcpClient otherPlayer)
        {
            Game currentGAME = gamesLobby[gameName];

            /*pop out the game out of the lobby and moves it
            to another dictonery of a being played games */
            gamesLobby.Remove(gameName);
            gamesBeingPlayed.Add(otherPlayer, currentGAME);
            gamesBeingPlayed.Add(currentGAME.getCreatorPlayer(), currentGAME);

            //notify that another player joined the game.
            currentGAME.joinAnotherPlayer(otherPlayer);

            return currentGAME.PlayedMaze().ToJSON();
        }

        /// <summary>
        /// Plays the specified direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="otherPlayer">The other player.</param>
        /// <returns>string of the direction that was played.</returns>
        public string play(string direction, TcpClient otherPlayer)
        {
            Game currentGAME = gamesBeingPlayed[otherPlayer];
            TcpClient playerToNotify = currentGAME.getOtherPlayer(otherPlayer);
            return currentGAME.PlayedMaze().ToJSON();
        }

        /// <summary>
        /// Closes the game.
        /// </summary>
        /// <param name="firstPlayer">The first player.</param>
        /// <param name="secondPlayer">The second player.</param>
        public void close(TcpClient firstPlayer, TcpClient secondPlayer)
        {
            Game currentGame = gamesBeingPlayed[firstPlayer];
            //removing the maze of the game from the maze dictionery.
            multiplayerMazesDictionary.Remove(currentGame.Name);
            multiplayerMazesAndGamesNames.Remove(currentGame.Name);
            //removing both of the players from the list.
            gamesBeingPlayed.Remove(firstPlayer);
            gamesBeingPlayed.Remove(secondPlayer);
         }

        /// <summary>
        /// Gets the other player of the game.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public TcpClient getOtherPlayerClient(TcpClient client)
        {
            Game currentGAME = gamesBeingPlayed[client];
            TcpClient otherPlayer = currentGAME.getOtherPlayer(client);
            return otherPlayer;

        }

        /// <summary>
        /// Gets the name of the player that playes that specific game.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>the name of the game</returns>
        public string getClientGameName(TcpClient client)
        {
            Game currentGAME = gamesBeingPlayed[client];
            return currentGAME.Name;

        }

        /// <summary>
        /// Gets the list of games.
        /// </summary>
        /// <returns>list of games.</returns>
        public string[] getListOfGames()
        {
            return gamesLobby.Keys.ToArray();
        }


        /// <summary>
        /// Solves the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="algo">The algo.</param>
        /// <returns>the solution by the desired algo</returns>
        public string solve(string name, int algo)
        {
            if (algo == 0)
            {
                //checks if the  BFS solution is already exists.
                if (mazeBFSSolutions.ContainsKey(name))
                {
                    return mazeBFSSolutions[name];
                }

            }
            else if (mazeDFSSolutions.ContainsKey(name))
            //checks if the DFS solution is already exists.
            {
                return mazeDFSSolutions[name];
            }


            //if there is no existing solution - solving it. 
            Maze maze = singleplayerMazesDictionary[name];
            SearchableMaze searchableMaze = new SearchableMaze(maze);
            JObject solveObj = new JObject();
            solveObj["Name"] = name;

            if (algo == 0)
            {
                Solution<Position> bfsSolution = new Solution<Position>();
                bfsSolution = bfsSolver.search(searchableMaze);
                //Json
                solveObj["Solution"] = calculateSolution(bfsSolution.PathToGoal());
                solveObj["NodesEvaluated"] = bfsSolver.getNumberOfNodesEvaluated().ToString();
                mazeBFSSolutions.Add(name, solveObj.ToString());
            }
            else
            {
                Solution<Position> dfsSolution = new Solution<Position>();
                dfsSolution = dfsSolver.search(searchableMaze);
                //Json                   
                solveObj["Solution"] = calculateSolution(dfsSolution.PathToGoal());
                solveObj["NodesEvaluated"] = dfsSolver.getNumberOfNodesEvaluated().ToString();
                mazeDFSSolutions.Add(name, solveObj.ToString());
            }
            //adding the solution to the dictionary.
           
            return solveObj.ToString();
        }

        /// <summary>
        /// Determines whether [is name already exists] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if [is name already exists] [the specified name]; otherwise, <c>false</c>.
        /// </returns>
        public bool isNameAlreadyExists(string name)
        {
            if (multiplayerMazesAndGamesNames.Contains(name))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether [is name already exists] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if [is name already exists] [the specified name]; otherwise, <c>false</c>.
        /// </returns>
        public bool singleGameExists(string name)
        {
            if (singleplayerMazesAndGamesNames.Contains(name))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Calculates the solution.
        /// </summary>
        /// <param name="pathToGoal">The path to goal.</param>
        /// <returns>return the solution as we want</returns>
        private string calculateSolution(List<State<Position>> pathToGoal)
        {
            StringBuilder pathToReturn = new StringBuilder();

            List<Position> newList = new List<Position>();

            foreach (State<Position> position in pathToGoal)
            {
                //if we came from the same row

                if (position.getInstance().Row == position.CameFrom.getInstance().Row)
                {
                    if (position.getInstance().Col > position.CameFrom.getInstance().Col)
                    {
                        pathToReturn.Append("1");
                    }
                    else
                    {
                        pathToReturn.Append("0");
                    }
                }
                //if we came from the same col
                else if (position.getInstance().Row != position.CameFrom.getInstance().Row)
                {
                    if (position.getInstance().Row > position.CameFrom.getInstance().Row)
                    {
                        pathToReturn.Append("3");
                    }
                    else
                    {
                        pathToReturn.Append("2");
                    }
                }

            }

            //returning it back reversed
            char[] arrayToReverse = pathToReturn.ToString().ToCharArray();
            Array.Reverse(arrayToReverse);
            return new string(arrayToReverse);
        }
    }
}



