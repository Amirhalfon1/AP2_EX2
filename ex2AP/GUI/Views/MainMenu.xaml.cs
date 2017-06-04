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
using GUI.Views;
using learnWPF.Models;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
            //Properties.Settings.Default.ServerIP = "127.0.0.1";
            //Properties.Settings.Default.ServerPort = 49251;
        }

        private void singlePlayerBtn_Click(object sender, RoutedEventArgs e)
        {
            SinglePlayerSettingsWindow spSetWin = new SinglePlayerSettingsWindow();
            spSetWin.Show();
            this.Close();
            
        }

        private void multiPlayerBtn_Click(object sender, RoutedEventArgs e)
        {
            MultiplayerSettingsWindows mpSetWin = new MultiplayerSettingsWindows();
            if (CheckingConnection.isConnectionEstablished)
            {
                mpSetWin.Show();

                this.Close();
            }
        }

        private void settingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWin = new SettingsWindow();
            settingsWin.ShowDialog();
        }
    }
}
