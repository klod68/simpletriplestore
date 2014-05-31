using System;
using System.Collections.Generic;
using Klod.Web.Semantic.Interfaces;
using System.Text;

namespace Klod.Web.Semantic.Common
{
	public class StringValue:IStringValue
	{
		private string _v;

		internal StringValue() 
		{
			_v = string.Empty;
		}
	   internal StringValue(string value)
	   {
		  _v = value;
	   }

		public string Value
		{
			get
			{
				return _v;
			}
			set
			{
				_v = value;
			}
		}
	}
}
