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
using GUI.ViewModels;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for MultiplayerSettingsWindows.xaml
    /// </summary>
    public partial class MultiplayerSettingsWindows : Window
    {
        MP_SettingsViewModel vm;
        public MultiplayerSettingsWindows()
        {

            InitializeComponent();
            vm = new MP_SettingsViewModel();
            this.DataContext = vm;
            vm.requestGamesList();
            mazeProperties.startClicked += startClicked;
        }

        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            string gameName;
            if( (gameName=gamesComboBox.SelectedItem.ToString())!=null)
            {
                MultiPlayerWindow mpWindow = new MultiPlayerWindow(vm.joinGame(gameName));
                mpWindow.Show();
                this.Close();
            }
            
            
        }
        protected void startClicked(object sender, EventArgs e)
        {
            string gameName;
            if ((gameName = mazeProperties.MazeName) != null)
            {
                //MultiplayerWaitingScreen waitScreen = new MultiplayerWaitingScreen();
                //waitScreen.ShowDialog();
                //System.Windows.MessageBox.Show("Waiting for other player");
                MultiPlayerWindow mpWindow = new MultiPlayerWindow(vm.startGame(gameName));
                //waitScreen.Close();
                mpWindow.Show();
                this.Close();
            }

        }

        //private void btnStart_Click(object sender, RoutedEventArgs e)
        //{
        //    //vm.startGame();
        //    string gameName;
        //    if ((gameName = txtMazeName.Text.ToString()) != null)
        //    {
        //        //MultiplayerWaitingScreen waitScreen = new MultiplayerWaitingScreen();
        //        //waitScreen.ShowDialog();
        //        //System.Windows.MessageBox.Show("Waiting for other player");
        //        MultiPlayerWindow mpWindow = new MultiPlayerWindow(vm.startGame(gameName));
        //        //waitScreen.Close();
        //        mpWindow.Show();
        //        this.Close();
        //    }
        //}

        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{


        //}

        private void gamesComboBox_DropDownOpened(object sender, EventArgs e)
        {
            vm.requestGamesList();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Views.MainMenu menuWin = new Views.MainMenu();
            menuWin.Show();
            this.Close();
        }
    }
}
