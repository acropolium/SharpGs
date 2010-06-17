using System;
using System.Xml.Linq;

namespace SharpGs.Acl.Scopes
{
    internal class GroupByDomain : BaseScope
    {
        private readonly string _domain;

        public GroupByDomain(XContainer element)
        {
            _domain = GetValue(element, "Domain");
        }

        public GroupByDomain(string domain)
        {
            _domain = domain;
        }

        public override string ToXmlString()
        {
            return String.Format("<Scope type='GroupByDomain'><Domain>{0}</Domain></Scope>",
                                 EscapeString(_domain));
        }
    }
}
