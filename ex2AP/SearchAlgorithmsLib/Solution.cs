using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Solution<T>
    {
        protected List<State<T>> pathToGoal;
        public Solution()
        {
            pathToGoal = new List<State<T>>();
        }


        /// <summary>
        /// Pathes to goal.
        /// </summary>
        /// <returns> List<State<T>></returns>
        public List<State<T>> PathToGoal() 
        {
                return pathToGoal;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder pathToReturn = new StringBuilder();
            foreach(State<T> node in pathToGoal)
            {
                pathToReturn.Append(node.ToString()+ " ");
            }
            return pathToReturn.ToString();
        }

        /// <summary>
        /// Inserts the node.
        /// </summary>
        /// <param name="node">The node.</param>
        public void insertNode(State<T> node)
        {
            pathToGoal.Add(node);
        }

        /// <summary>
        /// Solutions the length.
        /// </summary>
        /// <returns>int</returns>
        public int solutionLength()
        {
            return pathToGoal.Count();
        }
    }
}
