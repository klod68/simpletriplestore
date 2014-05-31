using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleTripleStore
{
    public class WestCoastRule:InferenceRule
    {
        public WestCoastRule() { }

        /// <summary>
        /// Get a list of queryies.
        /// </summary>
        /// <returns></returns>
        public override List<string[]> GetQueries()
        {
            List<string[]> queries = new List<string[]>();
            string[] sfoquery = {"?company,headquarters,San_Francisco_California"};
            string[] seaquery = {"?company,headquarters,Seattle_Washington"};
            string[] laxquery = {"?company,headquarters,Los_Angeles_California"};
            string[] porquery = {"?company,headquarters,Portland_Oregon"};
            
            queries.Add(sfoquery);
            queries.Add(seaquery);
            queries.Add(laxquery);
            queries.Add(porquery);

            return queries;
        }
        /// <summary>
        /// Make inferences, based on a known predicate and object.
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public override string[] MakeTriple(string bindings)
        {
            string[] tripleString = new string[3];

            if (bindings == null)
                return null;
            //assume that the parameter is in the form of ?company:value"
            string[] bs = bindings.Split(':');

            tripleString[0] = bs[1]; //second value is the content
            tripleString[1] = "on_coast";
            tripleString[2]="west_coast";
            
            return tripleString;
        }
     
    }
}
