using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klod.Web.Semantic.Interfaces
{
	public interface IParameterBindingSet
	{
	   IParameterBinding[] Bindings { set; get; }
	   void Add(IParameterBinding pb);
	   void Remove(IParameterBinding pb);
	   int Count{get;}
	   string[] GetCurrentParametersList();
	   bool Contains(IParameterBinding pb);
	   bool Contains(IParameter p);
	   IParameterBinding GetParameterBinding(IParameter p);
	   IParameterBinding GetParameterBinding(string p);
	   IParameterBinding GetParameterBinding(string p, string v);
	   bool IsMatch(ITriple t,IQuery q);
	   void AddFromTriple(ITriple t);

	   IParameterBindingSet Clone();
	   string Serialize(Char parValueDelimiter,Char bindingDelimiter);

	   IParameterBinding MakeParameterBinding();
	   IParameterBinding MakeParameterBinding(IParameter parameter, IStringValue binding);
	   IParameterBinding MakeParameterBinding(string parameter, string binding);
	}
}
