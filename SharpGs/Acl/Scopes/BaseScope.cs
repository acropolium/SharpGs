using System.Linq;
using System.Xml.Linq;

namespace SharpGs.Acl.Scopes
{
    internal abstract class BaseScope : IScope
    {
        protected string EscapeString(string value)
        {
            return (value ?? string.Empty).Replace("<", "&lt;").Replace("<", "&gt;");
        }

        protected static string GetValue(XContainer container, string key)
        {
            return container.Descendants(key).Select(e => e.Value).FirstOrDefault();
        }

        public abstract string ToXmlString();
    }
}
