using learnWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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

        //protected void startClicked(object sender, EventArgs e)
        //{

        //    SinglePlayerWindow win = new SinglePlayerWindow(mazeProperties.MazeName,mazeProperties.MazeRows,mazeProperties.MazeCols);
        //    win.Show();
        //    this.Closed -= Window_Closed;
        //    this.Close();

        //}
        protected void startClicked(object sender, EventArgs e)
        {
            SinglePlayerWindow win = new SinglePlayerWindow(mazeProperties.MazeName,
                mazeProperties.MazeRows, mazeProperties.MazeCols);
            if (CheckingConnection.isConnectionEstablished)
            {
                win.Show();
                this.Closed -= Window_Closed;
                this.Close();
            }
            else
            {

                string message = "Error connecting to server :[";
                string caption = "Warning!!!!!!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                // Displays the MessageBox.

                result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);

                if (result == System.Windows.Forms.DialogResult.OK)
                {

                    // Closes the parent form.

                    this.Close();

                }
            }


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
