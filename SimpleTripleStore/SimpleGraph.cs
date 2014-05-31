using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace SimpleTripleStore
{
    /// <summary>
    /// SimpleGraph class translation from python to c#, from the book "Semantic Web", isbn:978-0-596-15381-6
    /// </summary>
    public class SimpleGraph
    {
        //triples indexes
        private Dictionary<string, Dictionary<string, List<string>>> _spo;
        private Dictionary<string, Dictionary<string, List<string>>> _pos;
        private Dictionary<string, Dictionary<string, List<string>>> _osp;

        /// <summary>
        /// Initialize the three dictionaries (with inner dictionary and inner list) that behave as indexes
        /// </summary>
        public SimpleGraph()
        {
            _spo = new Dictionary<string, Dictionary<string, List<string>>>();
            _pos = new Dictionary<string, Dictionary<string, List<string>>>();
            _osp = new Dictionary<string, Dictionary<string, List<string>>>();
        }

        /// <summary>
        /// Add triples to dictionaries/indexes in three forms or variations.
        /// </summary>
        /// <param name="sub"></param>
        /// <param name="pred"></param>
        /// <param name="obj"></param>
        public void Add(string sub,string pred,string obj)
        {
            _AddToIndex(_spo,sub,pred,obj);
            _AddToIndex(_pos,pred,obj,sub);
            _AddToIndex(_osp,obj,sub,pred);
        }
        public void Add(string[] triple)
        {
            //check null values and the size of the array
            if (triple != null && triple.GetUpperBound(0) < 2)
                return;
            Add(triple[0], triple[1], triple[2]);
        }
        /// <summary>
        /// Add triples in specific index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        private void _AddToIndex(Dictionary<string, Dictionary<string, List<string>>> index,string a,string b,string c)
        {
            Dictionary<string,List<string>> _innerDic = new Dictionary<string,List<string>>();
            List<string> _innerList = new List<string>();
            if (!index.ContainsKey(a)) //if not has the outer index key, add it
            {
                //add c value in list
                _innerList.Add(c);
                //add list value in inner dic
                _innerDic.Add(b,_innerList);
                //then add inner dic in the index
                index.Add(a, _innerDic);
            }
            else if (!index[a].ContainsKey(b)) //if there's an existing outer index key, but no inner dic key, then add it
            {
                _innerList.Add(c);
                _innerDic.Add(b,_innerList);
                index[a].Add(b,_innerList);
            }
            else if (!index[a][b].Contains(c)) //if there are outer and inner index keys, but no list value, then add it
            {
                index[a][b].Add(c);
            }
        }

        /// <summary>
        /// Remove any key from indexes
        /// </summary>
        /// <param name="sub"></param>
        /// <param name="pred"></param>
        /// <param name="obj"></param>
        public void Remove(string sub,string pred,string obj)
        {
            //get the triples and remove them from index
            string[] _forDelete=Triples(sub,pred,obj);
            if (_forDelete == null)
                return;
            foreach(string _triple in _forDelete)
            {
                //split by comma
                string[]  _values=_triple.Split(',');
                _RemoveFromIndex(_spo, _values[0], _values[1], _values[2]);
                _RemoveFromIndex(_pos, _values[1], _values[2], _values[0]);
                _RemoveFromIndex(_osp, _values[2], _values[0], _values[1]);

            }
        }
        private void _RemoveFromIndex(Dictionary<string, Dictionary<string, List<string>>> index,string a,string b,string c)
        {
            try
            {
                if (index.ContainsKey(a)) //index
                {
                    if (index[a].ContainsKey(b)) //inner dic
                    {
                        if (index[a][b].Contains(c)) //inner list
                        {
                            index[a][b].Remove(c);
                        }
                    }
                }
            }
            catch { }
           
        }
        /// <summary>
        /// Load triples stored in a csv file.
        /// </summary>
        /// <param name="filename"></param>
        public void Load(string filename)
        {
            try
            {
                StreamReader file = new StreamReader(filename);
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();
                    string[] triple = line.Split(',');

                    this.Add(triple[0], triple[1], triple[2]);
                }
                file.Close();
                //file.Dispose();
                file = null;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }

        }
        /// <summary>
        ///Save all in-memory triple store to file
        /// </summary>
        /// <param name="filename"></param>
        public void Save(string filename)
        {
            try
            {
                StreamWriter _file = new StreamWriter(filename, false, Encoding.UTF8);
                _file.Write(this._SerializeTripleListInMemory(Triples(null, null, null)));
                _file.Flush();
                _file.Close();
                //_file.Dispose();
                _file = null;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }
        /// <summary>
        /// Serialize triples in memory
        /// </summary>
        /// <param name="triples"></param>
        /// <returns></returns>
        private string _SerializeTripleListInMemory(string[] triples)
        {
            StringBuilder _triples=new StringBuilder();

            foreach (string _triple in triples)
            {
                _triples.Append(_triple + "\n");
            }
            return _triples.ToString();
        }

        public string[] Triples(string sub,string pred, string obj)
        {
            string[] _results = null; //represents an array of triples comma-delimited records
            StringBuilder _triplesString = new StringBuilder(); //global string of the current triple collection 
            string _triple = string.Empty; //string of the triple
            try
            {
                if (sub != null && sub != string.Empty) //if it's not null nor empty the subject, then query index for similarity
                {
                    if (!this._spo.ContainsKey(sub)) //if no sub key present then the resultset must be empty
                        return null;
                    if (pred != null && pred != string.Empty)
                    {
                        if (!this._spo[sub].ContainsKey(pred))  //if no pred key present then the resultset must be empty
                            return null;
                        if (obj != null && obj != string.Empty) //sub-pred-obj
                        {
                            if (!this._spo[sub][pred].Contains(obj)) //if no obj key present then the resulset must be empty
                                return null;
                            //if all keys are present then return the instances than have all of them
                            for (int i = 0; i < this._spo[sub][pred].Count; i++) //loop through the whole list to find matched obj with sub,pred keys
                            {
                                if (this._spo[sub][pred][i] == obj)
                                {
                                    _triple = sub + "," + pred + "," + this._spo[sub][pred][i];
                                    _triplesString.Append(_triple + "\n"); //get the triple
                                    break;
                                }
                            }
                        }
                        else //sub-pred-null
                        {
                            //if no obj is present then search for sub and pred only
                            for (int i = 0; i < this._spo[sub][pred].Count; i++) //loop through the whole list to add all objs
                            {
                                _triple = sub + "," + pred + "," + this._spo[sub][pred][i];
                                _triplesString.Append(_triple + "\n"); //get the triple
                            }
                        }
                    }
                    else //sub-null-obj, sub key exists
                    {
                        
                        if (obj != null && obj != string.Empty)
                        {
                            //check if there's available obj key in _osp 
                            if (!this._osp.ContainsKey(obj))
                                return null;
                            //check if there's avaliable obj + sub key combination
                            if (!this._osp[obj].ContainsKey(sub))
                                return null;

                            for (int i = 0; i < this._osp[obj][sub].Count; i++) //loop through the whole list to add all
                            {
                                _triple = sub + "," + this._osp[obj][sub][i] + "," + obj; //then get all set (sub, pred, obj)
                                _triplesString.Append(_triple + "\n"); //get the triple
                            }

                        }
                        else //sub-null-null, sub key exists
                        {
                            foreach (string _pred in this._spo[sub].Keys)//loop through all pred dictionary
                            {
                                for (int i = 0; i < this._spo[sub][_pred].Count; i++) //loop through the whole list to add all objs
                                {
                                    _triple = sub + "," + _pred + "," + this._spo[sub][_pred][i];
                                    _triplesString.Append(_triple + "\n"); //get the triple
                                }
                            }
                        }
                    }
                }
                else //if sub is null or empty
                {
                    //check if there's a pred
                    if (pred != null && pred != string.Empty)
                    {
                        //check if pred key exists
                        if (!this._pos.ContainsKey(pred))
                            return null;

                        if (obj != null && obj != string.Empty) //null-pred-obj
                        {
                            if (!this._pos[pred].ContainsKey(obj)) //if pred key exist but no obj, then no resultset is posible
                                return null;

                            for (int i = 0; i < this._pos[pred][obj].Count; i++) //loop through the whole list to find matched sub
                            {
                                _triple = this._pos[pred][obj][i] + "," + pred + "," + obj; //then get all set
                                _triplesString.Append(_triple + "\n"); //get the triple
                            }

                        }
                        else //null-pred-null, pred key exists
                        {
                            foreach (string _obj in this._pos[pred].Keys)//loop through all obj dictionary
                            {
                                for (int i = 0; i < this._pos[pred][_obj].Count; i++) //loop through the whole list to add all subjs
                                {
                                    _triple = this._pos[pred][_obj][i] + "," + pred + "," + _obj; //return any sub, pred, and any obj
                                    _triplesString.Append(_triple + "\n"); //get the triple
                                }
                            }
                        }
                    }
                    else //null-null-obj
                    {
                        if (obj != null && obj != string.Empty) 
                        {
                            if (!this._osp.ContainsKey(obj))//null-null-obj
                                return null;

                            foreach (string _sub in this._osp[obj].Keys)//loop through all sub dictionary
                            {
                                for (int i = 0; i < this._osp[obj][_sub].Count; i++) //loop through the whole list to add all preds
                                {
                                    _triple = _sub + "," + _osp[obj][_sub][i] + "," + obj;//return all resultsets in sub, pred, obj format
                                    _triplesString.Append(_triple + "\n"); //get the triple
                                }
                            }

                        }
                        else //null-null-null
                        {
                            foreach (string _sub in this._spo.Keys) //loop through all sub dictionary
                            {
                                foreach (string _pred in this._spo[_sub].Keys)//loop through all pred dictionary
                                {
                                    for (int i = 0; i < this._spo[_sub][_pred].Count; i++) //loop through the whole list to add all objs
                                    {
                                        _triple = _sub + "," + _pred + "," + _spo[_sub][_pred][i];
                                        _triplesString.Append(_triple + "\n"); //get the triple
                                    }
                                }
                            }
                        }
                    }

                }
                //then return all as a list

                if (_triplesString.Length == 0)
                    return null;

                _triplesString.Remove(_triplesString.Length - 1, 1);
                _results = _triplesString.ToString().Split('\n');
                return _results;
            }
            catch (Exception e)
            {

                Debug.Write(e.Message);
                return null;
            }
        }
       

        /// <summary>
        /// Get single first value from two parameters.
        /// </summary>
        /// <param name="sub"></param>
        /// <param name="pred"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string Value(string sub,string pred,string obj)
        {
            string[] _triples = Triples(sub, pred, obj);
            if (_triples != null && _triples.Length > 0)
            {
                string[] _values=_triples[0].Split(',');
                if (string.IsNullOrEmpty(sub))
                    return _values[0];
                else if (string.IsNullOrEmpty(pred))
                    return _values[1];
                else if (string.IsNullOrEmpty(obj))
                    return _values[2];
            }
            return string.Empty;
        }

        public string[] Query(params string[] clauses)
        {
            //to store variables names and position in clauses. E. g. "?variable",1
            Dictionary<string, byte> bpos = null; 
            //list to store all variable bindings (?variable,value2,...)
            List<Dictionary<string,string>> bindings = null; 

            //for each clause ("sub,pred,obj") in clauses with variables
            foreach (string clause in clauses)
            {
                //initialize a new dictionary with variables:positions for the next clause
                bpos = new Dictionary<string, byte>();
                //split the clause to check every part (sub,pred,obj)
                string[] items = clause.Split(','); //replace qc variable in code of p. 41
                //if the part is a variable (starts with ?)
                for (byte b = 0; b < 3; b++)
                {
                    //if it's a variable
                    if (items[b].StartsWith("?"))
                    {
                        bpos.Add(items[b], b);//store variable value and its position in the triple
                        items[b] = null; //set item to null for query the Triples method
                    }
                }
                //get all values that match and store them in rows
                string[] rows = Triples(items[0], items[1], items[2]);
                //if there's no rows then no binding is possible, then go to next clause
                if (rows == null) 
                    continue;

                //if there's no bindings yet, all rows will enter in the bindings dictionary
                if (bindings==null)
                {
                    bindings = new List<Dictionary<string, string>>();
                    //go through all rows to fill variables:value pairs in bindings dictionary
                    foreach (string row in rows)
                    {
                        string[] rowValues = row.Split(',');
                        Dictionary<string, string> binding = new Dictionary<string, string>();
                        foreach (string key in bpos.Keys)
                        {
                            byte pos = bpos[key];
                            binding.Add(key, rowValues[pos]);
                        }
                        bindings.Add(binding);
                    }
                      
                }
                else //it there are values, then filter new values with the existing ones
                {
                    //items
                    //=======
                    //1. bpos = has all variables:positions in clause
                    //2. bindings = it's a list of dictionaries with one variables:values pair
                    //3. rows = it's a string (comma separated) array of sub,pred,obj containing all values returned by Triples method
                    //goal
                    //====
                    //get all variables:values requested by query parameters

                    List<Dictionary<string,string>> newBindings = new List<Dictionary<string,string>>();
                    //for each dictionary in the bindings exclude the entries if the new clause doesn't match
                    foreach (Dictionary<string, string> binding in bindings)
                    {
                        foreach (string row in rows)
                        {
                            string[] rowValues=row.Split(',');
                            bool validMatch = true;
                            Dictionary<string, string> tempBinding = new Dictionary<string, string>(binding);
                            foreach (string key in bpos.Keys)
                            {
                                byte pos=bpos[key];
                                if (tempBinding.ContainsKey(key))
                                {
                                    if (tempBinding[key] != rowValues[pos])
                                    {
                                        validMatch = false;
                                    }
                                }
                                else
                                {
                                    tempBinding.Add(key, rowValues[pos]);
                                }
                            }
                            if (validMatch)
                            {
                                newBindings.Add(tempBinding);
                            }

                        }
                    }
                    bindings = newBindings;
                
                }
            }
            //if there's no binding then exit
            if (bindings == null)
                return null;
            StringBuilder results = new StringBuilder();
            foreach (Dictionary<string,string> binding in bindings)
            {
                foreach(string key in binding.Keys)
                {
                    results.Append(key + ":" + binding[key] + ",");
                }
                results.Replace(results.ToString(),results.ToString().Substring(0, results.ToString().Length - 1));
                results.Append("\n");
            }
            if (results.Length == 0)
                return null;

            results.Remove(results.Length - 1, 1);

            string[] final = results.ToString().Split('\n');


            return final;
        }

        public void ApplyInference(InferenceRule rule)
        {
            //get the array of queries parameters
            List<string[]>queries = rule.GetQueries();
            
            string[] bindings = null;
            string[] newTriple = null;


            foreach (string[] query in queries)
            {
                //get a set of parameter bindings (term:value). E.g. "?param:value1,param2:value2")
                bindings = Query(query);
                //for each binding set add a triple/statement expressing the rule
                foreach (string binding in bindings)
                {
                   newTriple = rule.MakeTriple(binding); //make a triple string passing binding values
                   Add(newTriple);
                }
            }
        }

        public void SaveTriplesToDotFile(string filename,string[] triples)
        {
            try
            {
                StreamWriter _file = new StreamWriter(filename, false, Encoding.UTF8);
                _file.Write("graph \"SimpleGraph\" {\n");
                _file.Write("overlap = \"scale\";\n");
                foreach (string triple in triples)
                {
                    string[] t = triple.Split(',');
                    string g = "\"" + t[0] + "\" -- \"" + t[2] + "\" [label=\"" + t[1] + "\"];\n";
                    g=g.Replace("\"\"","\"");
                    _file.Write(g);
                }
                _file.Write("\n}");
                _file.Flush();
                _file.Close();
                //_file.Dispose();
                _file = null;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }
        public void SaveQueryToDotFile(string filename, string[] queries,string p1,string p2)
        {
            try
            {
                StreamWriter _file = new StreamWriter(filename, false, Encoding.UTF8);
                List<string> graphs = new List<string>();
 
                _file.Write("graph \"SimpleGraph\" {\n");
                _file.Write("overlap = \"scale\";\n");

                //get every single binding (?par1:value,?par2:value...)
                foreach (string binding in queries)
                {     
                    //variables to store node values for the graphic
                    string v1=string.Empty;
                    string v2=string.Empty;

                    //split into a single parameter:value pair in binding
                    string[] b = binding.Split(',');
                    
                    //get every binding
                    foreach (string pv in b)
                    {

                        //if the string contains selected paramaeters
                        if (pv.Contains(p1) || pv.Contains(p2))
                        {
                            //split
                            string[] qv = pv.Split(':');
                            if (qv[0] == p1)
                            {
                                v1=qv[1];
                            }
                            else //the it's p2
                            {
                                v2=qv[1];
                            }

                        }
                        else
                            continue;
                     
                    }
                    if (v1 == string.Empty || v2 == string.Empty)
                        continue;
                    if (v1 == v2)
                        continue;
                    
                    string g = "\"" + v1 + "\" -- \"" + v2 + "\";\n";
                    g = g.Replace("\"\"", "\"");
                    if (!graphs.Contains(g))
                    {
                        graphs.Add(g);
                        _file.Write(g);
                    }
                }
                _file.Write("\n}");
                _file.Flush();
                _file.Close();
                //_file.Dispose();
                _file = null;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }
       
    }
}
