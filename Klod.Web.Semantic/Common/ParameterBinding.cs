using System;
using System.Collections.Generic;
using Klod.Web.Semantic.Interfaces;
using System.Text;

namespace Klod.Web.Semantic.Common
{
	public class ParameterBinding:IParameterBinding
	{
	   private IParameter _p;
	   private IStringValue _b;

	   internal ParameterBinding() 
	   {
		  _p = new Parameter();
		  _b = new StringValue();
	   }
	   internal ParameterBinding(IParameter p,IStringValue b)
	   { 
		  _p = p;
		  _b = b;
	   }
	   internal ParameterBinding(string p,string b)
	   {
		  _p = this.MakeParameter(p);
		  _b = RootFactory.GetInstance().MakeStringValue(b);
	   }

	   public IParameter Parameter
	   {
		  get
		  {
			 return _p;
		  }
		  set
		  {
			 _p=value;
		  }
	   }

	   public IStringValue Binding
	   {
		  get
		  {
			 return _b;
		  }
		  set
		  {
			 _b=value;
		  }
	   }

	   public string Serialize(char delimiter)
	   {
		  return _p.Value+ delimiter.ToString()+_b.Value;
	   }

	   public IParameter MakeParameter()
	   {
		  return new Parameter();
	   }
	   public IParameter MakeParameter(string parameter)
	   {
		  return new Parameter(parameter);
	   }
	}
}
