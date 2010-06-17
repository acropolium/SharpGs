using System.Collections.Generic;
using System.Xml.Linq;

namespace SharpGs.Acl
{
    public interface IAccessControlList
    {
        IOwner Owner { get; }
        IEnumerable<IAclEntry> Entries { get; }
        IAclEntry AddEntry(AclPermission permission, ScopeType scopeType, params string[] scopeParameters);
        void RemoveEntry(IAclEntry entry);
        void CleanEntries();
        string ToXmlString();
        XDocument ToXml();
        void Save();
    }
}
