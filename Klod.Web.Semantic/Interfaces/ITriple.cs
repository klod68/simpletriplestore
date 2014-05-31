using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klod.Web.Semantic.Interfaces
{
	public interface ITriple
	{
		ISubject Subject { set; get; }
		IPredicate Predicate { set; get; }
		IObject Object { set; get; }
		string Serialize(Char delimiter);

	   bool Compare(ITriple triple);
	   bool Compare(string value, TriplePart part);

		ISubject MakeSubject();
		ISubject MakeSubject(string sub);
		IPredicate MakePredicate();
		IPredicate MakePredicate(string pred);
		IObject MakeObject();
		IObject MakeObject(string obj);
	}
}
