using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class PrioritySearcher<T> : Searcher<T>
    {
        protected SimplePriorityQueue<State<T>> openList;
        //private int evaluatedNodes;

        public PrioritySearcher()
        {
            openList = new SimplePriorityQueue<State<T>>();
        }

        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        public override abstract Solution<T> search(ISearchable<T> searchable);

        /// <summary>
        /// Pops the open list.
        /// </summary>
        /// <returns></returns>
        protected State<T> popOpenList()
        {
            evaluatedNodes++;
            return openList.Dequeue();
        
        }

        /// <summary>
        /// Adds to open list.
        /// </summary>
        /// <param name="stateToAdd">The state to add.</param>
        protected void addToOpenList(State<T> stateToAdd)
        {
            openList.Enqueue(stateToAdd,(float)stateToAdd.Cost);
        }

        /// <summary>
        /// Gets the size of the open list.
        /// </summary>
        /// <value>
        /// The size of the open list.
        /// </value>
        public int OpenListSize
        {
            get { return openList.Count; }
        }





    }
}

