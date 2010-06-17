using System;
using System.Xml.Linq;

namespace SharpGs.Acl.Scopes
{
    internal class UserById : BaseScope
    {
        private readonly string _id;
        private readonly string _name;

        public UserById(XContainer element)
        {
            _id = GetValue(element, "ID");
            _name = GetValue(element, "Name");
        }

        public UserById(string id, string name)
        {
            _id = id;
            _name = name;
        }

        public override string ToXmlString()
        {
            return String.Format("<Scope type='UserById'><ID>{0}</ID><Name>{1}</Name></Scope>",
                                 EscapeString(_id), EscapeString(_name));
        }
    }
}
