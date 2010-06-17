using System;
using System.Xml.Linq;

namespace SharpGs.Acl.Scopes
{
    internal class AllAuthenticatedUsers : BaseScope
    {
        public AllAuthenticatedUsers(XContainer element) { }

        public AllAuthenticatedUsers() { }

        public override string ToXmlString()
        {
            return String.Format("<Scope type='AllAuthenticatedUsers' />");
        }
    }
}
