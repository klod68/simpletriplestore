using System;
using System.Collections.Generic;
using Klod.Web.Semantic.Common;
using Klod.Web.Semantic.Interfaces;
using System.Text;

namespace Klod.Web.Semantic
{
    /// <summary>
    /// Singleton class (thread-safe) to create root objects (ITripleStore, IQuerySet, IParameterBindingSet, and IStringValue types.
    /// </summary>
	public class RootFactory
	{
		static readonly RootFactory _me = new RootFactory();

		private RootFactory() { }
		static RootFactory() { }

		public static RootFactory GetInstance()
		{
			return _me;
		}
		public ITripleStore MakeTripleStore()
		{
			return new TripleStore();
		}
		public IQuerySet MakeQuerySet()
		{
			return new QuerySet();
		}
		public IParameterBindingSet MakeParameterBindingSet()
		{
			return new ParameterBindingSet();
		}
		public IStringValue MakeStringValue()
		{
			return new StringValue();
		}
	   public IStringValue MakeStringValue(string value)
	   {
		  return new StringValue(value);
	   }

	}
}
