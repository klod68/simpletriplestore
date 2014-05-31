using System;
using System.Collections.Generic;
using Klod.Web.Semantic.Interfaces;
using System.Text;

namespace Klod.Web.Semantic.Common
{
	public class Triple:ITriple
	{
	   private ISubject _s;
	   private IPredicate _p;
	   private IObject _o;

	   internal Triple()
	   {
		  _s = new Subject();
		  _p = new Predicate();
		  _o = new Object();
	   }
	   internal Triple(ISubject s, IPredicate p, IObject o)
	   {
		  _s = s;
		  _p = p;
		  _o = o;

		  _s.Value = CleanValue(_s.Value);
		  _p.Value = CleanValue(_p.Value);
		  _o.Value = CleanValue(_o.Value);
	   }
	   internal Triple(string s, string p, string o)
	   {
		  _s = new Subject(CleanValue(s));
		  _p = new Predicate(CleanValue(p));
		  _o = new Object(CleanValue(o));
	   }
	   public ISubject Subject
	   {
		  get
		  {
			 return _s;
		  }
		  set
		  {
			 if(value!=null && value.Value!=string.Empty)
				value.Value = CleanValue(value.Value);
			 _s = value;
		  }
	   }
	   public IPredicate Predicate
	   {
		  get
		  {
			 return _p;
		  }
		  set
		  {
			 if (value != null && value.Value != string.Empty)
				 value.Value = CleanValue(value.Value);
			 _p = value;
		  }
	   }
	   public IObject Object
	   {
		  get
		  {
			 return _o;
		  }
		  set
		  {
			 if (value != null && value.Value != string.Empty)
				 value.Value = CleanValue(value.Value);
			 _o = value;
		  }
	   }

	   public string Serialize(Char delimiter)
	   {
		  return _s.Value+delimiter.ToString()+_p.Value+delimiter.ToString()+_o.Value;
	   }

	   public ISubject MakeSubject()
	   {
		  return new Subject();
	   }
	   public ISubject MakeSubject(string sub)
	   {
		  return new Subject(sub);
	   }
	   public IPredicate MakePredicate()
	   {
		  return new Predicate();
	   }
	   public IPredicate MakePredicate(string pred)
	   {
		  return new Predicate(pred);
	   }
	   public IObject MakeObject()
	   {
		  return new Object();
	   }
	   public IObject MakeObject(string obj)
	   {
		  return new Object(obj);
	   }
	   private string CleanValue(string val)
	   {
		  return val.Replace("\"", "");
	   }

	   public bool Compare(ITriple triple)
	   {
		  if (_s.Value.ToLower() == triple.Subject.Value.ToLower() &&
			 _p.Value.ToLower() == triple.Predicate.Value.ToLower() &&
			 _o.Value.ToLower() == triple.Object.Value.ToLower())
			 return true;
		  return false;
	   }
	   public bool Compare(string value, TriplePart part)
	   {
		  if(part ==TriplePart.TripleSubject)
			 if (_s.Value.ToLower()==CleanValue(value).ToLower())
				return true;
		  if(part==TriplePart.TriplePredicate)
			 if(_p.Value.ToLower()==CleanValue(value).ToLower())
				return true;

		  if (part == TriplePart.TripleObject)
			 if (_o.Value.ToLower() == CleanValue(value).ToLower())
				return true;
		  return false;

	   }
	}
}
