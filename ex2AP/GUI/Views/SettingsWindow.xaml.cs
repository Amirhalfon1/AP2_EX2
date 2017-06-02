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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private SettingsViewModel vm;
        public SettingsWindow()
        {
            InitializeComponent();
            vm = new SettingsViewModel();
            this.DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveSettings();
            MainMenu win = (MainMenu)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.Reload();
            Properties.Settings.Default.Reload();
            MainMenu win = (MainMenu)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
    }
}
