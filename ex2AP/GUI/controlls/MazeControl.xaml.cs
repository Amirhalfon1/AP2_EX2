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
using MazeLib;
using System.Windows.Threading;

namespace GUI.controlls
{
    /// <summary>
    /// Interaction logic for MazeControl.xaml
    /// </summary>
    public partial class MazeControl : UserControl
    {
        private static int solCurrentIndex;
        //Rectangle[,] rectArray;
        //string order;
        public event EventHandler playerMoved;
        public event EventHandler ReachedToGoal;
        private Rectangle[,] rectArray;
        private char[,] mazeAsChars;
        string lastDirection;
        //The player image as member for open it just once.
        private ImageBrush playerImage;
        //Maze maze;
        public MazeControl()
        {
            InitializeComponent();
            playerImage = new ImageBrush();
            playerImage.ImageSource = new BitmapImage(new Uri("../../controlls/player.jpg", UriKind.Relative));
            solCurrentIndex = 0;
        }

        public string LastDirection 
        {
            get { return lastDirection; }
            set { lastDirection = value; }
        }

        protected void userMovedActuator(object sender,EventArgs e)
        {
            this.playerMoved?.Invoke(this, e);
        }
        protected void ReachedToGoalActuator(object sender, EventArgs e)
        {
            this.ReachedToGoal?.Invoke(this, e);
        }
        private static void onOrderPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MazeControl)d).DrawMaze();
        }
        private static void onOtherPlayerMovePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MazeControl)d).MoveByString();
        }
        private static void onSolutionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MazeControl)d).SolveMazeAnimation();
        }

        public static readonly DependencyProperty OtherPlayerMoveProperty = DependencyProperty.Register("OtherPlayerMove", typeof(string), typeof(MazeControl),
                                                                                    new PropertyMetadata(onOtherPlayerMovePropertyChanged));
        public string OtherPlayerMove
        {
            get { return (string)GetValue(OtherPlayerMoveProperty); }
            set { SetValue(OtherPlayerMoveProperty, value); }
        }

        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register("Rows", typeof(int), typeof(MazeControl),
                                                                                    new PropertyMetadata(0));
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        public static readonly DependencyProperty ColsProperty = DependencyProperty.Register("Cols", typeof(int), typeof(MazeControl),
                                                                                                    new PropertyMetadata(0));
        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        public static readonly DependencyProperty OrderProperty = DependencyProperty.Register("Order",
            typeof(string), typeof(MazeControl), new PropertyMetadata(onOrderPropertyChanged));
        public string Order
        {
            get { return (string)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }
        public static readonly DependencyProperty CurrentPositionProperty = DependencyProperty.Register("CurrentPosition", typeof(Position), typeof(MazeControl));
        public Position CurrentPosition
        {
            get { return (Position)GetValue(CurrentPositionProperty); }
            set { SetValue(CurrentPositionProperty, value); }
        }
        public static readonly DependencyProperty GoalPositionProperty = DependencyProperty.Register("GoalPosition", typeof(Position), typeof(MazeControl));
        public Position GoalPosition
        {
            get { return (Position)GetValue(GoalPositionProperty); }
            set { SetValue(GoalPositionProperty, value); }
        }
        public static readonly DependencyProperty StartPositionProperty = DependencyProperty.Register("StartPosition", typeof(Position), typeof(MazeControl));
        public Position StartPosition
        {
            get { return (Position)GetValue(StartPositionProperty); }
            set { SetValue(StartPositionProperty, value); }
        }
        public static readonly DependencyProperty SolutionProperty = DependencyProperty.Register("Solution", typeof(string), typeof(MazeControl), new PropertyMetadata(onSolutionPropertyChanged));
        public string Solution
        {
            get { return (string)GetValue(SolutionProperty); }
            set { SetValue(SolutionProperty, value); }
        }


        private void DrawMaze()
        {
            //ImageBrush playerImageBrush = new ImageBrush();
            //playerImageBrush.ImageSource = new BitmapImage(new Uri("../../controlls/player.jpg", UriKind.Relative));
            ImageBrush targetImageBrush = new ImageBrush();
            targetImageBrush.ImageSource = new BitmapImage(new Uri("../../controlls/openedDoor.png", UriKind.Relative));
            int offset = -1;
            int mazeRows = Rows;
            int mazeCols = Cols;
            double rectSize = 0;
            if( (mazeRows == 0)||(mazeCols == 0))
            {
                return;
            }
            rectSize = Math.Min(300 / mazeRows, 300 / mazeCols);
            string mazeStr = Order;
            if(mazeStr == null)
            {
                return;
            }
            rectArray = new Rectangle[mazeRows, mazeCols];
            mazeAsChars = new char[mazeRows, mazeCols];
            for (int i = 0; i < mazeRows; i++)
            {
                for (int j = 0; j < mazeCols; j++)
                {
                    Rectangle myRectangle = new Rectangle();
                    //Rectangle currentRec = myRectangle.MazeRect;
                    myRectangle.Height = rectSize;
                    myRectangle.Width = rectSize;
                    rectArray[i, j] = myRectangle;
                    switch (mazeStr[++offset])
                    {
                        case '0':
                            rectArray[i,j].Fill = new SolidColorBrush(System.Windows.Media.Colors.White);
                            break;
                        case '1':
                            rectArray[i, j].Fill = new SolidColorBrush(System.Windows.Media.Colors.Black);
                            break;
                        case '*':

                            //rectArray[i, j].Fill = new SolidColorBrush(System.Windows.Media.Colors.Yellow);
                            rectArray[i, j].Fill = playerImage;
                            CurrentPosition = new Position(i, j);
                            break;

                        default:
                            rectArray[i, j].Fill = targetImageBrush;
                            break;
                    }
                    mazeAsChars[i, j] = mazeStr[offset];
                    myCanvas.Children.Add(rectArray[i, j]);
                    Canvas.SetLeft(rectArray[i, j], (i * rectSize));
                    Canvas.SetTop(rectArray[i, j], (j * rectSize));
                }
                offset+=2;
                
            }

            
        }

        private void MoveByString()
        {
            switch (OtherPlayerMove)
            {
                case "up":
                    movePlayerUp();
                    
                    break;
                case "down":
                    movePlayerDown();
                    break;
                case "left":
                    movePlayerLeft();
                    break;
                case "right":
                    movePlayerRight();
                    break;
            }
            if ((CurrentPosition.Col == GoalPosition.Col) && (CurrentPosition.Row == GoalPosition.Row))
            {
                //System.Windows.MessageBox.Show("You Won!");
                ReachedToGoalActuator(this, null);
            }
            //OtherPlayerMove = null;
        }

        private void SolveMazeAnimation()
        {
            CurrentPosition = new Position(StartPosition.Row, StartPosition.Col);
            int timeToWait = Solution.Length;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.25);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {

            if (solCurrentIndex == Solution.Length)
            {
                return;
            }
            //CurrentPosition = new Position(pos)
            char directionChar = Solution[solCurrentIndex];
            switch (directionChar)
            {
                case '0':
                    movePlayerUp();
                    break;
                case '1':
                    movePlayerDown();
                    break;
                case '2':
                    movePlayerLeft();
                    break;
                case '3':
                    movePlayerRight();
                    break;
            }

            solCurrentIndex++;
            if ((CurrentPosition.Row == GoalPosition.Row) && (CurrentPosition.Col == GoalPosition.Col))
            {
                //System.Windows.MessageBox.Show("You Won!");
                solCurrentIndex = 0;
                ReachedToGoalActuator(e, null);
                (sender as DispatcherTimer).Stop();
            }


        }
        public void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            Position oldPosition = new Position(CurrentPosition.Row, CurrentPosition.Col);
            switch (e.Key) {
                case Key.Up:
                    movePlayerUp();
                    break;
                case Key.Down:
                    movePlayerDown();
                    break;
                case Key.Left:
                    movePlayerLeft();
                    break;
                case Key.Right:
                    movePlayerRight();
                    break;
            }
            //If actually was movement - run the delegates!
            if ((CurrentPosition.Col != oldPosition.Col) || (CurrentPosition.Row != oldPosition.Row))
            {
                userMovedActuator(e, null);
            }
            if ( (CurrentPosition.Col == GoalPosition.Col)&& (CurrentPosition.Row == GoalPosition.Row) )
            {
                //System.Windows.MessageBox.Show("You Won!");
                ReachedToGoalActuator(e, null);
            }
            
            
        }

        private void movePlayerUp()
        {
            if ((CurrentPosition.Col - 1) < 0)
                return;
            Position oldPosition = new Position(CurrentPosition.Row, CurrentPosition.Col);
            if ( (mazeAsChars[CurrentPosition.Row, CurrentPosition.Col - 1] == '0') ||
                    (mazeAsChars[CurrentPosition.Row, CurrentPosition.Col - 1] == '#'))
            {
                rectArray[CurrentPosition.Row, CurrentPosition.Col].Fill = new SolidColorBrush(System.Windows.Media.Colors.White);
                rectArray[CurrentPosition.Row, CurrentPosition.Col - 1].Fill = playerImage;
                CurrentPosition = new Position(oldPosition.Row, oldPosition.Col - 1);
                LastDirection = "up";
            }
        }
        private void movePlayerDown()
        {
            if ((CurrentPosition.Col + 1) >= Rows)
                return;
            Position oldPosition = new Position(CurrentPosition.Row, CurrentPosition.Col);
            if ( (mazeAsChars[CurrentPosition.Row, CurrentPosition.Col + 1] == '0') ||
                        (mazeAsChars[CurrentPosition.Row, CurrentPosition.Col + 1] == '#') )
            {
                rectArray[CurrentPosition.Row, CurrentPosition.Col].Fill = new SolidColorBrush(System.Windows.Media.Colors.White);
                rectArray[CurrentPosition.Row, CurrentPosition.Col + 1].Fill = playerImage;
                CurrentPosition = new Position(oldPosition.Row, oldPosition.Col + 1);
                LastDirection = "down";
            }
        }
        private void movePlayerLeft()
        {
            if ((CurrentPosition.Row - 1) < 0)
                return;
            Position oldPosition = new Position(CurrentPosition.Row, CurrentPosition.Col);
            if ( (mazeAsChars[CurrentPosition.Row - 1 , CurrentPosition.Col] == '0') || 
                    (mazeAsChars[CurrentPosition.Row - 1, CurrentPosition.Col] == '#') )
            {
                rectArray[CurrentPosition.Row, CurrentPosition.Col].Fill = new SolidColorBrush(System.Windows.Media.Colors.White);
                rectArray[CurrentPosition.Row - 1, CurrentPosition.Col].Fill = playerImage;
                CurrentPosition = new Position(oldPosition.Row - 1, oldPosition.Col);
                LastDirection = "left";
            }
        }
        private void movePlayerRight()
        {
            if ((CurrentPosition.Row + 1) >= Cols)
                return;
            Position oldPosition = new Position(CurrentPosition.Row, CurrentPosition.Col);
            if ( (mazeAsChars[CurrentPosition.Row + 1, CurrentPosition.Col] == '0') ||
                    (mazeAsChars[CurrentPosition.Row + 1, CurrentPosition.Col] == '#') )
            {
                rectArray[CurrentPosition.Row, CurrentPosition.Col].Fill = new SolidColorBrush(System.Windows.Media.Colors.White);
                rectArray[CurrentPosition.Row + 1, CurrentPosition.Col].Fill = playerImage;
                CurrentPosition = new Position(oldPosition.Row + 1, oldPosition.Col);
                LastDirection = "right";
            }
        }

        public void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //var window = Window.GetWindow(this);
            //window.KeyDown += UserControl_KeyDown;
        }


    }

}

