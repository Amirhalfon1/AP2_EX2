﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MazeLib;
using Newtonsoft.Json;
using SearchAlgorithmsLib;
using System.Collections.ObjectModel;

namespace GUI
{
    public class SinglePlayerWindowModel : Model
    {
        private Maze maze;
        private string solution;
        private string otherDirection;
        private string currentCommand;
        private ObservableCollection<string> gamesList;
        public SinglePlayerWindowModel()
        {
            GamesList = new ObservableCollection<string>();
        }
        public Maze Maze
        {
            get { return maze; }
            set
            {
                maze = value;
                NotifyPropertyChanged("maze");
            }
        }

        public string Solution
        {
            get { return solution; }
            set
            {
                solution = value;
                NotifyPropertyChanged("solution");
            }
        }
        public string OtherDirection
        {
            get { return otherDirection; }
            set
            {
                otherDirection = value;
                NotifyPropertyChanged("otherDirection");
            }
        }
        public string CurrentCommand
        {
            get { return currentCommand; }
            set
            {
                currentCommand = value;
            }
        }
        public ObservableCollection<string> GamesList
        {
            get { return gamesList; }
            set
            {
                gamesList = value;
                NotifyPropertyChanged("GamesList");
            }
        }
        public String MazeName
        {
            get { return Properties.Settings.Default.MazeName; }
            set { Properties.Settings.Default.MazeName = value; }
        }
        public int MazeRows
        {
            get { return Properties.Settings.Default.MazeRows; }
            set { Properties.Settings.Default.MazeRows = value; }
        }
        public int MazeCols
        {
            get { return Properties.Settings.Default.MazeCols; }
            set { Properties.Settings.Default.MazeCols = value; }
        }
        public void Connect(string command)
        {
            bool multiPlayerStarted = false;
            int port = int.Parse(ConfigurationSettings.AppSettings["port"]);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("Client has been connected");
            
            //string command = null;
            bool getNewCommand = true;
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            {
                //always working till the user himself exit the program via the GUI.
                while (true)
                {
                    bool isMultiplayerGame = true ;
                    //if (getNewCommand)
                    //{
                    //    command = Console.ReadLine();
                    //}
                    getNewCommand = true;
                    if(command == null)
                    {
                        continue;
                    }
                    if (!client.Connected)
                    {
                        client = new TcpClient();
                        client.Connect(ep);
                        stream = client.GetStream();
                        reader = new StreamReader(stream);
                        writer = new StreamWriter(stream);
                    }
                    //if ((command.Contains("join")) || (command.Contains("start")))
                    //{
                    //    isMultiplayerGame = true;
                    //}
                    writer.WriteLine(command);
                    writer.Flush();
                    string feedLine;
                    string feedback = "";
                    while (true)
                    {
                        feedLine = reader.ReadLine();
                        feedback += feedLine;
                        if (reader.Peek() == '@')
                        {
                            //Console.WriteLine("{0}", feedback);
                            feedback.TrimEnd('\n');
                            break;
                        }
                        //feedback +=
                        //Console.WriteLine("{0}", feedback);
                        //if the name of the game already exists, or the game we want to join
                        //does not exists - exits multiplayer mode.
                        if (feedback.Contains("try another name"))
                        {
                            isMultiplayerGame = false;
                        }
                    }
                    reader.ReadLine();
                    if(command.Contains("start") || command.Contains("join"))
                    {
                        Maze = Maze.FromJSON(feedback);
                        feedback = null;
                        command = null;
                       // break;
                    }
                    else if (command.Contains("generate"))
                    {
                        //Console.WriteLine(feedback);
                        isMultiplayerGame = false;
                        Maze = Maze.FromJSON(feedback);
                        feedback = null;
                        command = null;
                        break;
                    }
                    else if (command.Contains("solve"))
                    {
                        isMultiplayerGame = false;
                        Solution = feedback;
                        feedback = null;
                        command = null;
                        break;
                    }
                    else if (command.Contains("list"))
                    {
                        List<string> gamesLst = JsonConvert.DeserializeObject<List<string>>(feedback);
                        //GamesList = new ObservableCollection<string>(gamesLst);
                        foreach (string gameName in gamesLst)
                        {
                            if (!GamesList.Contains(gameName))
                            {
                                GamesList.Add(gameName);
                            }
                            
                        }
                        //if(game)
                        //ObservableCollection<string> obsCollection = new ObservableCollection<string>(TheArray);
                        //gamesList = feedback;
                        isMultiplayerGame = false;
                        feedback = null;
                        command = null;
                        break;
                    }
                    //}else if (command.Contains("start"))
                    //{
                    //    GamesList.Add("blablabla");
                    //}
                    if (isMultiplayerGame)
                    {
                        bool close = false;
                        Task writerTask = new Task(() =>
                        {
                            while (!close)
                            {

                                //varifies the player use just multiplayer commands in multi mode.
                                //command = Console.ReadLine();
                                //if (!command.Contains("close") && !command.Contains("play") &&
                                //    !close)
                                //{
                                //    Console.WriteLine("Multiplayer game.Please enter new command");
                                //    continue;
                                //}
                                if (CurrentCommand != null)
                                {
                                    Console.WriteLine("CURENTTTTTTTT COMANNNNNNNND");
                                    command = CurrentCommand;
                                    if (command.Contains("close"))
                                    {
                                        close = true;
                                        multiPlayerStarted = false;
                                    }
                                    writer.WriteLine(command);
                                    writer.Flush();
                                    currentCommand = null;
                                }
                                //if the player wants to close the session - it will be closed.


                                
                                //break;/////////////////////
                            }
                        });
                        Task readerTask = new Task(() =>
                        {
                            while (!close)
                            {
                                string serverFeedback = "";
                                string wholeFeedback = "";
                                while (true)
                                {
                                    serverFeedback = reader.ReadLine();
                                    
                                    if (reader.Peek() == '@')
                                    {
                                        {
                                            if (serverFeedback != "close")
                                            {
                                                //Console.WriteLine("{0}", serverFeedback);
                                            }
                                        }
                                        serverFeedback.TrimEnd('\n');
                                        break;
                                    }
                                    wholeFeedback += serverFeedback;
                                    //Console.WriteLine("{0}", serverFeedback);
                                }
                                reader.ReadLine();
                                //if(!multiPlayerStarted)
                                //{
                                //    Console.WriteLine(serverFeedback);
                                //    multiPlayerStarted = true;
                                //    Maze = Maze.FromJSON(serverFeedback);
                                //}
                                if (wholeFeedback.Contains("Direction"))
                                {
                                    OtherDirection = wholeFeedback;

                                }
                                if (feedback == "close")
                                {
                                    writer.WriteLine(feedback);
                                    writer.Flush();
                                    close = true;
                                    Console.WriteLine("other player closed connection");
                                    getNewCommand = false;
                                }
                            }
                        });
                        writerTask.Start();
                        readerTask.Start();
                        break;
                        //writerTask.Wait();
                        //readerTask.Wait();
                    }
                    else
                    {
                        
                        client.Close();
                        stream.Dispose();
                        writer.Dispose();
                        reader.Dispose();
                        break;

                    }

                }

            }
        }
    }
}
