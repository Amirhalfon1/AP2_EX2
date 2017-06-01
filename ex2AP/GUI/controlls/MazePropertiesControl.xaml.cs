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
    /// Interaction logic for MazePropertiesControl.xaml
    /// </summary>
    public partial class MazePropertiesControl : UserControl
    {
        public event EventHandler startClicked;

        protected void startClickedActuator(object sender, EventArgs e)
        {
            this.startClicked?.Invoke(this, e);
        }
        string name;
        int rows;
        int cols;

        public string MazeName
        {
            get { return name; }
            set { this.name = value ; }
        }
        public int MazeRows
        {
            get { return rows; }
            set { this.rows = value; }
        }
        public int MazeCols
        {
            get { return cols; }
            set { this.cols = value; }
        }

        //public static readonly DependencyProperty MazeNameProperty = DependencyProperty.Register("MazeName", typeof(string), typeof(MazePropertiesControl));
        //public string MazeName
        //{
        //    get { return (string)GetValue(MazeNameProperty); }
        //    set { SetValue(MazeNameProperty, value); }
        //}
        //public static readonly DependencyProperty MazeRowsProperty = DependencyProperty.Register("MazeRows", typeof(int), typeof(MazePropertiesControl));
        //public int MazeRows
        //{
        //    get { return (int)GetValue(MazeRowsProperty); }
        //    set { SetValue(MazeRowsProperty, value); }
        //}
        //public static readonly DependencyProperty MazeColsProperty = DependencyProperty.Register("MazeCols", typeof(int), typeof(MazePropertiesControl));
        //public int MazeCols
        //{
        //    get { return (int)GetValue(MazeColsProperty); }
        //    set { SetValue(MazeColsProperty, value); }
        //}

        public MazePropertiesControl()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            MazeName = txtMazeName.Text.ToString();
            MazeRows = int.Parse(txtRows.Text.ToString());
            MazeCols = int.Parse(txtCols.Text.ToString());
            startClickedActuator(this, null);
        }
    }
}
