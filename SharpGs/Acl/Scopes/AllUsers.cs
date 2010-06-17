using System;
using System.Xml.Linq;

namespace SharpGs.Acl.Scopes
{
    internal class AllUsers : BaseScope
    {
        public AllUsers(XContainer element) { }

        public AllUsers() { }

        public override string ToXmlString()
        {
            return String.Format("<Scope type='AllUsers' />");
        }
    }
}
