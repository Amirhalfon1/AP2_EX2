using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public interface ISearchable <T>
    {
        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns>initial state</returns>
        State<T> getInitialState();

        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns>goal state</returns>
        State<T> getGoalState();

        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>list of all possible states</returns>
        List<State<T>> getAllPossibleStates(State<T> s);
    }
}
