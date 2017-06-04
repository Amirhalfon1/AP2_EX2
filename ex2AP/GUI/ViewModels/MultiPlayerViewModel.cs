using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.ComponentModel;
using Newtonsoft.Json.Linq;

namespace GUI.ViewModels
{
    class MultiPlayerViewModel : ViewModel
    {
        SinglePlayerWindowModel model;

        public MultiPlayerViewModel()
        {
            this.model = new SinglePlayerWindowModel();
            this.model.PropertyChanged +=
                                    delegate (Object sender, PropertyChangedEventArgs e)
                                    {
                                        NotifyPropertyChanged("VM_" + e.PropertyName);
                                    };
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

        public string VM_otherDirection
        {
            get
            {
                //return model.Solution;
                string serializedDirection = model.OtherDirection;
                if (serializedDirection != null)
                {
                    JObject solObj = JObject.Parse(serializedDirection);
                    model.OtherDirection = null;
                    return solObj["Direction"].ToString();

                }
                return null;

            }
            set
            {
                model.OtherDirection = value;
                NotifyPropertyChanged("VM_otherDirection");
            }
        }
        public void playToDirection(string direction)
        {
            string playCommand = "play ";
            playCommand += direction;
            model.CurrentCommand = playCommand;
        }


        //public Position PlayerPosition
        //{
        //    get { return model.Maze; }
        //    set
        //    {
        //        model.Maze = value;
        //        NotifyPropertyChanged("VM_maze");
        //    }
        //}
        //public Position OpponentPosition
        //{
        //    get { return model.Maze; }
        //    set
        //    {
        //        model.Maze = value;
        //        NotifyPropertyChanged("VM_maze");
        //    }
        //}
        public void StartGame(string command)
        {
            this.model.Connect(command);
        }
        public void CloseGame()
        {
            model.CurrentCommand = "close";
        }

        public void SignCloseDelegate(EventHandler eventHandler)
        {
            model.otherClosed += eventHandler;
        }

    }
}
