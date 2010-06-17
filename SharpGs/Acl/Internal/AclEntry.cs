using System;
using System.Linq;
using System.Xml.Linq;

namespace SharpGs.Acl.Internal
{
    internal class AclEntry : IAclEntry
    {
        public AclPermission Permission { get; private set; }
        public IScope Scope { get; private set; }

        public AclEntry(XContainer element)
        {
            AclPermission permission;
            Permission =
                Enum.TryParse(element.Descendants("Permission").Select(e => e.Value).FirstOrDefault() ?? String.Empty,
                              out permission)
                    ? permission
                    : AclPermission.READ;
            Scope = ScopeBuilder.CreateScope(element.Descendants("Scope").First());
        }

        public AclEntry(AclPermission permission, IScope scope)
        {
            Permission = permission;
            Scope = scope;
        }

        public string ToXmlString()
        {
            return String.Format("<Entry><Permission>{0}</Permission>{1}</Entry>", Permission, Scope.ToXmlString());
        }
    }
}
