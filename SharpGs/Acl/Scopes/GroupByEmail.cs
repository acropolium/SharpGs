using System;
using System.Xml.Linq;

namespace SharpGs.Acl.Scopes
{
    internal class GroupByEmail : BaseScope
    {
        private readonly string _emailAddress;
        private readonly string _name;

        public GroupByEmail(XContainer element)
        {
            _emailAddress = GetValue(element, "EmailAddress");
            _name = GetValue(element, "Name");
        }

        public GroupByEmail(string email, string name)
        {
            _emailAddress = email;
            _name = name;
        }

        public override string ToXmlString()
        {
            return String.Format("<Scope type='GroupByEmail'><EmailAddress>{0}</EmailAddress><Name>{1}</Name></Scope>",
                                 EscapeString(_emailAddress), EscapeString(_name));
        }
    }
}
