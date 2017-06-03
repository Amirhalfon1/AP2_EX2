using System;
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
        int mazeRows;
        int mazeCols;
        public event EventHandler otherClosed;

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
        //public String MazeName
        //{
        //    get { return Properties.Settings.Default.MazeName; }
        //    set { MazeName = value; }
        //}
        public int MazeRows
        {
            get {
                    if(this.mazeRows == 0)
                    {
                        return Properties.Settings.Default.MazeRows;
                    }
                    return this.mazeRows;
                 }
            set { this.mazeRows = value; ; }
        }
        public int MazeCols
        {
            get {
                if (this.mazeCols == 0)
                {
                    return Properties.Settings.Default.MazeCols;
                }
                return this.mazeCols;
                 }
            set { this.mazeCols = value; }
        }
        protected void otherClosedActuator(object sender, EventArgs e)
        {
            this.otherClosed?.Invoke(this, e);
        }
        public void Connect(string command)
        {
            bool multiPlayerStarted = false;
          
            int port = Properties.Settings.Default.ServerPort;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(Properties.Settings.Default.ServerIP), port);
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
                                   // Console.WriteLine("CURENTTTTTTTT COMANNNNNNNND");
                                    command = CurrentCommand;
                                    if (command.Contains("close"))
                                    {
                                        close = true;
                                       // break;
                                        multiPlayerStarted = false;
                                    }
                                    writer.WriteLine(command);
                                    writer.Flush();
                                    currentCommand = null;
                                }
                                //if the player wants to close the session - it will be closed.


                                
                                //break;/////////////////////
                            }
                            Console.WriteLine("Writer Task Finished");
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
                                Console.WriteLine(wholeFeedback);
                                if (wholeFeedback.Contains("Direction"))
                                {
                                    OtherDirection = wholeFeedback;

                                }
                                if (wholeFeedback != null)
                                {
                                    if (wholeFeedback.StartsWith("close")) 
                                    {
                                        //writer.WriteLine(wholeFeedback);
                                        //writer.Flush();
                                        //close = true;
                                        CurrentCommand = "close";

                                        otherClosedActuator(this, null);

                                        //Console.WriteLine("other player closed connection");
                                        getNewCommand = false;
                                        break;
                                    }
                                }

                            }
                            Console.WriteLine("Reader Task Finished");
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
