using System;
using System.Collections.Generic;
using Klod.Web.Semantic.Interfaces;
using System.Text;

namespace Klod.Web.Semantic.Common
{
    public class Subject:ISubject
    {
        private string _s;

        internal Subject() { }
        internal Subject(string s)
        {
            _s = s; 
        }
        public string Value
        {
            get
            {
                return _s;
            }
            set
            {
                _s = value;
            }
        }
    }
}
