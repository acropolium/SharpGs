using System;
using System.Linq;
using System.Xml.Linq;

namespace SharpGs.Exceptions
{
    public class BaseGoogleException : Exception
    {
        public XElement GoogleMessage
        {
            get; private set;
        }

        public BaseGoogleException(XElement content)
            : this(content.Descendants(@"Message").First().Value, content)
        { }

        public BaseGoogleException(string message, XElement content)
            : base(message)
        {
            GoogleMessage = content;
        }
    }
}
