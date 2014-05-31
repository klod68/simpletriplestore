using System;
using System.Collections.Generic;
using Klod.Web.Semantic.Interfaces;
using System.Text;

namespace Klod.Web.Semantic.Common
{
	public class Parameter:IParameter
	{
	   private string _p;

	   internal Parameter(){}
	   internal Parameter(string p)
	   { _p = p; }

	   public bool IsParameter(IStringValue p)
	   {
		   return p.Value.StartsWith("?");
	   }

		public bool IsParameter()
	   {
		  return _p.StartsWith("?");
	   }
	   public string Value
	   {
		  get
		  {
			 return _p;
		  }
		  set
		  {
			 if (value.StartsWith("?"))
				_p = value;
			 else
				_p = "?" + value;
		  }
	   }

	  
	}
}
