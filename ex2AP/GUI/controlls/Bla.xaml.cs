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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.controlls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        Label[] tiles;
        public UserControl1()
        {
            InitializeComponent();
            tiles = new Label[9];
            tiles[0] = l1;
            tiles[1] = l2;
            tiles[2] = l3;
            tiles[3] = l4;
            tiles[4] = l5;
            tiles[5] = l6;
            tiles[6] = l7;
            tiles[7] = l8;
            tiles[8] = l9;
        }
        public string Order
        {
            get
            {
                string s = "";
                foreach (Label l in tiles)
                    s += l.Content.ToString();
                return s;
            }
            set
            {
                string s = value;
                for (int i = 0; i < 9; i++)
                {
                    tiles[i].Content = s[i];
                    if (s[i] == ' ')
                        tiles[i].Background = Brushes.Gray;
                    else
                        tiles[i].Background = Brushes.White;
                }
            }
        }        private void l2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            char[] s = Order.ToCharArray();
            if (s[0] == ' ') s[0] = s[1];
            else if (s[2] == ' ') s[2] = s[1];
            else if (s[4] == ' ') s[4] = s[1];
            s[1] = ' ';
            Order = new String(s);
        }

    }
}
