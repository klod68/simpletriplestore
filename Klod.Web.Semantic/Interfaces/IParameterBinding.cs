using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klod.Web.Semantic.Interfaces
{
	public interface IParameterBinding
	{
	   IParameter Parameter { set; get; }//parameter in format of ?parameter or similar
	   IStringValue Binding { set; get; }//value assigned to parameter
	   string Serialize(Char delimiter);//get the parameter:binding string

	   IParameter MakeParameter();
	   IParameter MakeParameter(string parameter);
	}
}
