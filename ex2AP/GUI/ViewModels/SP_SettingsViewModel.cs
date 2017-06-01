using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class SP_SettingsViewModel : ViewModel
    {
        private SP_SettingsModel model;
        public SP_SettingsViewModel(SP_SettingsModel model)
        {
            this.model = model;
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
        public void SaveSettings()
        {
            model.SaveSettings();
        }
    }
}
