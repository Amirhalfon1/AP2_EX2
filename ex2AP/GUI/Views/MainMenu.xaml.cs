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

        }

        private void singlePlayerBtn_Click(object sender, RoutedEventArgs e)
        {
            SinglePlayerSettingsWindow spSetWin = new SinglePlayerSettingsWindow();
            spSetWin.ShowDialog();
            this.Close();
            
        }

        private void multiPlayerBtn_Click(object sender, RoutedEventArgs e)
        {
            MultiplayerSettingsWindows mpSetWin = new MultiplayerSettingsWindows();
            mpSetWin.Show();

            this.Hide();
        }

        private void settingsBtn_Click(object sender, RoutedEventArgs e)
        {

            SettingsWindow settingsWin = new SettingsWindow();
            settingsWin.ShowDialog();
        }
    }
}
