using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleTripleStore
{
    public abstract class InferenceRule
    {
        /// <summary>
        /// Get a list of queries.
        /// </summary>
        /// <returns></returns>
        public abstract List<string[]> GetQueries();
        /// <summary>
        /// Make a triple.
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        public abstract string[] MakeTriple(string bindings);
    }
}
