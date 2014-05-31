using System;
using System.Collections.Generic;
using Klod.Web.Semantic.Interfaces;
using System.Text;

namespace Klod.Web.Semantic.Common
{
	public class Query:IQuery
	{
	   private IStringValue _sv;
	   private IStringValue _pv;
	   private IStringValue _ov;

	   internal Query() 
	   {
		  _sv = new StringValue();
		  _pv =  new StringValue();
		  _ov = new StringValue();

	   }
	   internal Query(IStringValue sv,IStringValue pv,IStringValue ov)
	   {
		  _sv = sv;
		  _pv = pv;
		  _ov = ov;
	   }
	   internal Query(string sv,string pv,string ov)
	   {
		  _sv = RootFactory.GetInstance().MakeStringValue(sv);
		  _pv = RootFactory.GetInstance().MakeStringValue(pv);
		  _ov = RootFactory.GetInstance().MakeStringValue(ov);

	   }

	   public IStringValue SubjectValue
	   {
		  get
		  {
			 return _sv; ;
		  }
		  set
		  {
			 _sv = value;
		  }
	   }
	   public IStringValue PredicateValue
	   {
		  get
		  {
			 return _pv;
		  }
		  set
		  {
			 _pv = value;
		  }
	   }
	   public IStringValue ObjectValue
	   {
		  get
		  {
			 return _ov;
		  }
		  set
		  {
			 _ov=value;
		  }
	   }
	   public string Serialize(char delimiter)
	   {
		  return _sv.Value + delimiter.ToString() + _pv.Value + delimiter.ToString() + _ov.Value;
	   }
	   public bool Contains(QueryPart part)
	   {
		  if (part == QueryPart.QuerySubject && _sv != null)
			return true;
		  
		  if (part == QueryPart.QueryPredicate && _pv!= null)
			 return true;
		  
		  if (part == QueryPart.QueryObject && _ov != null)
			 return true;
		  return false;
	   }
	   public ITriple GetTripleForQuery()
	   {
		  ITriple t = RootFactory.GetInstance().MakeTripleStore().MakeTriple();


		  if (_sv != null && !_sv.Value.Contains("?"))
			 t.Subject.Value = _sv.Value;
		  else
			 t.Subject = null;

		  if (_pv != null && !_pv.Value.Contains("?"))
			 t.Predicate.Value = _pv.Value;
		  else
			 t.Predicate = null;

		  if (_ov != null && !_ov.Value.Contains("?"))
			 t.Object.Value = _ov.Value;
		  else
			 t.Object = null;

		  return t;
	   }

	   public bool IsParameter(QueryPart part)
	   {
		  if (part == QueryPart.QuerySubject && _sv != null && _sv.Value.StartsWith("?"))
			 return true;

		  if (part == QueryPart.QueryPredicate && _pv != null && _pv.Value.StartsWith("?"))
			 return true;

		  if (part == QueryPart.QueryObject && _ov != null && _ov.Value.StartsWith("?"))
			 return true;

		  return false;
	   }
	}
}
