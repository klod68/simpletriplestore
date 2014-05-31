using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleTripleStore
{
    public class EnemyRule:InferenceRule
    {
        public EnemyRule() { }

        public override List<string[]> GetQueries()
        {
            List<string[]> queries = new List<string[]>();
            string[] query =  {"?person,enemy,?enemy","?relation,with,?person","?relation,with,?partner" };

            queries.Add(query);
         
            return queries;
        }
        public override string[] MakeTriple(string bindings)
        {
            string[] tripleString = new string[3];
            string partner = null;
            string enemy = null;

            if (bindings == null)
                return null;

            //assume that the parameter is in the form of ?person:value,?partner:value,?relation:value,?partner:value"
            string[] bs = bindings.Split(',');

            foreach (string b in bs)
            {
                if (b.Contains("?partner") || b.Contains("?enemy"))
                {
                    string[] p = b.Split(':');
                    if(b.Contains("?partner"))
                    {
                        partner=p[1];
                    }
                    else if (b.Contains("?enemy"))
                    {
                        enemy=p[1];
                    }
                }
            }
            tripleString[0] = partner;
            tripleString[1] = "enemy";
            tripleString[2] = enemy;
            return tripleString;
        }
    }
}
