using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klod.Web.Semantic.Interfaces
{
	public interface IQuerySet
	{
	   IQuery[] Queries { set; get; }
	   void Add(IQuery q);
	   void Remove(IQuery q);
	   IQuery MakeQuery();
	   IQuery MakeQuery(IStringValue sv, IStringValue pv, IStringValue ov);
	   IQuery MakeQuery(string sv, string pv, string ov);
	   IParameterBindingSet GetParameterBindings();
	}
}
