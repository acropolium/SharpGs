using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SharpGs.Acl.Internal
{
    internal class AccessControlList : IAccessControlList
    {
        private readonly IAclSetup _ownerObject;

        public IOwner Owner { get; private set; }

        public AccessControlList(XContainer document, IAclSetup ownerObject)
        {
            _ownerObject = ownerObject;

            var element = document.Descendants("Owner").FirstOrDefault();
            if (element != null)
            {
                Owner = new SharpGs.Internal.Owner(null)
                            {
                                ID = element.Descendants("ID").Select(e => e.Value).FirstOrDefault(),
                                DisplayName = element.Descendants("Name").Select(e => e.Value).FirstOrDefault()
                            };
            }
            foreach (var aclEntry in document.Descendants("Entry"))
                _entries.Add(new AclEntry(aclEntry));
        }

        private readonly List<IAclEntry> _entries = new List<IAclEntry>();
        public IEnumerable<IAclEntry> Entries
        {
            get { return _entries; }
        }

        public IAclEntry AddEntry(AclPermission permission, ScopeType scopeType, params string[] scopeParameters)
        {
            var entry = new AclEntry(permission, ScopeBuilder.CreateScope(scopeType, scopeParameters));
            _entries.Add(entry);
            return entry;
        }

        public void RemoveEntry(IAclEntry entry)
        {
            _entries.Remove(entry);
        }

        public void CleanEntries()
        {
            _entries.Clear();
        }

        public XDocument ToXml()
        {
            return XDocument.Parse(ToXmlString());
        }

        public void Save()
        {
            _ownerObject.AclSave(this);
        }

        public string ToXmlString()
        {
            var sb = new StringBuilder("<?xml version='1.0' encoding='utf-8'?>").AppendLine();
            sb.AppendLine("<AccessControlList>");
            sb.AppendLine("\t<Owner>");
            sb.AppendLine("\t\t<ID>" + (Owner.ID ?? String.Empty) + "</ID>");
            sb.AppendLine("\t\t<Name>" + (Owner.DisplayName ?? String.Empty) + "</Name>");
            sb.AppendLine("\t</Owner>");
            sb.AppendLine("\t<Entries>");
            foreach (var entry in Entries)
                sb.Append("\t\t").AppendLine(entry.ToXmlString());
            sb.AppendLine("\t</Entries>");
            sb.AppendLine("</AccessControlList>");
            return sb.ToString();
        }
    }
}
