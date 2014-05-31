using System;
using System.Collections.Generic;
using Klod.Web.Semantic.Interfaces;
using System.Text;

namespace Klod.Web.Semantic.Common
{
	public class Object:IObject
	{
		private string _o;

	   internal Object() { }
	   internal Object(string o)
	   {
		  _o = o;
	   }
	
		public string  Value
		{
			get 
			{ 
				return _o; 
			}
			set 
			{
				_o = value; 
			}
		}
	}
}
