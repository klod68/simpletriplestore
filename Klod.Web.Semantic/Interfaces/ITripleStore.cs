using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klod.Web.Semantic.Interfaces
{
	public interface ITripleStore
	{
		ITriple[] GetTriples();
		ITriple[] GetTriples(ITriple t);
		ITriple[] GetTriples(string s, string p, string o);
		//IParameterBindingSet[] GetParameterBindings(IQuery[] q);
		IParameterBindingSet[] GetParameterBindings2(IQuery[] q);
		ITriple GetValue(ISubject s, IPredicate p, IObject o);
		void Add(ITriple triple);
		void Remove(ITriple triple);
		void Load(string filename);
		void Save(string filename);

		ITriple MakeTriple();
		ITriple MakeTriple(ISubject sub, IPredicate pred, IObject obj);
		ITriple MakeTriple(string sub, string pred, string oobj);

	}
}
