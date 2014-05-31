using System;
using System.Collections.Generic;

using System.Text;

namespace Klod.Web.Semantic.Interfaces
{
    /// <summary>
    /// Encapsulate a string primitive value. Can be extended to apply rules or other constraints to the string value type.
    /// </summary>
    public interface IStringValue
    {
        string Value { set; get; }
    }
}
