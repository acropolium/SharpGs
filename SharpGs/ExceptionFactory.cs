using System;
using System.Linq;
using System.Xml.Linq;
using SharpGs.Exceptions;

namespace SharpGs
{
    internal static class Factory
    {
        public static Exception FindException(this XElement error)
        {
            var code = error.Descendants(@"Code").First().Value;
            var exType = typeof(BaseGoogleException).Assembly.GetType(typeof(BaseGoogleException).Namespace + "." + code);
            if (exType == null)
                return new BaseGoogleException(code + ": " + error.Descendants(@"Message").First().Value, error);
            return Activator.CreateInstance(exType, error) as BaseGoogleException;
        }
    }
}
