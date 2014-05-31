using System;
using System.Collections.Generic;
using Klod.Web.Semantic.Interfaces;
using System.Collections;
using System.Text;

namespace Klod.Web.Semantic.Common
{
	public class ParameterBindingSet:IParameterBindingSet
	{
	   private List<IParameterBinding> _bindings;

	   internal ParameterBindingSet()
	   {
		  _bindings = new List<IParameterBinding>();
	   }

	   public IParameterBinding[] Bindings
	   {
		  get
		  {
			 IParameterBinding[] b = new IParameterBinding[_bindings.Count];
			 _bindings.CopyTo(b);
		
			 return b;
		  }
		  set
		  {
			 foreach(IParameterBinding p in value)
			 {
				_bindings.Add(p);
			 }
		  }
	   }
	   public void Add(IParameterBinding p)
	   {
		  if (_bindings.Contains(p))
			 return;
		  _bindings.Add(p);
	   }
	   public void Remove(IParameterBinding p)
	   {
		  if (_bindings.Contains(p))
		  {
			 _bindings.Remove(p);
		  }
	   }

	   public int Count
	   {
		  get
		  {
			 if (_bindings != null)
			 {
				return _bindings.Count;
			 }
			 return 0;
		  }
	   }
	   public bool Contains(IParameter p)
	   {
		  foreach (IParameterBinding b in _bindings)
		  {
			 if (b.Parameter.Value.ToLower() == p.Value.ToLower())
			 {
				return true;
			 }
		  }
		  return false;
	   }
	   public bool Contains(IParameterBinding pb)
	   {
		  foreach(IParameterBinding b in _bindings)
		  {
			 if (b.Parameter.Value.ToLower() == pb.Parameter.Value.ToLower()
				&& b.Binding.Value.ToLower() == pb.Binding.Value.ToLower())
			 {
				return true;
			 }
		  }
		  return false;
	   }

	   public string[] GetCurrentParametersList()
	   {
		  if (_bindings.Count > 0)
		  {
			  string[] bindings = new string[_bindings.Count];
			  for(int i=0;i<_bindings.Count;i++)
			  {
				bindings[i] = _bindings[i].Parameter.Value;
			  }
			 return bindings;
		  }
		  return null;
	   }

	   public IParameterBinding GetParameterBinding(IParameter p)
	   {
		  foreach (IParameterBinding pb in _bindings)
		  {
			 if (pb.Parameter.Value==p.Value)
				return pb;
		  }
		  return null;
	   }
	   public IParameterBinding GetParameterBinding(string p)
	   {
		  foreach (IParameterBinding pb in _bindings)
		  {
			 if (pb.Parameter.Value == p)
				return pb;
		  }
		  return null;
	   }
	   public IParameterBinding GetParameterBinding(string p,string v)
	   {
		  ParameterBinding pb = new ParameterBinding(p, v);
		  return pb;
	   }

	   public IParameterBindingSet Clone()
	   {
		  IParameterBindingSet pbs = RootFactory.GetInstance().MakeParameterBindingSet();
		  foreach (IParameterBinding pb in _bindings)
		  {
			 IParameterBinding newPb = pbs.MakeParameterBinding();
			 newPb.Parameter.Value = pb.Parameter.Value;
			 newPb.Binding.Value = pb.Binding.Value;

			 pbs.Add(newPb);

		  }
		  return pbs;
	   }
	   public string Serialize(char parValueDelimiter, char bindingDelimiter)
	   {
		  string ps=null;
		  StringBuilder serial = new StringBuilder();

		  foreach (IParameterBinding p in _bindings)
		  {
			 ps = p.Parameter.Value + parValueDelimiter.ToString() + p.Binding.Value; //+bindingDelimiter.ToString();
			 ps = ps + "\n";
			 serial.Append(ps);
		  }
		  
		  return serial.ToString();
	   }


	   public IParameterBinding MakeParameterBinding()
	   {
		  return new ParameterBinding();
	   }

	   public IParameterBinding MakeParameterBinding(IParameter parameter, IStringValue binding)
	   {
		  return new ParameterBinding(parameter, binding);
	   }
	   public IParameterBinding MakeParameterBinding(string parameter,string binding)
	   {
		  return new ParameterBinding(parameter, binding);
	   }

	   public bool IsMatch(ITriple t, IQuery q)
	   {
		  int intersectCount=0;
		  IParameterBindingSet qpbs = GetParameterBindingSetFromQuery(t, q);

		  //if parameters intersect and are equal its values
		  foreach (IParameterBinding pbFromQuery in qpbs.Bindings)
		  {
			 foreach (IParameterBinding pb in _bindings)
			 {
				if (ParameterBindingIntersects(pb.Parameter.Value, pbFromQuery.Parameter.Value))
				{
					intersectCount++;
					if (pb.Binding.Value != string.Empty)
					{
					   if (!BindigsAreEquals(pb.Binding.Value, pbFromQuery.Binding.Value))
					   {
						  return false;
					   }
					}
					else
					{
					  pb.Binding.Value = pbFromQuery.Binding.Value;
					}
				}
			 }
		  }
		  if (intersectCount==0)
			 return false;
		  
		  return true;
	   }
	   private bool BindigsAreEquals(string bindingValue,string newBindingValue)
	   {
		  return bindingValue == newBindingValue;
	   }
	   private bool ParameterBindingIntersects(string storedParameter,string newParameter)
	   {
		  return storedParameter == newParameter;
	   }
	   private IParameterBindingSet GetParameterBindingSetFromQuery(ITriple t, IQuery q)
	   {
		  IParameterBindingSet qpbs = RootFactory.GetInstance().MakeParameterBindingSet();
		  IParameterBinding qpb = null;
		 

		  if (q.IsParameter(QueryPart.QuerySubject))
		  {
			 //compare the bindings with the query+triple subject value
			 qpb = qpbs.MakeParameterBinding();
			 qpb.Parameter.Value = q.SubjectValue.Value;
			 qpb.Binding.Value = t.Subject.Value;
			 qpbs.Add(qpb);
		  }
		  if (q.IsParameter(QueryPart.QueryPredicate))
		  {
			 //compare the bindings with the query+triple subject value
			 qpb = qpbs.MakeParameterBinding();
			 qpb.Parameter.Value = q.PredicateValue.Value;
			 qpb.Binding.Value = t.Predicate.Value;
			 qpbs.Add(qpb);
		  }
		  if (q.IsParameter(QueryPart.QueryObject))
		  {
			 //compare the bindings with the query+triple subject value
			 qpb = qpbs.MakeParameterBinding();
			 qpb.Parameter.Value = q.ObjectValue.Value;
			 qpb.Binding.Value = t.Object.Value;
			 qpbs.Add(qpb);
		  }
		  return qpbs;
	   }

	   public void AddFromTriple(ITriple t)
	   {
		  throw new NotImplementedException();
	   }
	}
}
