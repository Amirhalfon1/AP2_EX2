using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    interface ISearcher<T>
    {

        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns>Solution<T></returns>
        Solution<T> search(ISearchable<T> searchable);


        /// <summary>
        /// get how many nodes were evaluated by the algorithm
        /// </summary>
        /// <returns>the number</returns>
        int getNumberOfNodesEvaluated();
    }
}
