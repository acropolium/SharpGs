using System;
using System.Xml.Linq;

namespace SharpGs.Acl.Scopes
{
    internal class GroupById : BaseScope
    {
        private readonly string _id;
        private readonly string _name;

        public GroupById(XContainer element)
        {
            _id = GetValue(element, "ID");
            _name = GetValue(element, "Name");
        }

        public GroupById(string id, string name)
        {
            _id = id;
            _name = name;
        }

        public override string ToXmlString()
        {
            return String.Format("<Scope type='GroupById'><ID>{0}</ID><Name>{1}</Name></Scope>",
                                 EscapeString(_id), EscapeString(_name));
        }
    }
}
