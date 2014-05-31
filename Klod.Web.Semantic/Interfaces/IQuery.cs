using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klod.Web.Semantic.Interfaces
{
	public interface IQuery
	{
	   IStringValue SubjectValue { set; get; } //should accept IParameters or ISubjects
	   IStringValue PredicateValue { set; get; }//should accept IParameters or IPredicates
	   IStringValue ObjectValue { set; get; }//should accept IParameters or IObjects
	   bool IsParameter(QueryPart p);

	   string Serialize(Char delimiter);//get the string in sub,pred,obj format or similar
	   bool Contains(QueryPart qp);
	   ITriple GetTripleForQuery();
	}
}
