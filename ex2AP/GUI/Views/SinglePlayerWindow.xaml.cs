using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MazeLib;
using GUI.controlls;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class SinglePlayerWindow : Window
    {
        SinglePlayerViewModel vm;
        public SinglePlayerWindow(string mazeName , int mazeRows, int mazeCols)
        {
            InitializeComponent();
            vm = new SinglePlayerViewModel();
            this.DataContext = vm;
            vm.StartGame(mazeName,mazeRows,mazeCols);
            //this.Loaded += myMaze.UserControl_Loaded;
            this.KeyDown += myMaze.UserControl_KeyDown;
            myMaze.ReachedToGoal += reachedToGoal;


        }
        //private void btnSolve_Click(object sender, RoutedEventArgs e)
        //{
            
        //    vm.SolveGame((char)Properties.Settings.Default.SearchAlgorithm);
        //    //vm.SaveSettings();
        //    //SinglePlayerWindow win = new SinglePlayerWindow();

        //    //win.Show();
        //    //this.Close();
        //}
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //Views.MainMenu menuWin = new Views.MainMenu();
            //menuWin.Show();
            //this.Close();
            string message = "Are you sure you want to back to menu?";
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
                Views.MainMenu menuWin = new Views.MainMenu();
                this.Close();
                menuWin.Show();
               
            }

        }
        protected void reachedToGoal(object sender, EventArgs e)
        {
            System.Windows.MessageBox.Show("You Won!");
            Views.MainMenu menuWin = new Views.MainMenu();
            menuWin.Show();
            this.Close();
            
        }

       private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
           
            string message = "Are you sure you want to solve the maze?";
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

                myMaze.RestartGame();

            }
        }
        private void btnSolve_Click(object sender, RoutedEventArgs e)
        {

            string message = "Are you sure you want to solve?";
            string caption = "solve";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                //Views.MainMenu menuWin = new Views.MainMenu();
                vm.SolveGame((char)Properties.Settings.Default.SearchAlgorithm);
                //menuWin.Show();
                //menuWin.ShowDialog();
            }
            //vm.SolveGame((char)Properties.Settings.Default.SearchAlgorithm);
            //vm.SaveSettings();
            //SinglePlayerWindow win = new SinglePlayerWindow();

            //win.Show();
            //this.Close();
        }
    }
}
