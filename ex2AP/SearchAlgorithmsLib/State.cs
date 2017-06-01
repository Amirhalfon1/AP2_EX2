using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
namespace SearchAlgorithmsLib
{
    public class State<T> //where T : Position
    {
        private T state;
        private double cost;
        private State<T> cameFrom;

        public State(T state)
        {
            this.state = state;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return state.ToString();
        }

        /// <summary>
        /// Equalses the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public bool Equals(State<T> s)
        {
            return state.Equals(s.state);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
       ///  structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return state.ToString().GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this
       ///  instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance
        /// .</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance;
       ///  otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if(obj!=null) 
             return state.Equals((obj as State<T>).state);
            return false;
        }

        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        public double Cost
        {
            get; set;

        }

        /// <summary>
        /// Gets or sets the came from.
        /// </summary>
        /// <value>
        /// The came from.
        /// </value>
        public State<T> CameFrom
        {
            get; set;

        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>T</returns>
        public T getInstance() {
            return state;
        }

        /// <summary>
        /// State pool static class.
        /// contains a dictionary of unique states.
        /// </summary>
        public static class StatePool
        {
            public static Dictionary<T, State<T>> statesDictionary = new Dictionary<T, State<T>>();

            /// <summary>
            /// Gets the instance requsted from the state pool.
            /// </summary>
            /// <param name="state">The state to get from the pool.</param>
            /// <returns>State<T></returns>
            public static State<T> getInstance(T state)
            {
                //if the statepool already contain the specific state , return it. 
                if (statesDictionary.ContainsKey(state))
                {
                    return statesDictionary[state];
                }
                //if the state pool dosen't contain the state, create it and add it to the state pool.
                State<T> newStateToReturn = new State<T>(state);
                statesDictionary.Add(state, newStateToReturn);
                return newStateToReturn;
            }
        }
    }
}
