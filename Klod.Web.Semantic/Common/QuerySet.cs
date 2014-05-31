using System;
using System.Collections.Generic;
using Klod.Web.Semantic.Interfaces;
using System.Text;

namespace Klod.Web.Semantic.Common
{
	public class QuerySet:IQuerySet
	{
	   private List<IQuery> _queries;

	   internal QuerySet()
	   {
		  _queries = new List<IQuery>();
	   }
	   public void Add(IQuery query)
	   {
		  _queries.Add(query);
	   }
	   public void Remove(IQuery query)
	   {
		  _queries.Remove(query);
	   }
	   public IQuery[] Queries
	   {
		  get
		  {
			 IQuery[] q = new IQuery[_queries.Count];
			 _queries.CopyTo(q);
			 return q;
		  }

		  set
		  {
			 foreach (IQuery q in value)
			 {
				_queries.Add(q);
			 }
		  }
	   }
	   public string Serialize(char queryValueDelimiter, char queryDelimiter)
	   {
		  string qs=null;
		  foreach (IQuery q in _queries)
		  {
			 qs=q.SubjectValue.Value+queryValueDelimiter.ToString()+q.PredicateValue.Value+queryValueDelimiter+q.ObjectValue.Value+queryDelimiter.ToString()+"\n";
		  }
		  qs.Replace(qs,qs.Substring(0, qs.Length - 1));

		  return qs;
	   }

	   public IQuery MakeQuery()
	   {
		  return new Query();
	   }
	   public IQuery MakeQuery(IStringValue sv, IStringValue pv, IStringValue ov)
	   {
		  return new Query(sv, pv, ov);
	   }
	   public IQuery MakeQuery(string sv,string pv,string ov)
	   {
		  return new Query(sv, pv, ov);
	   }

	   public IParameterBindingSet GetParameterBindings()
	   {
		  IParameterBindingSet pbs = RootFactory.GetInstance().MakeParameterBindingSet();
		  
		  foreach (IQuery q in _queries)
		  {
			 IParameterBinding pb = null;
			 IParameter p = null;
			 IStringValue b = RootFactory.GetInstance().MakeStringValue();

			 if (q.Contains(QueryPart.QuerySubject) && q.SubjectValue.Value.Contains("?"))
			 {
				pb = pbs.MakeParameterBinding();
				p = pb.MakeParameter();
				p.Value = q.SubjectValue.Value;
				b.Value = string.Empty;
				pb.Parameter = p;
				pb.Binding = b;
				if (!pbs.Contains(p))
					pbs.Add(pb);
				
			 } 
			 if (q.Contains(QueryPart.QueryPredicate) && q.PredicateValue.Value.Contains("?"))
			 {
				pb = pbs.MakeParameterBinding();
				p = pb.MakeParameter();
				p.Value = q.PredicateValue.Value;
				b.Value = string.Empty;
				pb.Parameter = p;
				pb.Binding = b;
				if (!pbs.Contains(p))
					pbs.Add(pb);
			 }
			 if (q.Contains(QueryPart.QueryObject) && q.ObjectValue.Value.Contains("?"))
			 {
				pb = pbs.MakeParameterBinding();
				p = pb.MakeParameter();
				p.Value = q.ObjectValue.Value;
				b.Value = string.Empty;
				pb.Parameter = p;
				pb.Binding = b;
				if (!pbs.Contains(p))
					pbs.Add(pb);
			 }
		  }
		  if (pbs.Count > 0)
			 return pbs;
		  return null;
	   }
	 
	}
}
