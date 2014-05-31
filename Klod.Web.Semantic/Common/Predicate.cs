using System;
using System.Collections.Generic;
using Klod.Web.Semantic.Interfaces;
using System.Text;

namespace Klod.Web.Semantic.Common
{
    public class Predicate:IPredicate
    {
        private string _p;

        internal Predicate() { }
        internal Predicate(string p)
        { _p = p; }

        public string Value
        {
            get
            {
                return _p;
            }
            set
            {
                _p = value;
            }
        }
    }
}
