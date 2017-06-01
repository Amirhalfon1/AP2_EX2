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
    /// Interaction logic for MultiPlayerWindow.xaml
    /// </summary>
    public partial class MultiPlayerWindow : Window
    {
        private MultiPlayerViewModel vm;
        private Task startMultiplayerGame;
        public MultiPlayerWindow(string command)
        {
            InitializeComponent();
            vm = new MultiPlayerViewModel();
           
            this.DataContext = vm;
            this.KeyDown += MyBoard.UserControl_KeyDown;
            MyBoard.playerMoved += notifyPlayCommand;
            //MultiplayerWaitingScreen waitScreen = new MultiplayerWaitingScreen();
            //waitScreen.ShowDialog();
            startMultiplayerGame = new Task(() =>
            {
                vm.StartGame(command);
            });

            startMultiplayerGame.Start();
            MultiplayerWaitingScreen waitWin = new MultiplayerWaitingScreen();
            waitWin.Show();
            startMultiplayerGame.Wait();
            //waitScreen.Close();
            waitWin.Close();
        }
        protected void notifyPlayCommand(object sender,EventArgs e)
        {
            vm.playToDirection(MyBoard.LastDirection);
        }

    }
}
