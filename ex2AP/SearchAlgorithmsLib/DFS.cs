using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class DFS<T> : Searcher<T>
    {
        /// <summary>
        /// Searches the specified searchable by the dfs algo.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns>solution of the dfs</returns>
        public override Solution<T> search(ISearchable<T> searchable)
        {
            
            Stack<State<T>> nodesStack = new Stack<State<T>>();
            HashSet<State<T>> closedNodesSet = new HashSet<State<T>>();

            //adding the intial State to the stack and the hashset.
            nodesStack.Push(searchable.getInitialState());
            evaluatedNodes++;
            closedNodesSet.Add(searchable.getInitialState());

            while (nodesStack.Count() != 0)
            {
                State<T> currentNode = nodesStack.Peek();
                List<State<T>> succerssors = searchable.getAllPossibleStates(currentNode);
                
                for (int i = 0; i < succerssors.Count; ++i)
                {
                    if (!closedNodesSet.Contains(succerssors[i]))
                    {
                        nodesStack.Push(succerssors[i]);
                        //increasing the counter
                        evaluatedNodes++;
                        closedNodesSet.Add(succerssors[i]);
                        if (succerssors[i].Equals(searchable.getGoalState())) {
                            return backTrace(nodesStack);
                        }
                        break;
                    }

                    //checking if all the succerssors already visited
                    if (i == succerssors.Count - 1)
                        nodesStack.Pop();
                }
            }

            return null;
       }


        /// <summary>
        /// returns the solution as we want to see it.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>return the solution of the bfs as we want to see it</returns>
        private Solution<T> backTrace(Stack<State<T>> path)
        {
            Solution<T> solution = new Solution<T>();
            State<T> currentState = null;
            if (path.Count > 0)
            {
                currentState = path.Pop();
            }

            while (path.Count != 0)
            {
                currentState.CameFrom = path.Peek();
                solution.insertNode(currentState);
                currentState = path.Pop();
            }

            currentState.CameFrom = currentState;
           
            return solution;
        }


    }
}
