using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;
using MazeLib;

namespace SearchAlgorithmsLib
{
    public class BFS<T> : PrioritySearcher<T>
    {

        /// <summary>
        /// Searches the specified searchable by bfs.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns>the Solution of the bfs algo</returns>
        public override Solution<T> search(ISearchable<T> searchable)
        {
            HashSet<State<T>> closedSet = new HashSet<State<T>>();
            State<T> initialState = searchable.getInitialState();
            addToOpenList(initialState);
            while (openList.Count > 0)
            {
                State<T> currentState = popOpenList();
                closedSet.Add(currentState);
                if (currentState.Equals(searchable.getGoalState()))
                    return backTrace(initialState,currentState);
                List<State<T>> succerssors =
                    searchable.getAllPossibleStates(currentState);
                foreach (State<T> currentSuccessor in succerssors)
                {
                    if (!closedSet.Contains(currentSuccessor) &&
                        !openList.Contains(currentSuccessor))
                    {

                        openList.Enqueue(currentSuccessor,
                                        (float)(currentSuccessor.Cost + currentState.Cost));
                        currentSuccessor.CameFrom = currentState;
                    }
                    else if (openList.Contains(currentSuccessor))
                    {
                        if (currentSuccessor.Cost + currentState.Cost < 
                            getSpecificElementCost(openList, currentSuccessor))
                        {
                            openList.UpdatePriority(currentSuccessor,
                            (float)(currentSuccessor.Cost + currentState.Cost));
                        }
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// building and returning a solution.
        /// </summary>
        /// <param name="initialState">The initial state.</param>
        /// <param name="goalState">State of the goal.</param>
        /// <returns>The solution as we want to see it</returns>
        private Solution<T> backTrace(State<T>initialState,State<T> goalState)
        {
            Solution<T> solution = new Solution<T>();
            State<T> currentState = goalState;
            while (!currentState.Equals(initialState))
            {
                solution.insertNode(currentState);
                currentState = currentState.CameFrom;
            }
            return solution;
        }

        /// <summary>
        /// Gets the specific element cost.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <param name="element">The element.</param>
        /// <returns>float - the cost</returns>
        private float getSpecificElementCost(Priority_Queue.SimplePriorityQueue<State<T>> queue,
                                             State<T> element)
        {
            foreach(State<T> elm in queue)
            {
                if(elm.Equals(element))
                {
                    return (float)elm.Cost;
                }
            }
            return 0;
        }

    }


}
