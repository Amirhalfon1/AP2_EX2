using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.ComponentModel;
using SearchAlgorithmsLib;
using Newtonsoft.Json.Linq;

namespace GUI
{
    class SinglePlayerViewModel : ViewModel
    {
        //Maze VM_maze;
        SinglePlayerWindowModel model;
        public SinglePlayerViewModel()
        {
            SinglePlayerWindowModel model = new SinglePlayerWindowModel();
            this.model = model;
            this.model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }
        public void StartGame(string mazeName, int mazeRows, int mazeCols)
        {
            string command = "generate ";
            command += mazeName + " ";
            command += mazeRows.ToString() + " ";
            command += mazeCols.ToString();
            this.model.Connect(command);
        }
        public Maze VM_maze
        {
            get { return model.Maze; }
            set
            {
                model.Maze = value;
                NotifyPropertyChanged("VM_maze");
            }
        }
        public string  VM_solution
        {
            get {
                //return model.Solution;
                string serializedSolution = model.Solution;
                if (serializedSolution != null)
                {
                    JObject solObj = JObject.Parse(serializedSolution);
                    return solObj["Solution"].ToString();
                }
                return null;

            }
            set
            {
                model.Solution = value;
                NotifyPropertyChanged("VM_solution");
            }
        }
        public void SolveGame(int alg)
        {
            string command = "solve ";
            command += model.Maze.Name + " ";
            command += alg.ToString();
            this.model.Connect(command);
        }
    }
}
