using System;
using System.Collections.Generic;
using Klod.Web.Semantic.Interfaces;
using System.Text;
using System.IO;

namespace Klod.Web.Semantic.Common
{
	public class TripleStore:ITripleStore
	{
		private List<ITriple> _triples;

        /// <summary>
        /// Package scope constructor. Why is that? To be created by the root factory.
        /// </summary>
		internal TripleStore()
		{
		    _triples = new List<ITriple>();
		}
		/// <summary>
		/// Return all triples in list.
		/// </summary>
		/// <returns></returns>
		public ITriple[] GetTriples()
		{
			if(_triples.Count>0)
			{
				return _triples.ToArray();
			}

			return null;
		}
		/// <summary>
		/// Return all triples based on a triple argument.
		/// </summary>
		/// <param name="triple"></param>
		/// <returns></returns>
		public ITriple[] GetTriples(ITriple triple)
		{
			//1. Set criteria for search
			//2. Look for every combination of search: s,p,o,spo,sp,so,po or null//all
			//3. Add triples to the return triples list

			//1
			StringBuilder criteria = GetSearchCriteria(triple);

			//if criteria is empty then return all
			if (criteria.ToString() == string.Empty)
			{ 
				return GetTriples();
			}
			 
			List<ITriple> ts=new List<ITriple>();
			foreach (ITriple t in _triples)
			{
				if (criteria.ToString()=="s" || criteria.ToString() == "spo" || criteria.ToString()=="sp" || criteria.ToString() == "so")
				{
				//if it's the same subject, continue
				if (triple.Compare(t.Subject.Value,TriplePart.TripleSubject))
				{
					//if it's only subject then add
					if(criteria.ToString()=="s")
					{
						ts.Add(t);
					}
					//if it has the same subject and predicate
					else if(criteria.ToString() == "spo" || criteria.ToString()=="sp")
					{
						if (triple.Compare(t.Predicate.Value, TriplePart.TriplePredicate))
						{
							//if it's only subject and predicate
							if(criteria.ToString()=="sp")
							{
								ts.Add(t);
							}
							//then if it's subject,predicate and object
							else
							{
								if (triple.Compare(t.Object.Value, TriplePart.TripleObject))
								{
								ts.Add(t);
								}
							}
						}
					}                        
					//if it's subject and object
					else if (criteria.ToString()=="so")
					{
						if (triple.Compare(t.Object.Value, TriplePart.TripleObject))
						{
							ts.Add(t);
						}
					}
				}
				}
				//if there's no subject criteria, but it has a predicate
				else if (criteria.ToString() == "p" || criteria.ToString()=="po")
				{
				if (triple.Compare(t.Predicate.Value,TriplePart.TriplePredicate))
				{
					if (criteria.ToString() == "p")
					{
						ts.Add(t);
					}
					else //then it's predicate and object
					{
						if (triple.Compare(t.Object.Value,TriplePart.TripleObject))
						{
							ts.Add(t);
						}
					}
				}
				}
				//if it's only object
				else if (criteria.ToString() == "o")
				{
				if (triple.Object.Value == t.Object.Value)
				{
					ts.Add(t);
				}
				}
			}
			return ts.ToArray();
		}
		/// <summary>
		/// Return all triples based on a subject, predicate and object argument.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="p"></param>
		/// <param name="o"></param>
		/// <returns></returns>
		public ITriple[] GetTriples(string s,string p,string o)
		{
			Triple triple = new Triple(s, p, o);
			return GetTriples(triple);

		}
		/// <summary>
		/// Return a string in form of "spo" for searching in the triple list.
		/// </summary>
		/// <param name="triple"></param>
		/// <returns></returns>
		private StringBuilder GetSearchCriteria(ITriple triple)
		{
		   StringBuilder criteria = new StringBuilder();
		   if (triple.Subject != null && triple.Subject.Value != string.Empty)
			   criteria.Append("s");
		   if (triple.Predicate != null && triple.Predicate.Value != string.Empty)
			   criteria.Append("p");
		   if (triple.Object != null && triple.Object.Value != string.Empty)
			   criteria.Append("o");

		   return criteria;

		}
		/// <summary>
		/// Accept a set of queries and return a parameter binding set.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
	   // public IParameterBindingSet[] GetParameterBindings(IQuery[] queries)
	   // {
	   //     //1. Get parameters data in queries. Store their names and positions (sub, pred or obj).
	   //     //2. Set the query triples to null in the parameters locations
	   //     //3. Execute the query to get some triples in return.
	   //     //4. Get every triple and bind the return values with parameters. If the parameters exists in the binding set, then compares the
	   //     //existing ones with the return values. If the values are equal then add the binding to the final binding set list

	   //     //List to store the full parameters binding sets.
	   //     List<IParameterBindingSet> bindingsSetList = new List<IParameterBindingSet>();

	   //     //1. Get parameters and store them in the dictionary
	   //     foreach (IQuery query in queries)
	   //     {
	   //         //store parameters and positions (0=subject; 1=predicate; 2=object)
	   //         Dictionary<byte, IStringValue> paramDic = new Dictionary<byte, IStringValue>();

	   //         //Triple parameter for GetTriples method call    
	   //         ITriple triple = this.MakeTriple();


	   //         /////////////////////////////////////////////////////////////
	   //         //it the subject, predicate or object is a parameter or not
	   //         if (query.SubjectValue.Value.StartsWith("?")) //then it's a parameter
	   //         {
	   //         paramDic.Add(1, query.SubjectValue);
	   //         triple.Subject = null; //2. Set values to null when a parameter is founded
	   //         }
	   //         else  //it's a literal value for the query
	   //         {
	   //         triple.Subject.Value = query.SubjectValue.Value;
	   //         }
	   //         if (query.PredicateValue.Value.StartsWith("?"))
	   //         {
	   //         paramDic.Add(2, query.PredicateValue);
	   //         triple.Predicate = null;
	   //         }
	   //         else
	   //         {
	   //         triple.Predicate.Value = query.PredicateValue.Value;
	   //         }
	   //         if (query.ObjectValue.Value.StartsWith("?"))
	   //         {
	   //         paramDic.Add(3, query.ObjectValue);
	   //         triple.Object = null;
	   //         }
	   //         else
	   //         {
	   //         triple.Object.Value = query.ObjectValue.Value;
	   //         }
	   //         ///////////////////

	   //         //Get triples and set parameter bindings
	   //         ITriple[] triples = GetTriples(triple);

	   //         if (triples == null) //if no triples then there's no binding to set to store.
	   //         return null;

	   //         //first make an empty copy of bindings sets and the list
	   //         IParameterBindingSet tempBindingsSet = null;

	   //         //if no bindings yet then add all results in the triples
	   //         if (bindingsSetList.Count == 0)
	   //         {

	   //         //look for every triple
	   //         foreach (ITriple t in triples)
	   //         {
	   //             IParameterBinding binding = null;
	   //             tempBindingsSet = RootFactory.GetInstance().MakeParameterBindingSet();

	   //             //if there's no binding already, then add all values inmediately in the bindings set list
	   //             binding = tempBindingsSet.MakeParameterBinding();

	   //             if (paramDic.ContainsKey(1))
	   //             {
	   //                 //configure the binding
	   //                 binding.Parameter = binding.MakeParameter(paramDic[1].Value);
	   //                 binding.Binding = t.Subject;
	   //                 //add binding to the bindings set
	   //                 tempBindingsSet.Add(binding);
	   //             }
	   //             if (paramDic.ContainsKey(2))
	   //             {
	   //                 binding.Parameter = binding.MakeParameter(paramDic[2].Value);
	   //                 binding.Binding = t.Predicate;
	   //                 tempBindingsSet.Add(binding);
	   //             }
	   //             if (paramDic.ContainsKey(3))
	   //             {
	   //                 binding.Parameter = binding.MakeParameter(paramDic[3].Value);
	   //                 binding.Binding = t.Object;
	   //                 tempBindingsSet.Add(binding);
	   //             }
	   //             //if there are bindings then add to the bindings set list directly
	   //             if (tempBindingsSet.Bindings.GetLength(0) > 0)
	   //             {
	   //                 bindingsSetList.Add(tempBindingsSet);
	   //             }
	   //         }
	   //         }
	   //         else //if there are bindings then compare existing values with the new ones. if equals then select.
	   //         {
	   //         //if (parameterDefined for triple exist in the binding list)
	   //         //then compare the bindings value with the triple value
	   //         //else add the new binding to the set 

	   //         //the new bindings set list to store and replace the current bindings set list
	   //         List<IParameterBindingSet> newBindingsSetList = new List<IParameterBindingSet>();

	   //         //the list has at least one binding set. Get the parameters list
	   //         string[] currentBindings = bindingsSetList[0].GetCurrentParametersList();

	   //         //get the parameters values for the current query
	   //         string[] spo = new string[3];
				
	   //         if(paramDic.ContainsKey(1))
	   //             spo[0] = paramDic[1].Value;
	   //         else spo[0]=string.Empty;

	   //         if (paramDic.ContainsKey(2))
	   //             spo[1] = paramDic[2].Value;
	   //         else spo[1] = string.Empty;

	   //         if (paramDic.ContainsKey(3))
	   //             spo[2] = paramDic[3].Value;
	   //         else spo[2] = string.Empty;
	   //         ////////////////////////////////////////////////////////////////////////////

	   //         //for each parameter binding in triple that exist in the current binding list, compare them to filter only valid
	   //         foreach (ITriple t in triples)
	   //         {
	   //             foreach (IParameterBindingSet pbs in bindingsSetList)
	   //             {
	   //                 bool isMatch = true;
	   //                 bool isNewBinding = true;
	   //                 tempBindingsSet = RootFactory.GetInstance().MakeParameterBindingSet();

	   //                 foreach (IParameterBinding pb in pbs.Bindings)
	   //                 {
	   //                     for (byte b = 0; b < 3; b++)
	   //                     {
	   //                         //if the parameter value it's in the binding set list
	   //                         if (spo[b] == pb.Parameter.Value)
	   //                         {
	   //                         //if it's the subject parameter
	   //                         if (b==0)
	   //                         {
	   //                             //if the values are equals, then add to the new binding set
	   //                             if (t.Subject.Value.ToLower() == pb.Binding.Value.ToLower())
	   //                             {
	   //                                 tempBindingsSet.Add(pb);
	   //                             }
	   //                         }
	   //                         //if it's the predicate parameter
	   //                         else if (b==1)
	   //                         {
	   //                             //if the values are equals, then add to the new binding set
	   //                             if (t.Predicate.Value.ToLower() == pb.Binding.Value.ToLower())
	   //                             {
	   //                                 tempBindingsSet.Add(pb);
	   //                             }
	   //                         }
	   //                         else if (b==2)
	   //                         {
	   //                             //if the values are equals, then add to the new binding set
	   //                             if (t.Object.Value.ToLower() == pb.Binding.Value.ToLower())
	   //                             {
	   //                                 tempBindingsSet.Add(pb);
	   //                             }
	   //                         }

	   //                         }
	   //                         //or it's a new parameter
	   //                         else
	   //                         {
	   //                         ////add binding
	   //                         //IParameterBinding tempBinding = pbs.MakeParameterBinding();
	   //                         //IParameter p = tempBinding.MakeParameter();
	   //                         //IStringValue sv = RootFactory.GetInstance().MakeStringValue();

	   //                         //if (spo[0])
	   //                         //    p.Value = spo[b];
	   //                         //if (b == 0)
	   //                         //{
	   //                         //    sv.Value = t.Subject.Value;
	   //                         //}
	   //                         //else if (b == 1)
	   //                         //{
	   //                         //    sv.Value = t.Predicate.Value;
	   //                         //}
	   //                         //else if (b == 2)
	   //                         //    sv.Value = t.Object.Value;

	   //                         }

	   //                     }
						 
	   //                 }
	   //             }
	   //         }
					  
	   //         bindingsSetList = newBindingsSetList;
	   //         }
	   //     }  
	   //     IParameterBindingSet[] pbsarray = new IParameterBindingSet[bindingsSetList.Count];
	   //     bindingsSetList.CopyTo(pbsarray);
			
	   //     return pbsarray;
	   //}

		public IParameterBindingSet[] GetParameterBindings2(IQuery[] queries)
		{
			//the list that will hold the query sets
			List<IParameterBindingSet> parameterBindingSetList = new List<IParameterBindingSet>();
		  
			IQuerySet querySet = RootFactory.GetInstance().MakeQuerySet();
			querySet.Queries = queries;
		  
			//get parameter binding set template
			IParameterBindingSet parameterBindingSetTempl = querySet.GetParameterBindings();
			//parameterBindingSetList.Add(parameterBindingSetTempl);

			foreach (IQuery q in queries)
			{
				ITriple triple = q.GetTripleForQuery();

				//Get triples and set parameter bindings
				ITriple[] triples = GetTriples(triple);

				if (triples == null)
				return null;

				if (parameterBindingSetList.Count == 0)
				{
				foreach (ITriple t in triples)
				{
						//add a template binding set instance with the triple parameters in the list
					IParameterBindingSet pbs = parameterBindingSetTempl.Clone(); //prototype pattern

					foreach (IParameterBinding bp in parameterBindingSetTempl.Bindings)
					{

						if (bp.Parameter.Value == q.SubjectValue.Value)
						{
							//replace the parameter value with the triple value
							IParameterBinding pb = pbs.GetParameterBinding(q.SubjectValue.Value);
							pb.Binding.Value = t.Subject.Value;
						}

						if (bp.Parameter.Value == q.PredicateValue.Value)
						{
							//replace the parameter value with the triple value
							IParameterBinding pb = pbs.GetParameterBinding(q.PredicateValue.Value);
							pb.Binding.Value = t.Predicate.Value;
						}
						if (bp.Parameter.Value == q.ObjectValue.Value)
						{
							//replace the parameter value with the triple value
							IParameterBinding pb = pbs.GetParameterBinding(q.ObjectValue.Value);
							pb.Binding.Value = t.Object.Value;
						}
					}
					parameterBindingSetList.Add(pbs);
				}
				}
				else
				{

				//the new bindings set list to store and replace the current bindings set list
				List<IParameterBindingSet> newBindingsSetList = new List<IParameterBindingSet>();

				foreach (ITriple t in triples)
				{
					//find binding sets (from query string) based on the existing sets in the list
					foreach(IParameterBindingSet pbs in parameterBindingSetList)
					{
						IParameterBindingSet temppbs = pbs.Clone(); //prototype pattern

						//if the stored binding match the triple results, select the triple instance 
						//and add the new variables bindings if any.
						if (temppbs.IsMatch(t, q))
						{
							newBindingsSetList.Add(temppbs);
						}
					}
					}
				parameterBindingSetList = newBindingsSetList;
				}
			}
			IParameterBindingSet[] pbsarray = new IParameterBindingSet[parameterBindingSetList.Count];
			parameterBindingSetList.CopyTo(pbsarray);

			return pbsarray;
		}

	   
		public ITriple GetValue(ISubject s, IPredicate p, IObject o)
		{
			throw new NotImplementedException();
		}

		public void Add(ITriple triple)
		{
			_triples.Add(triple);
		}
		public void Add(ISubject sub,IPredicate pred,IObject obj)
		{
			Triple t = new Triple(sub, pred, obj);
			Add(t);
		}
		public void Add(string sub,string pred,string obj)
		{
			Subject s = new Subject(sub);
			Predicate p = new Predicate(pred);
			Object o = new Object(obj);

			Triple t = new Triple(s, p, o);
			Add(t);
		}
		public void Remove(ITriple triple)
		{
			//1. Set criteria for search
			//2. Look for every combination of search: s,p,o,spo,sp,so,po or null//all that doesn't match the criteria
			//3. Add triple that doesn't match to a new ist

			//1
			StringBuilder criteria = new StringBuilder();
			if (triple.Subject != null && triple.Subject.Value != string.Empty)
				criteria.Append("s");
			if (triple.Predicate != null && triple.Predicate.Value != string.Empty)
				criteria.Append("p");
			if (triple.Object != null && triple.Object.Value != string.Empty)
				criteria.Append("o");

			//if criteria is empty then clear all
			if (criteria.ToString() == string.Empty)
			{
				_triples.Clear();
			}

			List<ITriple> ts = new List<ITriple>(_triples);

			foreach (ITriple t in _triples)
			{
				if (criteria.ToString() == "s" || criteria.ToString() == "spo" || criteria.ToString() == "sp" || criteria.ToString() == "so")
				{
				//if it's the same subject, continue
				if (triple.Subject.Value == t.Subject.Value)
				{
					//if it has the same subject and predicate
					if (criteria.ToString() == "spo" || criteria.ToString() == "sp")
					{
						if (triple.Predicate.Value == t.Predicate.Value)
						{
							//if it's only subject and predicate
							if (criteria.ToString() == "sp")
							{
								ts.Remove(t);
							}
							//then if it's subject,predicate and object
							else
							{
								if (triple.Object.Value == t.Object.Value)
								{
								ts.Remove(t);
								}
							}
						}
					}
					//if it's only the subject
					else if (criteria.ToString() == "s")
					{
						ts.Remove(t);
					}
					//if it's subject and object
					else if (criteria.ToString() == "so")
					{
						if (triple.Object.Value == t.Object.Value)
						{
							ts.Remove(t);
						}
					}
				}
				}
				//if there's no subject criteria, but it has a predicate
				else if (criteria.ToString() == "p" || criteria.ToString() == "po")
				{
				if (triple.Predicate.Value == t.Predicate.Value)
				{
					if (criteria.ToString() == "p")
					{
						ts.Remove(t);
					}
					else //then it's predicate and object
					{
						if (triple.Object.Value == t.Object.Value)
						{
							ts.Remove(t);
						}
					}
				}
				}
				//if it's only object
				else if (criteria.ToString() == "o")
				{
				if (triple.Object.Value == t.Object.Value)
				{
					ts.Remove(t);
				}
				}
			}
			_triples = ts;
		}
		public void Load(string filename)
		{
			try
			{
				StreamReader file = new StreamReader(filename);
				while (!file.EndOfStream)
				{
				string line = file.ReadLine();
				if (line.Contains("\"")) //if contains double quotes, then eliminate single commas
				{
					line = line.Replace(", ", " "); //assuming inner commas inside double quotes
				}
				string[] triple = line.Split(',');

				this.Add(triple[0], triple[1], triple[2]);
				}
				file.Close();
				//file.Dispose();
				file = null;
			}
			catch 
			{
		   
			}
		}

		/// <summary>
		/// Store the triple store content to disk.
		/// </summary>
		/// <param name="filename"></param>
		public void Save(string filename)
		{
			try
			{
				StreamWriter _file = new StreamWriter(filename, false, Encoding.UTF8);
			_file.Write(SerializeTripleListInMemory());
				_file.Flush();
				_file.Close();
				//_file.Dispose();
				_file = null;
			}
			catch
			{
			
			}
		}
		public string SerializeTripleListInMemory()
		{
			ITriple[] triples = null;
			triples = GetTriples();
			StringBuilder _triples = new StringBuilder();

			foreach (ITriple _triple in triples)
			{
				_triples.Append(_triple.Subject.Value+","+_triple.Predicate.Value+","+_triple.Object.Value + "\n");
			}
			return _triples.ToString();
		}
		public ITriple MakeTriple()
		{
			return new Triple();
		}
		public ITriple MakeTriple(ISubject sub, IPredicate pred, IObject obj)
		{
			return new Triple(sub, pred, obj);
		}
		public ITriple MakeTriple(string sub, string pred, string obj)
		{
			return new Triple(sub, pred, obj);
		}
	}
}
