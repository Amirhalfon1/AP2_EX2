using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace GUI.ViewModels
{
    class MP_SettingsViewModel : ViewModel
    {
        private SinglePlayerWindowModel model;
        public MP_SettingsViewModel()
        {
            this.model = new SinglePlayerWindowModel();
            this.model.PropertyChanged +=
                                        delegate (Object sender, PropertyChangedEventArgs e)
                                        {
                                            NotifyPropertyChanged("VM_" + e.PropertyName);
                                        };
        }
        public int VM_MazeRows
        {
            get { return model.MazeRows; }
            set
            {
                model.MazeRows = value;
                NotifyPropertyChanged("VM_MazeRows");
            }
        }
        public int VM_MazeCols
        {
            get { return model.MazeCols; }
            set
            {
                model.MazeCols = value;
                NotifyPropertyChanged("VM_MazeCols");
            }
        }
        public string VM_MazeName
        {
            get { return model.MazeName; }
            set
            {
                model.MazeName = value;
                NotifyPropertyChanged("VM_MazeName");
            }
        }
        public ObservableCollection<string> VM_GamesList
        {
            get { return model.GamesList; }
            
        }
        public void requestGamesList()
        {
            this.model.Connect("list");
        }
        public string startGame(string gameName)
        {
            string startCommand = "start ";
            startCommand += gameName + " ";
            startCommand += VM_MazeRows + " ";
            startCommand += VM_MazeCols;
            return startCommand;
            //this.model.Connect(startCommand);
        }
        public string joinGame(string gameName)
        {
            string joinCommand = "join ";
            joinCommand += gameName;
            //this.model.Connect(joinCommand);
            return joinCommand;
        }
        //public void SaveSettings()
        //{
        //    model.SaveSettings();
        //}
    }
}
