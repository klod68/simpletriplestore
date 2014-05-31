using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Klod.Web.Semantic.Interfaces
{
	public interface IParameterizable
	{
	   bool IsParameter();
	   bool IsParameter(IStringValue p);
	}
}
