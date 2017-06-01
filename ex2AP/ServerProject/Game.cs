using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using System.Net.Sockets;
using System.Net;

namespace ServerProject
{
    public class Game
    {

        private System.Net.Sockets.TcpClient gameCreatorPlayer, secondPlayer;
        private bool canStart;
        Maze playedMaze;
        string name;


        public Game(System.Net.Sockets.TcpClient firstPlayer,Maze maze)
        {
            gameCreatorPlayer = firstPlayer;
            playedMaze = maze;
            canStart = false;
            name = maze.Name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return this.name; }
        }

        //we declare a delegate to a function which looks like void gameEventHandler
        //(object source, EventArgs e).
        //public delegate void gameEventHandler(object source, EventArgs e);

        //public event gameEventHandler gameNeedsToStart;

        /// <summary>
        /// Joins another player to the game.
        /// </summary>
        /// <param name="OtherPlayer">The other player.</param>
        public void joinAnotherPlayer(System.Net.Sockets.TcpClient OtherPlayer)
        {
            secondPlayer = OtherPlayer;
            canStart = true;
            //notify
            //OngameNeedsToStart();
        }

        /// <summary>
        /// Gets the other player TcpClient.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        public TcpClient getOtherPlayer(TcpClient player)
        {
            if (player == gameCreatorPlayer)
                return secondPlayer;
            return gameCreatorPlayer;
        }

        //protected virtual void OngameNeedsToStart()
        //{
        //    if (gameNeedsToStart != null)
        //    {
        //        gameNeedsToStart(this, EventArgs.Empty);
        //    }
        //}

        /// <summary>
        /// returns the maze.
        /// </summary>
        /// <returns>that game's maze.</returns>
        public Maze PlayedMaze()
        {
            return playedMaze;
        }

        /// <summary>
        /// Determines whether the game can start.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can start; otherwise, <c>false</c>.
        /// </returns>
        public bool CanStart()
        {
            return canStart;
        }

        /// <summary>
        /// Gets the creator player.
        /// </summary>
        /// <returns></returns>
        public TcpClient getCreatorPlayer()
        {
            return gameCreatorPlayer;
        }
        

    }
}
