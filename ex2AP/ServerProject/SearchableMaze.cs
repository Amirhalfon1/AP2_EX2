using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;


namespace ServerProject
{
    class SearchableMaze : ISearchable<Position>
    {
        private Maze maze;
        private State<Position> initialState;
        private State<Position> goalState;
        // Dictionary between state and all his successors.
        private Dictionary<State<Position>,List<State<Position>>> adjacencyList;

        public SearchableMaze(Maze maze)
        {
            this.maze = maze;
            adjacencyList = new Dictionary<State<Position>, List<State<Position>>>();
            initializeSuccessors();
            this.initialState = new State<Position>(maze.InitialPos);
            this.goalState = new State<Position>(maze.GoalPos);
            
        }

        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>list of all the possible states</returns>
        public List<State<Position>> getAllPossibleStates(State<Position> s)
        {
            return this.adjacencyList[s];
        }

        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns>the goal state</returns>
        public State<Position> getGoalState()
        {
            return this.goalState;
        }

        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns>the initial state</returns>
        public State<Position> getInitialState()
        {
            return this.initialState;
        }

        //Method to initialize all the successors of each state.
        /// <summary>
        /// Initializes the successors.
        /// </summary>
        private void initializeSuccessors()
        {
            for(int row = 0;row < maze.Rows; ++row)
            {
                for(int col = 0;col < maze.Cols; ++col)
                {
                    List<State<Position>> successorsList = new List<State<Position>>();
                    State<Position> currentState = new State<Position>(new Position(row, col));
                    //Left succesor
                    if (isAccessable(row, col - 1)) {
                        State<Position> leftSuccessor = new State<Position>
                            (new Position(row, col - 1));
                        leftSuccessor.Cost = 1;
                        successorsList.Add(leftSuccessor);
                    }
                    //Right succesor
                    if (isAccessable(row, col + 1))
                    {
                        State<Position> rightSuccessor = new State<Position>
                            (new Position(row, col + 1));
                        rightSuccessor.Cost = 1;
                        successorsList.Add(rightSuccessor);
                    }
                    //Up succesor
                    if (isAccessable(row + 1, col))
                    {
                        State<Position> upSuccessor = new State<Position>
                            (new Position(row + 1, col));
                        upSuccessor.Cost = 1;
                        successorsList.Add(upSuccessor);
                    }
                    //Down succesor
                    if (isAccessable(row - 1, col))
                    {
                        State<Position> downSuccessor = new State<Position>
                            (new Position(row - 1, col));
                        downSuccessor.Cost = 1;
                        successorsList.Add(downSuccessor);
                    }
                    adjacencyList.Add(currentState, successorsList);
                }
            }
        }

        /// <summary>
        /// Determines whether the specified chamber is accessable.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        /// <returns>
        ///   <c>true</c> if the specified row is accessable; otherwise, <c>false</c>.
        /// </returns>
        private bool isAccessable(int row,int col)
        {
            if(row >= maze.Rows || row < 0 )
                return false;
            if (col >= maze.Cols || col < 0)
                return false;
            if (maze[row, col] == CellType.Wall)
                return false;
            return true;
        }
    }
}
