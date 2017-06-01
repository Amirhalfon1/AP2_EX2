using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class SinglePlayerSettingsWindow : Window
    {
        private SP_SettingsViewModel vm;
        public SinglePlayerSettingsWindow()
        {
            InitializeComponent();
            SP_SettingsModel spSettingModel = new SP_SettingsModel();
            vm = new SP_SettingsViewModel(spSettingModel);
            this.DataContext = vm;
            mazeProperties.startClicked += startClicked;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveSettings();
           
        }

        protected void startClicked(object sender, EventArgs e)
        {

            SinglePlayerWindow win = new SinglePlayerWindow(mazeProperties.MazeName,mazeProperties.MazeRows,mazeProperties.MazeCols);
            win.Show();
            this.Close();

        }

        private void Window_Closed(object sender, EventArgs e)
        {
        
            Views.MainMenu menuWin = new Views.MainMenu();
            menuWin.Show();
            this.Close();
        }

        private void MazePropertiesControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
