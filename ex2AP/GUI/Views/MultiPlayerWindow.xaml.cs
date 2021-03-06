﻿using System;
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
using System.Windows.Threading;
using System.Windows.Forms;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for MultiPlayerWindow.xaml
    /// </summary>
    public partial class MultiPlayerWindow : Window
    {
        private bool otherWon;
        private bool playerWon;
        private MultiPlayerViewModel vm;
        private Task startMultiplayerGame;
        public MultiPlayerWindow(string command)
        {
            InitializeComponent();
            vm = new MultiPlayerViewModel();
            otherWon = false;
            playerWon = false;
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
            MyBoard.ReachedToGoal += PlayerReachedToGoal;
            OtherBoard.ReachedToGoal += OtherReachedToGoal;
            vm.SignCloseDelegate(OtherClosedConnection);
        }
        protected void notifyPlayCommand(object sender, EventArgs e)
        {
            vm.playToDirection(MyBoard.LastDirection);
        }
        protected void PlayerReachedToGoal(object sender, EventArgs e)
        {
            playerWon = true;
            System.Windows.MessageBox.Show("You Won!");
            Views.MainMenu menuWin = new Views.MainMenu();
            vm.CloseGame();
            menuWin.Show();
            this.Close();
        }
        protected void OtherReachedToGoal(object sender, EventArgs e)
        {
            otherWon = true;
            System.Windows.MessageBox.Show("You Lost!");
            Views.MainMenu menuWin = new Views.MainMenu();
            menuWin.Show();
            this.Close();

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //string message = "Are you sure you want to finish the game?";
            //string caption = "Warning";
            //MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            //DialogResult result;

            //result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);

            //if (result == System.Windows.Forms.DialogResult.No)
            //{
            //    return;
            //}
            //if (result == System.Windows.Forms.DialogResult.Yes)
            //{

            //    this.Closing -= Window_Closing;
            //    vm.CloseGame();

            //}
            //vm.CloseGame();
            //MainMenu win = new MainMenu();
            //win.Show();
            //e.Cancel = true;
        }

        protected void OtherClosedConnection(object sender, EventArgs e)
        {
            if (otherWon || playerWon)
            {
                return;
            }
            System.Windows.Application.Current.Dispatcher.Invoke(
           DispatcherPriority.Background,
           new Action(() =>
           {
               MainMenu win = new MainMenu();
               win.Show();
               this.Close();
           }));
        }
        private void menuBtn_CLick(object sender, RoutedEventArgs e)
        {
            string message = "Are you sure you want to finish the game?";
            string caption = "Warning";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            if (result == System.Windows.Forms.DialogResult.Yes)
            {

                this.Closing -= Window_Closing;
                vm.CloseGame();

            }
        }
    }
}
